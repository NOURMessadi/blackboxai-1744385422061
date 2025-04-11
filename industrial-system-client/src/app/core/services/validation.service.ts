import { Injectable } from '@angular/core';

export interface ValidationError {
  propertyName: string;
  errorMessage: string;
}

@Injectable({
  providedIn: 'root'
})
export class ValidationService {
  private readonly errorMessages = new Map<string, string>([
    ['required', 'Ce champ est requis'],
    ['email', 'Format d\'email invalide'],
    ['maxlength', 'La longueur maximale est dépassée'],
    ['pattern', 'Format invalide'],
    ['min', 'La valeur minimale n\'est pas respectée'],
    ['max', 'La valeur maximale est dépassée']
  ]);

  handleValidationErrors(errors: any[]): ValidationError[] {
    if (!errors) return [];

    return errors.map(error => ({
      propertyName: error.propertyName,
      errorMessage: error.errorMessage
    }));
  }

  getErrorMessage(fieldName: string, error: any): string {
    if (!error) return '';

    // Handle FluentValidation errors from backend
    if (typeof error === 'string') {
      return error;
    }

    // Handle Angular form validation errors
    const errorType = Object.keys(error)[0];
    const defaultMessage = this.errorMessages.get(errorType) || 'Champ invalide';

    switch (errorType) {
      case 'maxlength':
        return `${fieldName} ne peut pas dépasser ${error.maxlength.requiredLength} caractères`;
      case 'minlength':
        return `${fieldName} doit avoir au moins ${error.minlength.requiredLength} caractères`;
      case 'min':
        return `${fieldName} doit être supérieur ou égal à ${error.min.min}`;
      case 'max':
        return `${fieldName} doit être inférieur ou égal à ${error.max.max}`;
      default:
        return defaultMessage;
    }
  }
}