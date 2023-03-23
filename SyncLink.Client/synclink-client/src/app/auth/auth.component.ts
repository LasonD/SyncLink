import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from "@ngrx/store";
import { ClearErrors, LoginStart, SignupStart } from "./store/auth.actions";
import { distinctUntilChanged, Observable, Subject, takeUntil } from "rxjs";
import { map } from "rxjs/operators";
import { AppState } from "../store/app.reducer";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  mode: 'signin' | 'signup' = 'signin';
  registrationForm: FormGroup;

  public isLoading$: Observable<boolean>;
  public authErrors$: Observable<Array<string>>;

  private destroyed$: Subject<boolean> = new Subject<boolean>();

  constructor(private fb: FormBuilder,
              private store: Store<AppState>) {
  }

  ngOnInit(): void {
    this.authErrors$ = this.store.select('auth')
      .pipe(
        takeUntil(this.destroyed$),
        map(state => state.authErrorMessages),
        distinctUntilChanged(),
      );

    this.store.select('auth')
      .subscribe(state => {
        console.log('State changed: ', state);
      });

    this.isLoading$ = this.store.select('auth')
      .pipe(
        takeUntil(this.destroyed$),
        map(state => state.isLoading),
      );

    this.buildForm();
  }

  buildForm(): void {
    if (this.mode === 'signin') {
      this.registrationForm = this.fb.group({
        usernameOrEmail: ['', Validators.required],
        password: ['', Validators.required]
      });
    } else {
      this.registrationForm = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        username: '',
        password: ['', Validators.required]
      });
    }
  }

  toggleMode(): void {
    this.mode = this.mode === 'signin' ? 'signup' : 'signin';
    this.buildForm();
  }

  onSubmit(): void {
    console.log(this.registrationForm.value);

    if (this.mode === 'signin') {
      this.store.dispatch(new LoginStart(this.registrationForm.value));
    } else {
      this.store.dispatch(new SignupStart(this.registrationForm.value));
    }
  }

  onClearErrors() {
    this.store.dispatch(new ClearErrors());
  }
}
