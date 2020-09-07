import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import * as moment from 'moment';
import { HRAService } from 'src/app/services/hra.service';
import { GroupService } from 'src/app/services/group.service';
import { AlertService } from 'src/app/services/AlertService.service';
import { HRADetails } from 'src/app/models/hraDetails';

@Component({
  templateUrl: 'hra.component.html',
  styleUrls: ['./hra.component.scss']
})
export class HRAFormComponent implements OnInit {
  hraForm: FormGroup;
  validLink = false;
  loading = false;
  submitted = false;
  currentDiagnosisQuestions = [
    { key: 'HighBloodPressure', label: 'High Blood Pressure' },
    { key: 'ElevatedCholesterol', label: 'Elevated Cholesterol' },
    { key: 'CognitiveHeartFailure', label: 'Cognitive Heart Failure' },
    { key: 'ThyroidDisorder', label: 'Thyroid Disorder' },
    { key: 'Anxiety', label: 'Anxiety' },
    { key: 'Depression', label: 'Depression' },
    { key: 'Asthma', label: 'Asthma' },
    { key: 'Emphysema', label: 'Emphysema' },
    { key: 'SeasonalAllergies', label: 'Seasonal Allergies' },
    { key: 'AcidRefluxOrHeatBurn', label: 'Acid Reflux or HeatBurn' },
    { key: 'Arthritis', label: 'Arthritis' },
    { key: 'Headaches', label: 'Headaches' },
    { key: 'Constipation', label: 'Constipation' },
    { key: 'Diarrhea', label: 'Diarrhea' },
    { key: 'TobaccoCessation', label: 'Tobacco Cessation' },
    { key: 'Diabetes', label: 'Diabetes' },
    { key: 'Cancer', label: 'Cancer' },
    { key: 'HeartDisease', label: 'Heart Disease' },
    { key: 'PhysicalActivityGuidance', label: 'Physical Activity Guidance' },
    { key: 'NutritionalGuidance', label: 'Nutritional Guidance' },
    { key: 'StressManagementGuidence', label: 'Stress Management Guidence' }
  ];

  healthQuestions = [
    { key: 'UseTobaccoProducts', label: 'Do you use tobacco products?' },
    { key: 'DesireToStopTobaccoUse', label: 'Do you desire to stop tobacco use?' },
    { key: 'DrinkAlcoholicBreverages', label: 'Do you drink alcoholic breverages?' },
    { key: 'DesignatedCaregiverForALovedOne', label: 'Are you a designated caregiver for a loved one?' },
    { key: 'ExperienceFrequentSeverePain', label: 'Do you experience frequent severe pain?' },
    { key: 'ConsumeAtleast5GlassOfWaterDaily', label: 'Do you consume at least 5 glass of water daily?' },
    { key: 'ExcessivelyThirsty', label: 'Are you excessively thirsty?' },
    { key: 'DesireChangeInYourBodyWeight', label: 'Do you desire a change in your body weight?' },
    { key: 'DesireChangeInYourDailyDiet', label: 'Do you desire a change in your daily diet?' },
    { key: 'ExperiencedDeathOrTraumaticLossInLast2Years', label: 'Have you experienced a death or traumatic loss in the last two years?' },
    { key: 'InterestedInIncreasingPhysicalActivity', label: 'Are you interested in increasing your physical activity?' },
    { key: 'DiagnosedWithIrritableBowelSyndrome?', label: 'Have you been diagnosed with irritable Bowel Syndrome?' },
    { key: 'DiagnosedOrTreatedWithSleepApnea', label: 'Have you been diagnosed or treated with sleep apnea?' },
    { key: 'ExperienceChestPainOrTightness', label: 'Do you ever experience chest pain or tightness?' },
    { key: 'FeelLikeYourHeartIsRacingOrBeatingStrangely', label: 'Do you ever feel like your heart is racing or beating strangely?' },
    { key: 'ExperienceUnusalShortnessOfBreath', label: 'Do you ever experience unusal shortness of breath?' },
    { key: 'ExperiencedHighBloodPressure', label: 'Have you ever experienced high blood pressure?' },
    { key: 'Experiencewheezing', label: 'Do you ever experience wheezing?' },
    { key: 'ExperiencedUnexplainedWeightLoss', label: 'Have you ever experienced unexplained weight loss?' },
    { key: 'SufferFromFrequentInfections', label: 'Do you suffer from frequent infections?' },
    { key: 'ExperienceNumbnessTinglingInHandsOrFeet', label: 'Do you ever experience numbness and/or tingling in your hands or feet?' },
    { key: 'SufferFromUnexplainedBlurredVision', label: 'Do you suffer from unexplained blurred vision?' },
    { key: 'SleepWellAtNight', label: 'Do you sleep well at night?' },
    { key: 'FrequentlyIrritableSadOrAnxious', label: 'Are you frequently Irritable, Sad, or anxious?' },
    { key: 'RoutinelyWearYourSeatbelt', label: 'Do you routinely wear your seatbelt?' },
    { key: 'UseYourPhonWhileOperatingAMotorVehicle', label: 'Do you use your phone while operating a motor vehicle?' }
  ];

