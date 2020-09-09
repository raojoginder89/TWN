import { Injectable } from '@angular/core';

import { of } from 'rxjs';
import { DynamicControlBase } from './dynamic-control.base';
import { Validators } from '@angular/forms';

@Injectable()
export class QuestionService {

    private sectionClassName = 'section';
    private subSectionClassName = 'sub-section';

    private yesNoOptions = [{ key: 'Yes', value: 'Yes' }, { key: 'No', value: 'No' }];

    private ratingOptions = [{ key: '0', value: '0' }, { key: '1', value: '1' }, { key: '2', value: '2' }, { key: '3', value: '3' }];

    private agreeDisAgreeOptions = [{ key: 'Agree', value: 'Agree' }, { key: 'Disagree', value: 'Disagree' }];

    private compareOptions = [{ key: 'less than', value: 'less than' }, { key: 'more than', value: 'more than' }, { key: 'the same amount', value: 'the same amount' }];

    private dayBasisOptions = [{ key: 'Never', value: 'Never' }, { key: 'Sometimes', value: 'Sometimes' }, { key: 'Often', value: 'Often' }, { key: 'Very often', value: 'Very often' }, { key: 'Every day', value: 'Every day' }];

    private stressOptions = [{ key: 'Not Very Likely', value: 'Not Very Likely' }, { key: 'Somewhat Likely', value: 'Somewhat Likely' }, { key: 'Likely', value: 'Likely' }, { key: 'Somewhat Unlikely', value: 'Somewhat Unlikely' }, { key: 'Very  Likely', value: 'Very  Likely' }];

    private stressManagementOptions = [{ key: 'Not difficult at all', value: 'Not difficult at all' }, { key: 'Somewhat difficult', value: 'Somewhat difficult' }, { key: 'Very difficult', value: 'Very difficult' }, { key: 'Extremely difficult', value: 'Extremely difficult' }];

    private motivatedOptions = [{ key: 'Not motivated', value: 'Not motivated' }, { key: 'Somewhat Motivated', value: 'Somewhat Motivated' }, { key: 'Motivated', value: 'Motivated' }, { key: 'Very Motivated', value: 'Very Motivated' }];

    private barrierOptions = [{ key: 'Not a barrier at all', value: 'Not a barrier at all' }, { key: 'A barrier somewhat', value: 'A barrier somewhat' }, { key: 'Tends to be a barrier', value: 'Tends to be a barrierYes' }, { key: 'Very much a barrier', value: 'Very much a barrier' }, { key: 'The largest barrier', value: 'The largest barrier' }];

