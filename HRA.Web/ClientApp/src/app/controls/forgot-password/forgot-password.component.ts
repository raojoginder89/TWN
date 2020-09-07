import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';
import { AlertService } from 'src/app/services/AlertService.service';

@Component({
    templateUrl: './forgot-password.component.html',
    styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
    forgotPasswordForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private userService: UserService,
        private alertService: AlertService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(['/']);
        }
    }

    ngOnInit() {
        this.forgotPasswordForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.email]]
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.forgotPasswordForm.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.forgotPasswordForm.invalid) {
            return;
        }

        this.loading = true;
        this.userService.forgotPassword(this.forgotPasswordForm.value)
            .pipe(first())
            .subscribe(
                (): void => {
                    this.loading = false;
                    this.alertService.success('Password reset link is sent to your registered email', true);
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
