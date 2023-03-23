import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from "@ngrx/store";
import { LoginStart, SignupStart } from "./store/auth.actions";
import { AppState } from "../store/app.reducer";
import { distinctUntilChanged, filter, Observable, Subject, takeUntil } from "rxjs";
import { map } from "rxjs/operators";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit, OnDestroy {
  private emailControl = ['', [Validators.required, Validators.email]];
  private usernameOrEmailControl = ['', [Validators.required]];

  public mode: 'signin' | 'signup' = 'signin';
  public registrationForm: FormGroup;
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
        filter(state => !!state?.authErrorMessages?.length),
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

    this.registrationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      usernameOrEmail: ['', [Validators.required]],
      username: '',
      password: ['', Validators.required]
    });
  }

  toggleMode(): void {
    this.mode = this.mode === 'signin' ? 'signup' : 'signin';

    if (this.mode === 'signin') {
      this.registrationForm.addControl('usernameOrEmail', this.usernameOrEmailControl)
      this.registrationForm.removeControl('email');
    } else {
      this.registrationForm.addControl('email', this.emailControl)
      this.registrationForm.removeControl('usernameOrEmail');
    }

    console.log(this.registrationForm);
  }

  onSubmit(): void {

    if (this.mode === 'signin') {
      this.store.dispatch(new LoginStart(this.registrationForm.value))
    } else {
      console.log('On submit form SignupStart', this.registrationForm.value);
      this.store.dispatch(new SignupStart(this.registrationForm.value))
    }
  }

  ngOnDestroy(): void {
    this.destroyed$.next(true);
  }
}
