import { combineReducers, createReducer, on } from "@ngrx/store";
import {
  createWhiteboard, createWhiteboardFailure, createWhiteboardSuccess,
  getWhiteboard,
  getWhiteboardFailure,
  getWhiteboards, getWhiteboardsFailure,
  getWhiteboardsSuccess,
  getWhiteboardSuccess, whiteboardUpdatedExternal
} from "./whiteboard.actions";
import { ElementTypeEnum, LineCapEnum, LineJoinEnum, WhiteboardElement } from "ng-whiteboard";
import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";

export interface WhiteboardsFeatureState {
  whiteboards: WhiteboardsState,
  whiteboardElements: WhiteboardElementsState
}

export interface WhiteboardsState extends EntityState<Whiteboard> {
  selectedWhiteboardId: number | null;
  createdWhiteboardId: number | null;
  whiteboardLoading: boolean;
  whiteboardError: any;
}

export interface WhiteboardElementsState extends EntityState<ExtendedWhiteboardElement> {

}

export const whiteboardsAdapter: EntityAdapter<Whiteboard> = createEntityAdapter<Whiteboard>();
export const whiteboardElementsAdapter: EntityAdapter<ExtendedWhiteboardElement> = createEntityAdapter<ExtendedWhiteboardElement>();

export const whiteboardsInitialState: WhiteboardsState = whiteboardsAdapter.getInitialState({
  selectedWhiteboardId: null,
  createdWhiteboardId: null,
  whiteboardError: null,
  whiteboardLoading: false,
});

export const whiteboardElementsInitialState: WhiteboardElementsState = whiteboardElementsAdapter.getInitialState({

});

export const whiteboardElementsReducer = createReducer(
  whiteboardElementsInitialState,
  on(getWhiteboardsSuccess, (state, { whiteboards }): WhiteboardElementsState => {
    const elements = whiteboards.entities.flatMap(ee => ee.whiteboardElements.map(e => adjustExternalElement(e)));
    return whiteboardElementsAdapter.addMany(elements, state);
  }),
);

export const whiteboardReducer = createReducer(
  whiteboardsInitialState,
  on(getWhiteboard, (state): WhiteboardsState => ({ ...state, whiteboardLoading: true })),
  on(getWhiteboards, (state): WhiteboardsState => ({...state, whiteboardLoading: true})),
  on(getWhiteboardSuccess, (state, { whiteboard }): WhiteboardsState => {
    whiteboard = adjustExternalWhiteboard(whiteboard);
    return whiteboardsAdapter.upsertOne(whiteboard, {
      ...state,
      selectedWhiteboardId: whiteboard.id,
      whiteboardLoading: false
    });
  }),
  on(getWhiteboardsSuccess, (state, { whiteboards }): WhiteboardsState => {
    whiteboards = {...whiteboards, entities: whiteboards.entities.map(w => adjustExternalWhiteboard(w))};
    return whiteboardsAdapter.addMany(whiteboards.entities, {...state, whiteboardLoading: false});
  }),
  on(getWhiteboardFailure, (state, { error }): WhiteboardsState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
  on(getWhiteboardsFailure, (state, { error }): WhiteboardsState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
  on(createWhiteboard, (state): WhiteboardsState => ({...state, whiteboardLoading: true})),
  on(createWhiteboardSuccess, (state, { whiteboard }): WhiteboardsState => whiteboardsAdapter.upsertOne(whiteboard, {
    ...state,
    createdWhiteboardId: whiteboard.id,
    whiteboardLoading: false
  })),
  on(createWhiteboardFailure, (state, { error }): WhiteboardsState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
  on(whiteboardUpdatedExternal, (state, { id, changes, groupId }): WhiteboardsState => {
    if (!state.entities[id] || state.entities[id].groupId !== groupId) {
      return state;
    }

    changes = changes.map(c => adjustExternalElement(c));
    const updatedWhiteboardElements = [...state.entities[id].whiteboardElements, ...changes];
    const updatedWhiteboard = { ...state.entities[id], whiteboardElements: updatedWhiteboardElements };

    return whiteboardsAdapter.updateOne({ id: id, changes: updatedWhiteboard }, state);
  }),
);

export const whiteboardFeatureReducer = combineReducers({
  whiteboards: whiteboardReducer,
  whiteboardElements: whiteboardElementsReducer
});

function adjustExternalWhiteboard(w: Whiteboard): Whiteboard {
  return {
    ...w,
    whiteboardElements: w.whiteboardElements.map(e => adjustExternalElement(e))
  };
}

function adjustExternalElement(e: ExtendedWhiteboardElement): ExtendedWhiteboardElement {
  return {
    ...e,
    type: stringToEnumValue(e.type, ElementTypeEnum),
    id: `${e.id}_External`,
    options: {
      ...e.options,
      lineJoin: stringToEnumValue(e?.options?.lineJoin, LineJoinEnum),
      lineCap: stringToEnumValue(e?.options?.lineJoin, LineCapEnum),
    }
  };
}

function stringToEnumValue<T>(input: string, enumObj: T): T[keyof T] | undefined {
  const enumValues = Object.values(enumObj);
  const matchingValue = enumValues.find(value => value === input);
  return matchingValue as T[keyof T] | undefined;
}

export class ExtendedWhiteboardElement extends WhiteboardElement {
  whiteboardId: number;
  authorId: number;
}

export interface Whiteboard {
  id: number,
  name: string,
  whiteboardElements: ExtendedWhiteboardElement[],
  creatorId: number,
  groupId: number,
}
