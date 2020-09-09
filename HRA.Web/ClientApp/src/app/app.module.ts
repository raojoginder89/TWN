import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './shared/controls/nav-menu/nav-menu.component';
import { AlertComponent } from './shared/controls/alert/alert.component';
import { HomeComponent } from './controls/home/home.component';
import { LoginComponent } from './controls/login/login.component';
import { RegisterComponent } from './controls/register/register.component';
import { ChangePasswordComponent } from './controls/change-password/change-password.component';
import { ForgotPasswordComponent } from './controls/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './controls/reset-password/reset-password.component';
import { VimeoPlayerComponent } from './controls/vimeo-player/vimeo-player.component';
import { HRAFormComponent } from './controls/hra/hra.component';
import { GroupListComponent } from './controls/groups/group-list/group-list.component';
import { AddEditGroupComponent } from './controls/groups/add-group/add-edit.component';
import { VimeoUrlPipe } from './shared/pipes/vimeo-url.pipe';
import { DynamicFormComponent } from './data-driven/dynamic-form/dynamic-form.component';
import { DynamicFormBuilderComponent } from './data-driven/dynamic-form-builder/dynamic-form-builder.component';
import { UpdateGroupComponent } from './controls/groups/update-group/update-group.component';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { appRoutingModule } from './app.routing';
import { AgGridModule } from 'ag-grid-angular';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { VimeoService } from './services/vimeo.service';
import { DynamicControlService } from './data-driven/dynamic-control.Service';
import { QuestionService } from './data-driven/question.service';
import { UpdateVideoComponent } from './controls/videos/update-video/update-video.component';
import { VideoListComponent } from './controls/videos/video-list/video-list.component';
import { AddEditVideoComponent } from './controls/videos/add-video/add-edit.component';
import { BtnCellRendererComponent } from './controls/renderers/button-renderer/btn.renderer';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './shared/controls/modal/modal.component';
import { CommonModule } from '@angular/common';
import { SharedLayoutModule } from './shared/controls/layout/shared-layout.module';
import { SharedInputIconModule } from './shared/controls/input-icon';
import { FooterControl } from './shared/controls/layout/footer/footer.ctrl';
import { ThankYouComponent } from './controls/ThankYou/thankyou.component';
import { WelcomeComponent } from './controls/welcome/welcome.component';
// import { SharedRadioGroupModule } from './shared/controls/radio-group/shared-radio-group.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AlertComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ChangePasswordComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    VimeoPlayerComponent,
    HRAFormComponent,
    GroupListComponent,
    AddEditGroupComponent,
    VimeoUrlPipe,
    DynamicFormComponent,
    DynamicFormBuilderComponent,
    BtnCellRendererComponent,
    UpdateGroupComponent,
    UpdateVideoComponent,
    VideoListComponent,
    AddEditVideoComponent,
    ModalComponent,
    FooterControl,
    ThankYouComponent,
    WelcomeComponent
  ],
  entryComponents: [
    ModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    CommonModule,
    appRoutingModule,
    SharedLayoutModule,
    SharedInputIconModule,
    // SharedRadioGroupModule,
    AgGridModule.withComponents([BtnCellRendererComponent])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    VimeoService,
    DynamicControlService,
    QuestionService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
