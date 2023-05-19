import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateWhiteboardComponent } from './create-whiteboard.component';

describe('CreateWhiteboardComponent', () => {
  let component: CreateWhiteboardComponent;
  let fixture: ComponentFixture<CreateWhiteboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateWhiteboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateWhiteboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
