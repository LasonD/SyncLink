import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-create-group-form',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.scss']
})
export class CreateGroupComponent implements OnInit {
  createGroupForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  ngOnInit(): void {
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

    this.http.post('/api/groups', createGroupDto).subscribe(
      response => {
        console.log(response);
      },
      error => {
        console.log(error);
      }
    );
  }
}
