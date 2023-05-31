import { Component } from '@angular/core';
import { distinctUntilChanged, Subject, takeUntil } from "rxjs";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { ActivatedRoute, Router } from "@angular/router";
import { selectCurrentGroupId } from "../../../groups/group-hub/store/group-hub.selectors";
import { filter, take } from "rxjs/operators";
import { createWhiteboard } from "../../whiteboard/store/whiteboard.actions";
import { selectCreatedWhiteboardId } from "../../whiteboard/store/whiteboard.selectors";
import { AppState } from "../../../store/app.reducer";

@Component({
  selector: 'app-create-words-chain',
  templateUrl: './create-words-chain.component.html',
  styleUrls: ['./create-words-chain.component.scss']
})
export class CreateWordsChainComponent {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  createWordsChainForm: FormGroup;

  constructor(private fb: FormBuilder, private store: Store<AppState>, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.createWordsChainForm = this.fb.group({
      topic: ['', Validators.required],
    });
  }

  onSubmit() {
    if (!this.createWordsChainForm.valid) {
      return;
    }

    const value = this.createWordsChainForm.value;

    const name = value.topic;

    this.store.select(selectCurrentGroupId).pipe(filter(id => !!id), take(1))
      .subscribe(groupId => {
        this.store.dispatch(createWhiteboard({ name, groupId }));
      });

    this.store.select(selectCreatedWhiteboardId)
      .pipe(distinctUntilChanged(), filter(id => !!id), takeUntil(this.destroyed$), take(1))
      .subscribe(_ => {
        this.router.navigate(['../'], { relativeTo: this.route });
      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
