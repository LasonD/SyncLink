import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { TextPlotGameStats } from "../store/text-plot-game.reducer";

@Component({
  selector: 'app-stats-modal',
  templateUrl: './stats-modal.component.html',
  styleUrls: ['./stats-modal.component.scss']
})
export class StatsModalComponent {
  showComments: { [key: number]: boolean } = {};
  showMoreDetails: { [key: number]: boolean } = {};

  constructor(@Inject(MAT_DIALOG_DATA) public data: TextPlotGameStats) {}

  ngOnInit() {
    this.data.userStats.sort((a, b) => b.wordsCommittedCount - a.wordsCommittedCount);
    this.data.userStats.forEach(user => {
      this.showComments[user.userId] = false;
      this.showMoreDetails[user.userId] = false;
    });
  }

  toggleComments(userId: number) {
    this.showComments[userId] = !this.showComments[userId];
  }

  toggleDetails(userId: number) {
    this.showMoreDetails[userId] = !this.showMoreDetails[userId];
  }
}
