import { Component, inject, input, OnInit, output, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminService } from '../../../core/services/admin-service';
import { RentalCompany } from '../../../Enviroment/model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-update-company',
  imports: [ReactiveFormsModule],
  templateUrl: './admin-update-company.html',
  styleUrl: './admin-update-company.css',
})
export class AdminUpdateCompany implements OnInit {
  fb = inject(FormBuilder);
  adminService = inject(AdminService);
  toastr = inject(ToastrService);
  rentalCompanyInput = input<RentalCompany>();

  closeModal = output<void>();
  loadcompanyOutput = output<void>();
  onClose() {
    this.closeModal.emit();
  }

  ngOnInit(): void {
    this.updateCompanyForm.patchValue({
      companyName: this.rentalCompanyInput()?.companyName,
      ownerName: this.rentalCompanyInput()?.ownerName,
      companyType: this.rentalCompanyInput()?.companyType.toString(),
      phoneNumber: this.rentalCompanyInput()?.phoneNumber,
      headquartersLocation: this.rentalCompanyInput()?.headquartersLocation,
      licenseNumber: this.rentalCompanyInput()?.licenseNumber,
      status: this.rentalCompanyInput()?.status.toString(),
    });
  }

  updateCompanyForm = this.fb.nonNullable.group({
    companyName: ['', [Validators.required, Validators.maxLength(150)]],
    ownerName: ['', [Validators.required, Validators.maxLength(100)]],
    companyType: ['', [Validators.required]],
    phoneNumber: ['', [Validators.required, Validators.pattern(/^[6-9]\d{9}$/)]],
    headquartersLocation: ['', [Validators.required, Validators.maxLength(200)]],
    licenseNumber: ['', [Validators.required]],
    status: ['', [Validators.required]],
  });

  onSubmit() {
    const data = this.updateCompanyForm.getRawValue();

    if (this.updateCompanyForm.invalid) {
      this.updateCompanyForm.markAllAsTouched();
      return;
    }

    this.adminService
      .updateCompany({
        CompanyId: this.rentalCompanyInput()?.id!,
        CompanyName: data.companyName,
        CompanyType: +data.companyType,
        OwnerName: data.ownerName,
        PhoneNumber: data.phoneNumber,
        HeadquartersLocation: data.headquartersLocation,
        LicenseNumber: data.licenseNumber,
        Status: +data.status,
      })
      .subscribe({
        next: (res) => {
          this.toastr.success('company updated');
          this.onClose();
        },
        error :(err)=>{
          this.toastr.error(err.error?.detail || "Something went wrong");
        }
      });

    console.log(data);
  }
}
