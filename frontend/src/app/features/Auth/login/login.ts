import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth-service';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Loader } from '../../../shared/loader/loader';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  authService = inject(AuthService);
  router = inject(Router);
  fb = inject(FormBuilder);
  toastr = inject(ToastrService);
  loading = signal(false);

  loginForm = this.fb.nonNullable.group({
    email: [
      '',
      [Validators.required, Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)],
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
  });

  onSubmit() {
    console.log(this.loginForm.value);
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }
    this.loading.set(true);
    const credentials = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password,
    };
    this.authService.login(credentials).subscribe({
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
        this.loading.set(false);
        this.toastr.success('logged in successfully');
      },
      error: (err) => {
        this.toastr.error(err.error?.detail || 'something went wrong');
        this.loading.set(false);
      },
    });
  }
}