  private _groupId: string;
  private _groupName: string;

  constructor(
    private _formBuilder: FormBuilder,
    private _hraService: HRAService,
    private _groupService: GroupService,
    private _alertService: AlertService,
    private _route: ActivatedRoute,
  ) {
    this._route.params.subscribe(params => { this._groupId = params['groupId']; });
  }

  ngOnInit() {
    this._alertService.clear();
    this._groupService.getGroupName(this._groupId).subscribe(groupName => {
      this._groupName = groupName.value;
      this.hraForm.controls['groupName'].patchValue(this._groupName);
      this.validLink = true;
    }, (error) => {
      console.log(error);
      this._alertService.error('Invalid Link');
      this.validLink = false;
    });

    this.hraForm = this._formBuilder.group({
      date: [moment(new Date()).format('MM/dd/YYYY'), [Validators.required]],
      age: [21, Validators.required],
      language: ['', Validators.required],
      firstName: ['', Validators.required],
      middleName: ['', Validators.required],
      lastName: ['', Validators.required],
      ssn: ['', Validators.required],
      groupName: [''],
      department: ['', Validators.required],
      position: ['', Validators.required],
      gender: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      alternateEmail: ['', [Validators.required, Validators.email]],
      mailingAddress: ['', Validators.required],
      apartmentNumber: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zipCode: ['', Validators.required],
      dob: ['', Validators.required],
      otherDiagnosis: ['', Validators.required],
      textMessageConfirmation: ['', Validators.required],
      contactMethodPreference: ['', Validators.required],
      cellPhone: ['', Validators.required],
      alternateCellPhone: ['', Validators.required],
      bestTimeToCall: ['', Validators.required],
      heightInFeet: ['', Validators.required],
      heightInInches: ['', Validators.required],
      weightInLbs: ['', Validators.required],
      bmi: ['', Validators.required],
      mostRecentCheckupDate: ['', Validators.required],
      comments: ['']
    });

    const _this = this;
    this.currentDiagnosisQuestions.forEach(function (que) {
      _this.hraForm.addControl(que.key, new FormControl('Yes', Validators.required));
    });

    this.healthQuestions.forEach(function (que) {
      _this.hraForm.addControl(que.key, new FormControl('No', Validators.required));
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.hraForm.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this._alertService.clear();

    // stop here if form is invalid
    if (this.hraForm.invalid) {
      return;
    }

    this.loading = true;
    this._hraService.addHRA(this._buildHraContract())
      .pipe(first())
      .subscribe(
        (): void => {
          this._alertService.success('HRA Form submitted successfully.');
          this.hraForm.reset();
        },
        error => {
          this._alertService.error(error.error);
          this.loading = false;
        });
  }

  private _buildHraContract(): HRADetails {
    const _this = this;
    let hraDetails = new HRADetails();
    hraDetails = this.hraForm.getRawValue();
    hraDetails.groupId = this._groupId;
    hraDetails.healthQuestions = [];
    hraDetails.currentDiagnosis = [];
    hraDetails.country = 'US';
    const keys = Object.keys(this.hraForm.getRawValue());
    keys.forEach(function (key) {
      if (_this._isKeyExists(key, false)) {
        delete hraDetails[key];
        hraDetails.currentDiagnosis.push({ key: key, question: _this._getLabel(key, false), answer: _this.hraForm.value[key] });
      }

      if (_this._isKeyExists(key, true)) {
        delete hraDetails[key];
        hraDetails.healthQuestions.push({ key: key, question: _this._getLabel(key, true), answer: _this.hraForm.value[key] });
      }
    });

    return hraDetails;
  }

  private _getLabel(key: string, isHealthQuestion: boolean): string {
    const que = this._getQuestion(key, isHealthQuestion);
    if (!!que) {
      return que.label;
    }

    return null;
  }

  private _isKeyExists(key: string, isHealthQuestion: boolean): boolean {
    const que = this._getQuestion(key, isHealthQuestion);
    return !!que ? true : false;
  }

  private _getQuestion(key: string, isHealthQuestion: boolean): any {
    return isHealthQuestion
      ? this.healthQuestions.find(x => x.key === key)
      : this.currentDiagnosisQuestions.find(x => x.key === key);
  }
}