    // TODO: get from a remote source of question metadata
    getQuestions() {
        const demographicCharacteristics: DynamicControlBase<any>[] = [

            new DynamicControlBase<string>('textbox', 'firstName', '', 'First Name',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1, null, false, 'col-md-4'),

            new DynamicControlBase<string>('textbox', 'middleName', '', 'Middle Inital',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1, null, false, 'col-md-4'),

            new DynamicControlBase<string>('textbox', 'lastName', '', 'Last Name',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1, null, false, 'col-md-4'),

            new DynamicControlBase<string>('number', 'age', '', 'Age',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1, null, false, 'col-md-4'),

            new DynamicControlBase<string>('textbox', 'gender', '', 'Gender',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1, null, false, 'col-md-4'),

            new DynamicControlBase<string>('textbox', 'sex', '', 'Sex',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1, null, false, 'col-md-4'),

            new DynamicControlBase<string>('textbox', 'ethnicity', '', 'Ethnicity',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1, null, false, 'col-md-4'),

            new DynamicControlBase<string>('textbox', 'ssn', '', 'Employee Id',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1, null, false, 'col-md-4')
        ];

        const medicalHealthCare = [
            new DynamicControlBase<string>('textarea', 'previousHealthConcerns', '', 'Previous health concerns',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, false),

            new DynamicControlBase<string>('textarea', 'currentHealthConcerns', '', 'Current health concerns',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, false),

            new DynamicControlBase<string>('textarea', 'healthConcerns', '', 'Health concern(s) I struggle to manage/would like to better manage in the next 6 months',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, false),

            new DynamicControlBase<string>('radio', 'visitAnnually', '', 'I see my physician for annual wellness and/or check-up visits',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'visitOnSymptoms', '', 'I only see my physician only if I have symptoms',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'followRecommendations', '', 'I tend to follow their recommendations',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'interestedInPHC', '', 'I would be interested in learning more about preventative medical healthcare',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),
        ];

        const excercise = [
            new DynamicControlBase<string>('radio', 'physicalActivityLevel', '', 'Which is most true about your level of physical activity:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.dayBasisOptions, true),

            new DynamicControlBase<string>('', '', '', 'In the next 6 months:', null, 1, null, false, null, this.subSectionClassName),

            new DynamicControlBase<string>('radio', 'excerciseMoreFrequently', '', 'I would like to exercise more frequently:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'excerciseMoreConsistently', '', 'I would like to exercise more consistently:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'excerciseSupportPromotion', '', 'Would you like support promoting exercise into your daily life?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true)
        ];

        const waterHabits = [
            new DynamicControlBase<string>('radio', 'drinkEnoughWater', '', 'I drink enough water:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.agreeDisAgreeOptions, true),

            new DynamicControlBase<string>('radio', 'drinkWaterDay', '', 'I drink water throughout the day:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.agreeDisAgreeOptions, true),

            new DynamicControlBase<string>('radio', 'drinkOtherBeverages', '', 'I drink other beverages ',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.compareOptions, true)
        ];

        const caffeine = [
            new DynamicControlBase<string>('radio', 'caffeine8', '', 'I consume approximately 8 oz. of coffee/caffeine in the morning',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'caffeine24', '', 'I consume approximately 16-24 oz. of coffee/caffeine in the morning',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'caffeineAfter2', '', 'I consume caffeine after 2 pm',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'noCaffeine', '', 'I do not consume caffeine ',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true)
        ];

        const eatingHabits = [
            new DynamicControlBase<string>('radio', 'balancedDiet', '', 'I eat a healthy and well-balanced diet:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.dayBasisOptions, true),

            new DynamicControlBase<string>('', '', '', 'I would eat healthier if:', null, 1, null, false, null, this.subSectionClassName),

            new DynamicControlBase<string>('radio', 'couldAfford', '', 'I could afford to',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.agreeDisAgreeOptions, true),

            new DynamicControlBase<string>('radio', 'knewHealthyFoods', '', 'If I knew more about what foods are considered healthy for me',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.agreeDisAgreeOptions, true),

            new DynamicControlBase<string>('radio', 'knowPrepareHealthymeals', '', 'If I knew how to prepare healthy meals',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.agreeDisAgreeOptions, true),

            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'waterHabits', waterHabits, 'Water:', null, 1, null, false, null, this.subSectionClassName),

            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'caffeine', caffeine, 'Caffeine:', null, 1, null, false, null, this.subSectionClassName),

            new DynamicControlBase<string>('radio', 'drinkOtherBeverages', '', 'I would like to learn more about how I can improve my eating and other dietary habits ',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),
        ];


        const stress = [
            new DynamicControlBase<string>('radio', 'feelStressedOut', '', 'I know when I feel stressed out ',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.stressOptions, true),

            new DynamicControlBase<string>('radio', 'knowMyStressors', '', 'I know what my stressors are ',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.stressOptions, true),

            new DynamicControlBase<string>('radio', 'knowHowToRelaxMyMind', '', 'I know what to do to relax my mind and body when I feel stressed out',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.stressOptions, true),

            new DynamicControlBase<string>('radio', 'learnNewWaysToCopeWithStress', '', 'I would like to learn new ways to cope with stress and relax ',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true)
        ];

        const smoking = [
            new DynamicControlBase<string>('radio', 'useTobacco', '', 'I currently use tobacco',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'smokeCigarettes', '', 'I smoke cigarettes',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('textbox', 'otherTobaccoProducts', '', 'Other tobacco products',
                null, 1, null, true),

            new DynamicControlBase<string>('radio', 'attemptToQuitSmoking', '', 'Number of times I have attempted to quit: ',
                null, 1,
                [{ key: '0-5', value: '0-5' },
                { key: '5-10', value: '5-10' },
                { key: '5-20', value: '5-20' },
                { key: 'more times than I can count', value: 'more times than I can count' }], true),

            new DynamicControlBase<string>('checkbox', 'triedWaysForQuitSmoking', '', 'I have tried:  ', null, 1,
                [{ key: 'Cold turkey', value: 'Cold turkey' },
                { key: 'Gum', value: 'Gum' },
                { key: 'The patch', value: 'The patch' },
                { key: 'Rxn medication (e.g. Chantix, Bupropion)', value: 'Rxn medication (e.g. Chantix, Bupropion)' },
                { key: 'Support group', value: 'Support group' },
                { key: 'Behavioral health specialist', value: 'Behavioral health specialist' }], false),

            new DynamicControlBase<string>('radio', 'motivatedToQuitSmoking', '', 'I am motivated to quit smoking',
                null, 1,
                this.yesNoOptions, true),
        ];

        const substanceUse = [
            new DynamicControlBase<string>('radio', 'alcoholicDrinksPerDay', '', 'How many standard drinks containing alcohol do you have on a typical day when drinking?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                [{ key: '0', value: '0' },
                { key: '1-2', value: '1-2' },
                { key: '3-4', value: '3-4' },
                { key: '5-6', value: '5-6' },
                { key: '7-9', value: '7-9' },
                { key: '10 or more', value: '10 or more' }], true),

            new DynamicControlBase<string>('radio', 'alcoholicDrinksFrequency', '', 'How often do you have a drink containing alcohol?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                [{ key: 'Never', value: 'Never' },
                { key: 'Monthly or less', value: 'Monthly or less' },
                { key: '2-4 times a month', value: '2-4 times a month' },
                { key: '2-3 times a week', value: '2-3 times a week' },
                { key: '4 or more times a week', value: '4 or more times a week' }], true),

            new DynamicControlBase<string>('radio', 'concernAboutConsumption', '', 'Do you have any concerns about your current use of alcohol or other substances or concerns regarding a member of your family?  ',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.stressOptions, true),

            new DynamicControlBase<string>('radio', 'feelGuiltyAfterDrink', '', 'Do you ever feel guilty or remorseful about drinking or using other substances?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'wantToKnowImpact', '', 'Would you be interested in finding out more information about how the use of alcohol or other substances can impact your daily life?  ',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'smoking', smoking, 'Smoking:', null, 1, null, false, null, this.subSectionClassName)
        ];

        const sleep = [
            new DynamicControlBase<string>('radio', 'troubleFallingAsleep', '', 'Do you routinely have trouble falling or staying asleep at your regular bedtime?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'troubleFallingAsleepInDay', '', 'Do you ever have trouble with sleepiness during the daytime that interferes with your daily activities?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'sleepApnea', '', 'Have you ever been diagnosed with Sleep Apnea?  If so, do you wear a C-PAP machine every night?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'sitttingAndReading', '', 'Sitting and Reading:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.ratingOptions, true),

            new DynamicControlBase<string>('radio', 'watchingTv', '', 'Watching TV:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.ratingOptions, true),

            new DynamicControlBase<string>('radio', 'sittingInactive', '', 'Sitting inactive in a public place (a theater or a meeting)',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.ratingOptions, true),

            new DynamicControlBase<string>('radio', 'lyingDown', '', 'Lying down to rest in the afternoon (when circumstances permit)',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.ratingOptions, true),

            new DynamicControlBase<string>('radio', 'sittingAndTalking', '', 'Sitting and talking to someone',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.ratingOptions, true),

            new DynamicControlBase<string>('radio', 'sittingQuietlyAfterLunch', '', 'Sitting quietly after lunch without alcohol',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.ratingOptions, true),

            new DynamicControlBase<string>('radio', 'inStoppedCar', '', 'In a car, while stopped for a few minutes during traffic',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.ratingOptions, true),

            new DynamicControlBase<string>('radio', 'likeStrategiesToImproveSleep', '', 'Would you like strategies to help you improve your sleep quality and daytime alertness?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),
        ];

        const stressManagement = [
            new DynamicControlBase<string>('radio', 'concernsAbourStressManagement', '', 'How difficult do concerns regarding nervousness or anxiety, make it for you to do your work, take care of things at home, or get along with other people?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.stressManagementOptions, true),

            new DynamicControlBase<string>('radio', 'addStressManagementServices', '', 'Would you like information or additional tools or services related to relaxation or other techniques to promote concentration and stress management during the day?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

        ];

        const financialHealth = [
            new DynamicControlBase<string>('radio', 'financeStress', '', 'Finances stress me out:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'financeBuyUnafforableThings', '', 'I sometimes buy things I can’t afford:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'financeFeelRegretFulafterBuy', '', 'I sometimes buy things and feel regretful afterwards:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'financesBuyWithoutThinking', '', 'I sometimes buy things without thinking it through:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'financeDebt', '', 'The amount of debt I am in weighs on me daily:',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),
        ];

        const socialSupport = [
            new DynamicControlBase<string>('radio', 'havingAddSupport', '', 'Having additional social support would help me improve my health goals?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'moreSupportFromfriends', '', 'Having more support from my friends and family would reduce my stress?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true),

            new DynamicControlBase<string>('radio', 'adequateSupport', '', 'I  have an adequate support system?',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.yesNoOptions, true)
        ];

        const motivatedRatings = [
            new DynamicControlBase<string>('radio', 'physicalHealth', '', 'Physical Health',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.motivatedOptions, true),

            new DynamicControlBase<string>('radio', 'habits', '', 'Exercise/Eating/Water habits',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.motivatedOptions, true),

            new DynamicControlBase<string>('radio', 'stressManagement', '', 'Stress management/Anxiety/mood',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.motivatedOptions, true),

            new DynamicControlBase<string>('radio', 'substanceTobaccoUse', '', 'Substance use/tobacco',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.motivatedOptions, true),

            new DynamicControlBase<string>('radio', 'sleep', '', 'Sleep',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.motivatedOptions, true),

            new DynamicControlBase<string>('radio', 'relationships', '', 'Relationships',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.motivatedOptions, true),

            new DynamicControlBase<string>('radio', 'financialHealth', '', 'Financial health',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.motivatedOptions, true),

            new DynamicControlBase<string>('textbox', 'other', '', 'I  have an adequate support system?',
                null, 1, null, true)
        ];

        const barrierRatings = [
            new DynamicControlBase<string>('radio', 'Stress', '', 'Stress/anxiety/mood',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.barrierOptions, true),

            new DynamicControlBase<string>('radio', 'timeManagement', '', 'Time management',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.barrierOptions, true),

            new DynamicControlBase<string>('radio', 'barrierInSleep', '', 'Sleep',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.barrierOptions, true),

            new DynamicControlBase<string>('radio', 'dietaryEatingHabits', '', 'Dietary and other eating habits',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.barrierOptions, true),

            new DynamicControlBase<string>('radio', 'familyObligations', '', 'Family obligations',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.barrierOptions, true),

            new DynamicControlBase<string>('radio', 'religiousReasons', '', 'Religious/spiritual reasons ',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.barrierOptions, true),

            new DynamicControlBase<string>('radio', 'lackOfMotivation', '', 'Lack of motivation',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.barrierOptions, true),

            new DynamicControlBase<string>('radio', 'financial', '', 'Financial',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.barrierOptions, true),

            new DynamicControlBase<string>('radio', 'fatigue', '', 'atigue/lack of energy',
                [{ validator: Validators.required, errorMessage: 'This field is required' }], 1,
                this.barrierOptions, true),

            new DynamicControlBase<string>('textbox', 'other', '', 'I  have an adequate support system?',
                null, 1, null, true)
        ];

        const ratings = [
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'motivatedRatings', motivatedRatings, 'Please rank each domain you are motivated to improve in the next 6 months to improve your wellness: ', null, 1, null, false, null, this.subSectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'barrierRatings', barrierRatings, 'Please rank the following barriers to achieving your health & wellness goals in the next 6 months:', null, 1, null, false, null, this.subSectionClassName),
        ];

        const questions: DynamicControlBase<any>[] = [
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'demographicCharacteristics', demographicCharacteristics, 'Demographic Characteristics:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'medicalHealthCare', medicalHealthCare, 'Medical HealthCare:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'excercise', excercise, 'Excercise:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'eatingHabits', eatingHabits, 'Eating Habits:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'stress', stress, 'Stress:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'substanceUse', substanceUse, 'Substance Use:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'sleep', sleep, 'Sleep:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'stressManagement', stressManagement, 'Stress Management:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'financialHealth', financialHealth, 'Financial Health:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'socialSupport', socialSupport, 'Social Support:', null, 1, null, false, null, this.sectionClassName),
            new DynamicControlBase<DynamicControlBase<any>[]>('array', 'ratings', ratings, 'Based on the above questions:', null, 1, null, false, null, this.sectionClassName),
        ];

        return of(questions.sort((a, b) => a.order - b.order));
    }
}
