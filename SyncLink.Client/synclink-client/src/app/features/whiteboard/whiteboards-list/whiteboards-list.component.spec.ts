import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WhiteboardsListComponent } from './whiteboards-list.component';

describe('WhiteboardsListComponent', () => {
  let component: WhiteboardsListComponent;
  let fixture: ComponentFixture<WhiteboardsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WhiteboardsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WhiteboardsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
