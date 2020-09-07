import { Questions } from './questions';

export class HRADetails {
  groupId: string;
  firstName: string;
  lastName: string;
  middleInitial: string;
  date: Date;
  dob: Date;
  ssn: string;
  language: string;
  department: string;
  position: string;
  gender: string;
  email: string;
  alternateEmail: string;
  apartmentNumber: string;
  mailingAddress: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
  currentDiagnosis: Questions[];
  otherDiagnosis: string;
  textMessageConfirmation: boolean;
  contactMethodPreference: string;
  cellPhone: string;
  alternateCellPhone: string;
  bestTimeToCall: string;
  heightInInches: 0;
  heightInFeet: 0;
  weightInLbs: 0;
  bmi: 0;
  mostRecentCheckupDate: Date;
  healthQuestions: Questions[];
  comments: string;
}
