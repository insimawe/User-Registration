import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { inject } from '@angular/core';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './registration.component.html',
  styles: ``
})
export class RegistrationComponent {
  constructor(){ }
 private formBuilder = inject(FormBuilder);

  form = this.formBuilder.group({
    fullName: [''],
    email: [''],
    password: [''],
    confirmPassword: [''],
  })

  onSubmit() {
    console.log(this.form.value);
  }
}
