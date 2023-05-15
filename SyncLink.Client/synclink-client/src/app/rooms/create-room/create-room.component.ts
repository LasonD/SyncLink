import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-create-room',
  templateUrl: './create-room.component.html',
  styleUrls: ['./create-room.component.scss']
})
export class CreateRoomComponent {
  createRoomForm: FormGroup;
  users: string[] = ['User1', 'User2', 'User3'];

  constructor(private fb: FormBuilder) {
    this.createRoomForm = this.fb.group({
      roomName: ['', Validators.required],
      description: [''],
      users: ['']
    });
  }

  onSubmit() {
    if (this.createRoomForm.valid) {
      console.log(this.createRoomForm.value);
    }
  }
}
