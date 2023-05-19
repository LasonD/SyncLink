import { Component } from '@angular/core';
import { Subject } from "rxjs";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { RoomsState } from "../../../rooms/store/rooms.reducer";
import { selectCurrentGroupId } from "../../../groups/group-hub/store/group-hub.selectors";
import { take } from "rxjs/operators";
import { createWhiteboard } from "../store/whiteboard.actions";

@Component({
  selector: 'app-create-whiteboard',
  templateUrl: './create-whiteboard.component.html',
  styleUrls: ['./create-whiteboard.component.scss']
})
export class CreateWhiteboardComponent {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  createWhiteboardForm: FormGroup;

  constructor(private fb: FormBuilder, private store: Store<RoomsState>) {

  }

  ngOnInit() {
    this.createWhiteboardForm = this.fb.group({
      whiteboardName: ['', Validators.required],
    });
  }

  onSubmit() {
    if (!this.createWhiteboardForm.valid) {
      return;
    }

    const value = this.createWhiteboardForm.value;

    const name = value.whiteboardName;

    this.store.select(selectCurrentGroupId).pipe(take(1))
      .subscribe(groupId => {
        this.store.dispatch(createWhiteboard({ name, groupId }));
      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
