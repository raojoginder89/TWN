import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { first } from 'rxjs/operators';
import { HRAService } from 'src/app/services/hra.service';
import { GroupService } from 'src/app/services/group.service';
import { AlertService } from 'src/app/services/AlertService.service';
import { HRADetails } from 'src/app/models/hraDetails';
import { QuestionService } from 'src/app/data-driven/question.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  templateUrl: 'hra.component.html',
  styleUrls: ['./hra.component.scss']
})
export class HRAFormComponent implements OnInit {
  hraForm: FormGroup;
  validLink = false;
  loading = false;
  submitted = false;
  private _groupId: string;
  private _groupName: string;
  questions;
  ssn;
  constructor(
    private _router: Router,
    private _hraService: HRAService,
    private _groupService: GroupService,
    private _alertService: AlertService,
    private _route: ActivatedRoute,
    private questionService: QuestionService,
    private _authenticationService: AuthenticationService
  ) {
    this._route.params.subscribe(params => { this._groupId = params['groupId']; });
    this.questionService.getQuestions().subscribe(x => {
      this.questions = x;
    });

    this._authenticationService.currentUser.subscribe(x => {
      if (!!x) {
        this.ssn = x.ssn;
      }
    });
  }

  ngOnInit() {
    this._alertService.clear();
    this._groupService.getGroupName(this._groupId).subscribe(groupName => {
      this._groupName = groupName.value;
      this.hraForm.controls['groupName'].patchValue(this._groupName);
      this.validLink = true;
    }, (error) => {
      console.log(error);
      // this._alertService.error('Invalid Link');
      // this.validLink = false;
    });
  }

  submitQuestionaire(payload: HRADetails) {
    this.loading = true;
    this._hraService.addHRA(this._groupId, payload)
      .pipe(first())
      .subscribe(
        (): void => {

          this._router.navigate(['/hra-form/thankyou']);
          this.hraForm.reset();
        },
        error => {
          this._alertService.error(error.error);
          this.loading = false;
        });
  }
}
