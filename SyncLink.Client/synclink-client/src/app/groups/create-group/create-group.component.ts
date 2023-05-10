import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AppState } from "../../store/app.reducer";
import { select, Store } from "@ngrx/store";
import { ToastrService } from "ngx-toastr";
import { selectCreatedGroup, selectCreateGroupError } from "./store/create-group.selectors";
import { createGroup } from "./store/create-group.actions";
import { Router } from "@angular/router";
import { distinctUntilChanged, filter, skip, Subject, takeUntil } from "rxjs";

@Component({
  selector: 'app-create-group-form',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.scss']
})
export class CreateGroupComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  createGroupForm: FormGroup;

  constructor(private fb: FormBuilder,
              private http: HttpClient,
              private store: Store<AppState>,
              private router: Router,
              private toastService: ToastrService,
  ) {
  }

  ngOnInit(): void {
    this.createGroupForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      description: '',
      isPrivate: [false]
    });

    this.store.pipe(
      takeUntil(this.destroyed$),
      select(selectCreatedGroup),
      distinctUntilChanged(),
      skip(1),
    )
      .subscribe((group) => {
        this.toastService.success('Your group was successfully created.');
        this.router.navigate([`/groups/${group.id}/hub`]);
      });

    this.store.pipe(select(selectCreateGroupError))
      .pipe(takeUntil(this.destroyed$), filter((e) => !!e))
      .subscribe((error) => {
        this.toastService.error(error, 'Something went wrong while creating a group.');
      });
  }

  get name() {
    return this.createGroupForm.get('name');
  }

  get description() {
    return this.createGroupForm.get('description');
  }

  get isPrivate() {
    return this.createGroupForm.get('isPrivate');
  }


  onSubmit() {
    const createGroupDto = this.createGroupForm.value;

    this.store.dispatch(createGroup(createGroupDto));
  }

  public getErrors(control: AbstractControl) {
    const errors = control.errors;

    if (errors) {
      return [];
    }

    return Object.keys(errors).map(key => errors[key]);
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
