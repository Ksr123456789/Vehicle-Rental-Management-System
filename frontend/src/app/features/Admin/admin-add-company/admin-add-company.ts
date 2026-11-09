import { Component, inject, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminService } from '../../../core/services/admin-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-add-company',
  imports: [ReactiveFormsModule],
  templateUrl: './admin-add-company.html',
  styleUrl: './admin-add-company.css',
})
export class AdminAddCompany {
  fb = inject(FormBuilder);
  adminService = inject(AdminService);
  toastr = inject(ToastrService);

  closeModal = output<void>();
  onClose() {
    this.closeModal.emit();
  }
  loadCompanyOutput = output<void>();

  addCompanyForm = this.fb.nonNullable.group({
    companyName: ['', [Validators.required, Validators.maxLength(150)]],
    ownerName: ['', [Validators.required, Validators.maxLength(100)]],
    companyType: ['', [Validators.required]],
    phoneNumber: ['', [Validators.required, Validators.pattern(/^[6-9]\d{9}$/)]],
    headquartersLocation: ['', [Validators.required, Validators.maxLength(200)]],
    licenseNumber: ['', [Validators.required]],
    status: ['', [Validators.required]],
  });

  onSubmit() {
    const data = this.addCompanyForm.getRawValue();

    if (this.addCompanyForm.invalid) {
      this.addCompanyForm.markAllAsTouched();
      return;
    }

    this.adminService
      .addCompany({
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
          this.toastr.success("company added");
          
          this.onClose();
        },
        error:(err) =>{
          this.toastr.error(err.error?.detail || "something went wrong");
          }
      });

    console.log(data);
  }
}
