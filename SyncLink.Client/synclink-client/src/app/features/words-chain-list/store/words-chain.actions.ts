import { createAction, props } from "@ngrx/store";

export const createWordsChain = createAction(
  '[Words Chain] Create Words Chain',
  props<{ groupId: number, topic: string }>()
);
