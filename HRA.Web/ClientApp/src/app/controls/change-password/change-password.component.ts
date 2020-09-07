import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { UserService } from '../../services/user.service';
import { AlertService } from '../../services/AlertService.service';
import { MustMatch } from '../../helpers/must-match.validators';

@Component({ templateUrl: 'change-password.component.html' })
export class ChangePasswordComponent implements OnInit {
  changePasswordForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.changePasswordForm = this.formBuilder.group({
      currentPassword: ['', [Validators.required, Validators.minLength(6)]],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, {
      validator: MustMatch('newPassword', 'confirmPassword')
  });
  }

  // convenience getter for easy access to form fields
  get f() { return this.changePasswordForm.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.changePasswordForm.invalid) {
      return;
    }

    this.loading = true;
    this.userService.changePassword(this.changePasswordForm.value)
      .pipe(first())
      .subscribe(
        () => {
          this.alertService.success('Password changed successfully', true);
          this.router.navigate(['/home']);
        },
        error => {
          if (error.error instanceof Array) {
            error.error.map(err => this.alertService.error(err.value));
          } else {
            this.alertService.error(error);
          }
          this.loading = false;
        });
  }
}
