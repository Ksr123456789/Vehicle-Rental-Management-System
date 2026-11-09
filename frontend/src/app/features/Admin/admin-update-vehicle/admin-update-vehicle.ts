import { Component, inject, input, OnInit, output, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminService } from '../../../core/services/admin-service';
import { RentalCompany, Vehicle, VehicleCategory } from '../../../Enviroment/model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-update-vehicle',
  imports: [ReactiveFormsModule],
  templateUrl: './admin-update-vehicle.html',
  styleUrl: './admin-update-vehicle.css',
})
export class AdminUpdateVehicle implements OnInit {
  fb = inject(FormBuilder);
  adminService = inject(AdminService);
  toastr = inject(ToastrService);
  rentalCompanyInput = input<RentalCompany[]>([]);
  vehicleCategoryInput = input<VehicleCategory[]>([]);
  vehicleInput = input<Vehicle>();
  loadVehicleOutput = output<void>();

  closeModal = output<void>();
  onClose() {
    this.closeModal.emit();
  }

  updateVehicleForm = this.fb.nonNullable.group({
    makeAndModel: ['', [Validators.required, Validators.maxLength(100)]],
    licensePlate: ['', [Validators.required, Validators.maxLength(20)]],
    registrationNumber: ['', [Validators.required, Validators.maxLength(50)]],
    yearofManufacture: ['', [Validators.required]],
    seatingCapacity: [
      '',
      [
        Validators.required,
        Validators.min(1),
        Validators.pattern(/^[1-9]\d*$/),
        Validators.max(20),
      ],
    ],
    dailyRentalRate: ['', [Validators.required, Validators.min(0.00000000001)]],
    vehicleAvailabilityStatus: ['', [Validators.required]],
    mileage: ['', [Validators.required, Validators.min(0.000001)]],
    rentalCompanyId: ['', [Validators.required]],
    vehicleCategoryId: ['', [Validators.required]],
  });

  ngOnInit(): void {
    this.updateVehicleForm.patchValue({
      makeAndModel: this.vehicleInput()?.makeAndModel,
      licensePlate: this.vehicleInput()?.licensePlate,
      registrationNumber: this.vehicleInput()?.registrationNumber.toString(),
      yearofManufacture: this.vehicleInput()?.yearofManufacture.toString(),
      seatingCapacity: this.vehicleInput()?.seatingCapacity.toString(),
      vehicleAvailabilityStatus: this.vehicleInput()?.vehicleAvailabilityStatus.toString(),
      mileage: this.vehicleInput()?.mileage.toString(),
      vehicleCategoryId: this.vehicleInput()?.vehicleCategoryId.toString(),
      rentalCompanyId: this.vehicleInput()?.rentalCompanyId.toString(),
      dailyRentalRate: this.vehicleInput()?.dailyRentalRate.toString(),
    });
  }

  years = signal(
    Array.from(
      { length: new Date().getFullYear() - 1950 + 1 },
      (_, index) => new Date().getFullYear() - index,
    ),
  );

  onSubmit() {
    const data = this.updateVehicleForm.getRawValue();

    if (this.updateVehicleForm.invalid) {
      this.updateVehicleForm.markAllAsTouched();
      return;
    }

    this.adminService
      .updateVehicle({
        VehicleId: this.vehicleInput()?.id!,
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
          this.toastr.success('Vehicle Updated');
          this.onClose();
        },
        error: (err) => {
          this.toastr.error(err.error?.detail || 'something went wrong');
        },
      });
  }
}
