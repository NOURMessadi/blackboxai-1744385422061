import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './shared/material.module';
import { LoadingSpinnerComponent } from './shared/components/loading-spinner/loading-spinner.component';
import { SampleFormComponent } from './components/sample-form/sample-form.component';
import { ValidationService } from './core/services/validation.service';

@NgModule({
  declarations: [
    AppComponent,
    LoadingSpinnerComponent,
    SampleFormComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  providers: [ValidationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
