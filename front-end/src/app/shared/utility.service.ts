import { Injectable } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {
  trimInputValue(control: FormControl<string | null>): void {
    const value = control.value;

    if (value?.length) {
      control.setValue(value.trim());
    }
  }

  markAllControlsAsTouched(form: FormGroup): void {
    Object.values(form.controls).forEach(control => {
      if (control instanceof FormGroup) {
        this.markAllControlsAsTouched(control);
      }
      else {
        control?.markAsTouched();
      }
    });
  }

  isPositiveInteger(text: string): boolean {
    const num = Number(text);
    return Number.isInteger(num) && num > 0;
  }
}
