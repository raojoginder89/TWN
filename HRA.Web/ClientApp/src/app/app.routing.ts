import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './controls/home/home.component';
import { AuthGuard } from './helpers/auth.guard';
import { LoginComponent } from './controls/login/login.component';
import { RegisterComponent } from './controls/register/register.component';
import { ChangePasswordComponent } from './controls/change-password/change-password.component';
import { ForgotPasswordComponent } from './controls/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './controls/reset-password/reset-password.component';
import { HRAFormComponent } from './controls/hra/hra.component';
import { GroupListComponent } from './controls/groups/group-list/group-list.component';
import { AdminAuthGuard } from './helpers/auth-admin.gaurd';
import { UpdateGroupComponent } from './controls/groups/update-group/update-group.component';
import { UpdateVideoComponent } from './controls/videos/update-video/update-video.component';
import { VideoListComponent } from './controls/videos/video-list/video-list.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard], pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'change-password', component: ChangePasswordComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password/:token', component: ResetPasswordComponent },
  { path: ':groupId/hra-form', component: HRAFormComponent },
  { path: 'groups', component: GroupListComponent , canActivate: [AdminAuthGuard]},
  { path: 'groups/:groupId', component: UpdateGroupComponent , canActivate: [AdminAuthGuard]},
  { path: 'videos', component: VideoListComponent , canActivate: [AdminAuthGuard]},
  { path: 'videos/:videoId', component: UpdateVideoComponent , canActivate: [AdminAuthGuard]},
  { path: '**', redirectTo: '' } // otherwise redirect to home
];

export const appRoutingModule = RouterModule.forRoot(routes);
