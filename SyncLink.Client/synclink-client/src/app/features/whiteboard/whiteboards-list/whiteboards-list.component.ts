import { Component } from '@angular/core';
import { Whiteboard } from "../store/whiteboard.reducer";

@Component({
  selector: 'app-whiteboards-list',
  templateUrl: './whiteboards-list.component.html',
  styleUrls: ['./whiteboards-list.component.scss']
})
export class WhiteboardsListComponent {
  whiteboards: Whiteboard[] = [
    {
      id: 1,
      name: 'Whiteboard 1',
      whiteboardElements: [],
      creatorId: 1,
      groupId: 1
    },
    {
      id: 2,
      name: 'Whiteboard 2',
      whiteboardElements: [],
      creatorId: 1,
      groupId: 1
    },
  ];

  constructor() { }

  ngOnInit(): void {
  }

  navigateToWhiteboard(id: number): void {
    // replace with your actual routing logic
    console.log(`Navigating to whiteboard ${id}`);
  }
}
