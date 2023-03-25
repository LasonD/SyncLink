import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AppState } from "../../store/app.reducer";
import { select, Store } from "@ngrx/store";
import { createGroup } from "../store/groups.actions";
import { selectCreatedGroup } from "../store/groups.selectors";

@Component({
  selector: 'app-create-group-form',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.scss']
})
export class CreateGroupComponent implements OnInit {
  createGroupForm: FormGroup;

  constructor(private fb: FormBuilder,
              private http: HttpClient,
              private store: Store<AppState>
  ) { }

  ngOnInit(): void {
    this.store.pipe(select(selectCreatedGroup))
      .subscribe((group) => {
        console.log('Group created: ', group)
      });

    this.createGroupForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      description: ''
    });
  }

  get name() {
    return this.createGroupForm.get('name');
  }

  onSubmit() {
    const createGroupDto = {
      name: this.createGroupForm.value.name,
      description: this.createGroupForm.value.description
    };

    this.store.dispatch(createGroup(createGroupDto));
  }
}
