import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef } from "@angular/material/dialog";

@Component({
  selector: 'app-vote-modal',
  templateUrl: './vote-modal.component.html',
  styleUrls: ['./vote-modal.component.scss']
})
export class VoteModalComponent implements OnInit {
  voteForm: FormGroup;
  scores = Array.from({length: 10}, (_, i) => i + 1);

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<any>) {
    this.voteForm = this.fb.group({
      comment: [''],
      score: ['']
    });
  }

  ngOnInit(): void {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  selectScore(score: number) {
    this.voteForm.get('score').setValue(score);
  }

  getButtonClass(score: number): string {
    if (score <= 3) {
      return 'btn-danger';
    } else if (score <= 6) {
      return 'btn-warning';
    } else {
      return 'btn-success';
    }
  }
}
