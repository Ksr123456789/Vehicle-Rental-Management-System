import { Component, inject, input, output, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminService } from '../../../core/services/admin-service';
import { RentalCompany, VehicleCategory } from '../../../Enviroment/model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-add-vehicle',
  imports: [ReactiveFormsModule],
  templateUrl: './admin-add-vehicle.html',
  styleUrl: './admin-add-vehicle.css',
})
export class AdminAddVehicle {
  fb = inject(FormBuilder);
  adminService = inject(AdminService);
  toastr = inject(ToastrService);
  rentalCompanyInput = input<RentalCompany[]>([]);
  vehicleCategoryInput = input<VehicleCategory[]>([]);
  loadVehicleOutput = output<void>();

  closeModal = output<void>();
  onClose() {
    this.closeModal.emit();
  }

  addVehicleForm = this.fb.nonNullable.group({
    makeAndModel: ['', [Validators.required, Validators.maxLength(100)]],
    licensePlate: ['', [Validators.required, Validators.maxLength(20)]],
    registrationNumber: ['', [Validators.required, Validators.maxLength(50)]],
    yearofManufacture: ['', [Validators.required]],
    seatingCapacity: [
      '',
      [
        Validators.required,
        Validators.pattern(/^[1-9]\d*$/),
        Validators.min(1),
        Validators.max(20),
      ],
    ],
    dailyRentalRate: ['', [Validators.required, Validators.min(0.00000000001)]],
    vehicleAvailabilityStatus: ['', [Validators.required]],
    mileage: ['', [Validators.required, Validators.min(0.000001)]],
    rentalCompanyId: ['', [Validators.required]],
    vehicleCategoryId: ['', [Validators.required]],
  });

  years = signal(
    Array.from(
      { length: new Date().getFullYear() - 1950 + 1 },
      (_, index) => new Date().getFullYear() - index,
    ),
  );

  onSubmit() {
    const data = this.addVehicleForm.getRawValue();

    if (this.addVehicleForm.invalid) {
      this.addVehicleForm.markAllAsTouched();
      return;
    }

    this.adminService
      .addVehicle({
        MakeAndModel: data.makeAndModel,
        LicensePlate: data.licensePlate,
        RegistrationNumber: data.registrationNumber,
        YearofManufacture: +data.yearofManufacture,
        SeatingCapacity: +data.seatingCapacity,
        DailyRentalRate: +data.dailyRentalRate,
        VehicleAvailabilityStatus: +data.vehicleAvailabilityStatus,
        Mileage: +data.mileage,
        RentalCompanyId: +data.rentalCompanyId,
        VehicleCategoryId: +data.vehicleCategoryId,
      })
      .subscribe({
        next: (res) => {
          this.toastr.success('Vehicle added');
          this.onClose();
        },
        error: (err) => {
          this.toastr.error(err.error?.detail || 'something went wrong');
        },
      });

    console.log(data);
  }
}
