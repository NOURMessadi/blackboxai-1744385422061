import { Component } from '@angular/core';
import { ApplicationDto } from '../../core/models/application.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidationService } from '../../core/services/validation.service';
import { ApplicationService } from '../../core/services/application.service';
import { finalize } from 'rxjs/operators';
import { LoadingService } from '../../core/services/loading.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-sample-form',
  templateUrl: './sample-form.component.html',
  styleUrls: ['./sample-form.component.scss']
})
export class SampleFormComponent {
  form: FormGroup;
  submitted = false;
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private validationService: ValidationService,
    private applicationService: ApplicationService,
    private loadingService: LoadingService,
    private snackBar: MatSnackBar
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email]],
      description: ['', [Validators.maxLength(500)]]
    });
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loadingService.show();
    this.isLoading = true;

    const formData = this.form.value;
    
    const applicationDto: ApplicationDto = {
      id: 0, // Will be assigned by backend
      name: formData.name,
      description: formData.description,
      posteId: undefined // Optional field
    };
    this.applicationService.create(applicationDto)
    .pipe(finalize(() => {
      this.loadingService.hide();
      this.isLoading = false;
    }))
    .subscribe({
      next: () => {
        this.showSuccess('Form submitted successfully!');
        this.form.reset();
        this.submitted = false;
      },
      error: () => this.showError('Failed to submit form')
    });
  }

  getErrorMessage(fieldName: string, control: any): string {
    if (control && control.errors) {
      return this.validationService.getErrorMessage(fieldName, control.errors);
    }
    return '';
  }

  private showSuccess(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000
    });
  }

  private showError(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 5000,
      panelClass: ['error-snackbar']
    });
  }
}