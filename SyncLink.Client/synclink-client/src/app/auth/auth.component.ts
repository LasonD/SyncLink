import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from "@ngrx/store";
import { LoginStart, SignupStart } from "./store/auth.actions";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  mode: 'signin' | 'signup' = 'signin';
  registrationForm: FormGroup;

  constructor(private fb: FormBuilder,
              private store: Store) {
  }

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: '',
      password: ['', Validators.required]
    });
  }

  toggleMode(): void {
    this.mode = this.mode === 'signin' ? 'signup' : 'signin'
  }

  onSubmit(): void {
    console.log(this.registrationForm.value);

    if (this.mode === 'signin') {
      this.store.dispatch(new LoginStart(this.registrationForm.value))
    } else {
      this.store.dispatch(new SignupStart(this.registrationForm.value))
    }
  }
}
