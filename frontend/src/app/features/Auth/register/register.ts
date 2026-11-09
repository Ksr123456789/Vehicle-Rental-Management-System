import { Component, inject, OnInit, signal } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
  ɵInternalFormsSharedModule,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth-service';
import { ToastrService } from 'ngx-toastr';
import { Loader } from '../../../shared/loader/loader';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register implements OnInit {
  fb = inject(FormBuilder);
  authService = inject(AuthService);
  router = inject(Router);
  toastr = inject(ToastrService);
  loading = signal(false);

  registerForm = this.fb.nonNullable.group(
    {
      fullName: [
        '',
        [
          Validators.required,
          Validators.maxLength(100),
          Validators.pattern(/^[a-zA-Z ]*$/),
        ],
      ],
      email: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/),
        ],
      ],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(24),
          Validators.pattern(
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).+$/,
          ),
        ],
      ],

      confirmPassword: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[6-9]\d{9}$/)]],
      drivingLicenseNumber: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9]+$/)]],
      licenseExpiryDate: ['', Validators.required],
      address: ['', [Validators.required, Validators.maxLength(300)]],
    },
    {
      validators: [this.passwordMatchValidator],
    },
  );

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (!password || !confirmPassword) return null;

    return password === confirmPassword ? null : { passwordMismatch: true };
  }

   minDateString: string = '';

   ngOnInit(): void {
     
    const future = new Date();
    future.setDate(future.getDate() + 1);
    const year = future.getFullYear();
    const mnth = String(future.getMonth() + 1).padStart(2, '0');
    const day = String(future.getDate()).padStart(2, '0');
    this.minDateString = `${year}-${mnth}-${day}`;
   }

  onSubmit() {
    const data = this.registerForm.getRawValue();

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      console.log('invalid form');
      return;
    }

    this.loading.set(true);
    this.authService
      .register({
        FullName: data.fullName,
        Email: data.email,
        Password: data.password,
        ConfirmPassword: data.confirmPassword,
        PhoneNumber: data.phoneNumber,
        DrivingLicenseNumber: data.drivingLicenseNumber,
        LicenseExpiryDate: data.licenseExpiryDate,
        Address: data.address,
      })
      .subscribe({
        next: (res) => {
          this.authService.setUser(res);
          console.log(this.authService.getRole());
          if (this.authService.getRole() == 'Admin') {
            this.router.navigate(['/admin']);
          } else if (this.authService.getRole() == 'Customer') {
            this.router.navigate(['/customer']);
          } else {
            this.router.navigate(['/login']);
          }
          this.toastr.success('user registred successfully');
          this.loading.set(false);
        },
        error: (err) => {
          this.toastr.error(err.error?.detail || "something went wrong");
          this.loading.set(false)
        },
      });
  }
}
