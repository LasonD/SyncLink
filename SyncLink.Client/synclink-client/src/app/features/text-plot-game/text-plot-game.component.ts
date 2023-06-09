import { Component, Input } from '@angular/core';
import { TextPlotEntry, TextPlotGame, TextPlotVote } from "./store/text-plot-game.reducer";
import { voteEntry } from "./store/text-plot-game.actions";

@Component({
  selector: 'app-text-plot-game',
  templateUrl: './text-plot-game.component.html',
  styleUrls: ['./text-plot-game.component.scss']
})
export class TextPlotGameComponent {
  @Input() game: TextPlotGame;
  @Input() entries: TextPlotEntry[];
  @Input() votes: TextPlotVote[];

  committedEntries: TextPlotEntry[];
  uncommittedEntries: TextPlotEntry[];

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
      { id: 1, userId: 1, entryId: 3, comment: 'Great entry!', creationDate: new Date() },
      { id: 2, userId: 1, entryId: 4, comment: 'I like this one.', creationDate: new Date() }
    ];

    this.committedEntries = this.entries.filter(entry => entry.isCommitted);
    this.uncommittedEntries = this.entries.filter(entry => !entry.isCommitted);

    this.committedEntries = this.entries.filter(entry => entry.isCommitted);
    this.uncommittedEntries = this.entries.filter(entry => !entry.isCommitted);
  }

  vote(entry: TextPlotEntry) {
    // this.store.dispatch(voteEntry({  }))
  }

  getVoteCount(id: number) {

  }
}
