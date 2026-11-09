import { Component, inject, input, OnInit, output, signal } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { AvailableVehicle, customerDetailDto, ExtraService } from '../../../Enviroment/model';
import { CustomerService } from '../../../core/services/customer-service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../core/services/auth-service';

@Component({
  selector: 'app-book-vehicle',
  imports: [ReactiveFormsModule],
  templateUrl: './book-vehicle.html',
  styleUrl: './book-vehicle.css',
})
export class BookVehicle implements OnInit {
  vehicleInput = input<AvailableVehicle>();
  closeModal = output<void>();

  customerService = inject(CustomerService);
  authService = inject(AuthService);
  toastr = inject(ToastrService);

  extraServices = signal<ExtraService[]>([]);

  pickupReturnDateTimeValidator(control: AbstractControl): ValidationErrors | null {
    const pickupDate = control.get('pickupDate')?.value;
    const pickupTime = control.get('pickupTime')?.value;
    const returnDate = control.get('returnDate')?.value;
    const returnTime = control.get('returnTime')?.value;
    if (!pickupDate || !pickupTime || !returnDate || !returnTime) return null;

    const pickupDateTime = new Date(`${pickupDate}T${pickupTime}`);
    const returnDateTime = new Date(`${returnDate}T${returnTime}`);

    return pickupDateTime >= returnDateTime ? { pickupInvalid: true } : null;
  }

  fb = inject(FormBuilder);
  bookVehicleForm = this.fb.nonNullable.group(
    {
      pickupDate: ['', Validators.required],
      pickupTime: ['', Validators.required],
      returnDate: ['', Validators.required],
      returnTime: ['', Validators.required],
      pickupLocation: ['', Validators.required],
      returnLocation: ['', Validators.required],
      extraServiceIds: this.fb.control<number[]>([]),
      insurance: [false],
    },
    {
      validators: [this.pickupReturnDateTimeValidator],
    },
  );

  toggleExtraService(serviceId: number) {
    const selected = this.bookVehicleForm.controls.extraServiceIds.value ?? [];

    if (selected.includes(serviceId)) {
      this.bookVehicleForm.controls.extraServiceIds.setValue(
        selected.filter((id) => id !== serviceId),
      );
    } else {
      this.bookVehicleForm.controls.extraServiceIds.setValue([...selected, serviceId]);
    }
  }

  loadExtraService() {
    this.customerService.getExtraServices().subscribe({
      next: (res) => {
        this.extraServices.set(res);
        console.log(this.extraServices());
      },
    });
  }

  minDateString: string = '';

  ngOnInit(): void {
    this.loadExtraService();

    const today = new Date();
    const year = today.getFullYear();
    const mnth = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    this.minDateString = `${year}-${mnth}-${day}`;
  }

  onSubmit() {
    const data = this.bookVehicleForm.getRawValue();

    if (this.bookVehicleForm.invalid) {
      this.bookVehicleForm.markAllAsTouched();
      return;
    }

    const pickupDateTime = new Date(`${data.pickupDate}T${data.pickupTime}`);

    const returnDateTime = new Date(`${data.returnDate}T${data.returnTime}`);

    this.customerService
      .bookVehicle({
        VehicleId: this.vehicleInput()?.id,
        PickUpDateTime: pickupDateTime.toISOString(),
        ReturnDateTime: returnDateTime.toISOString(),
        PickupLocation: data.pickupLocation,
        ReturnLocation: data.returnLocation,
        Insurance: data.insurance,
        ExtraServiceIds: data.extraServiceIds!,
      })
      .subscribe({
        next: (res) => {
          this.closeModal.emit();
          this.toastr.success('Applied for the vehicle');
        },
        error: (err) => {
          this.toastr.error(err.error?.detail || 'something went wrong');
        },
      });
  }
}
