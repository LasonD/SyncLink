import { Component, OnInit } from '@angular/core';
import { Whiteboard } from "../store/whiteboard.reducer";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-whiteboards-list',
  templateUrl: './whiteboards-list.component.html',
  styleUrls: ['./whiteboards-list.component.scss']
})
export class WhiteboardsListComponent implements OnInit {
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

  constructor(private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
  }

  navigateToWhiteboard(id: number): void {
    this.router.navigate([id], { relativeTo: this.activatedRoute });
  }

  openCreateForm() {
    this.router.navigate(['create']);
  }
}
