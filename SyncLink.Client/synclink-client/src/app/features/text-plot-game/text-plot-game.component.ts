import { Component, Input } from '@angular/core';
import { TextPlotEntry, TextPlotGame, TextPlotVote } from "./store/text-plot-game.reducer";
import { VoteModalComponent } from "./vote-modal/vote-modal.component";
import { MatDialog } from "@angular/material/dialog";

@Component({
  selector: 'app-text-plot-game',
  templateUrl: './text-plot-game.component.html',
  styleUrls: ['./text-plot-game.component.scss'],
  providers: [MatDialog]
})
export class TextPlotGameComponent {
  @Input() game: TextPlotGame;
  @Input() entries: TextPlotEntry[];
  @Input() votes: TextPlotVote[];

  committedEntries: TextPlotEntry[];
  uncommittedEntries: TextPlotEntry[];

  constructor(private dialog: MatDialog) {
  }

  ngOnInit() {
    this.game = {
      id: 1,
      groupId: 1,
      starterId: 1,
      createdAt: new Date(),
      creationDate: new Date(),
      topic: 'Sample Topic'
    };
    this.entries = [
      { id: 1, userId: 1, gameId: 1, text: 'Entry 1', creationDate: new Date(), isCommitted: true },
      { id: 2, userId: 1, gameId: 1, text: 'Entry 2', creationDate: new Date(), isCommitted: true },
      { id: 3, userId: 1, gameId: 1, text: 'Entry 3', creationDate: new Date(), isCommitted: false },
      { id: 4, userId: 1, gameId: 1, text: 'Entry 4', creationDate: new Date(), isCommitted: false }
    ];
    this.votes = [
      { id: 1, userId: 1, entryId: 3, comment: 'Great entry!', creationDate: new Date(), score: 8 },
      { id: 2, userId: 1, entryId: 4, comment: 'I like this one.', creationDate: new Date(), score: 9 }
    ];

    this.committedEntries = this.entries.filter(entry => entry.isCommitted);
    this.uncommittedEntries = this.entries.filter(entry => !entry.isCommitted);

    this.committedEntries = this.entries.filter(entry => entry.isCommitted);
    this.uncommittedEntries = this.entries.filter(entry => !entry.isCommitted);
  }

  openVoteModal(entry): void {
    const dialogRef = this.dialog.open(VoteModalComponent, {
      width: '550px',
      data: {entry: entry}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // perform your voting logic here with result.comment and result.score
    });
  }

  getVoteCount(id: number) {
    return this.votes.filter(v => v.entryId === id).length;
  }
}
