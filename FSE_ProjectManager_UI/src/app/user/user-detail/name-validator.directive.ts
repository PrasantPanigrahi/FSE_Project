import { Directive } from '@angular/core';
import { Validator, FormGroup, NG_VALIDATORS, FormControl } from '@angular/forms';

@Directive({
  selector: '[appValidateName]',
  providers: [{ provide: NG_VALIDATORS, useExisting: NameValidatorDirective, multi: true }]
})
export class NameValidatorDirective implements Validator {
  validate(formControl: FormControl): { [key: string]: any } {
    const firstName = formControl.parent.controls['firstName'].value;
    const lastName = formControl.parent.controls['lastName'].value;
    if ((firstName) || (lastName)) {
      return null;
    } else {
      return { appValidateName: false };
    }
  }
}