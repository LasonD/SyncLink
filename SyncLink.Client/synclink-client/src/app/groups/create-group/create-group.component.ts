import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AppState } from "../../store/app.reducer";
import { select, Store } from "@ngrx/store";
import { ToastrService } from "ngx-toastr";
import { selectCreatedGroup, selectCreateGroupError } from "./store/create-group.selectors";
import { createGroup } from "./store/create-group.actions";

@Component({
  selector: 'app-create-group-form',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.scss']
})
export class CreateGroupComponent implements OnInit {
  createGroupForm: FormGroup;

  constructor(private fb: FormBuilder,
              private http: HttpClient,
              private store: Store<AppState>,
              private toastService: ToastrService,
  ) { }

  ngOnInit(): void {
    this.store.pipe(select(selectCreatedGroup))
      .subscribe((group) => {
        this.toastService.success('Your group was successfully created.');
      });

    this.store.pipe(select(selectCreateGroupError))
      .subscribe((group) => {
        this.toastService.success('Your group was successfully created.');
      });

    this.createGroupForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      description: ''
    });
  }

  get name() {
    return this.createGroupForm.get('name');
  }

  get description() {
    return this.createGroupForm.get('description');
  }

  onSubmit() {
    const createGroupDto = {
      name: this.createGroupForm.value.name,
      description: this.createGroupForm.value.description
    };

    this.store.dispatch(createGroup(createGroupDto));
  }

  public getErrors(control: AbstractControl) {
    const errors = control.errors;

    if (errors) {
      return [];
    }

    return Object.keys(errors).map(key => errors[key]);
  }
}
