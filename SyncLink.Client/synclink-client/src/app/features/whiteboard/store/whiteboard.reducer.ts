import { createReducer, on } from "@ngrx/store";
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

export interface Whiteboard {
  id: number,
  name: string,
  whiteboardElements: WhiteboardElement[],
  creatorId: number,
  groupId: number,
}

export interface WhiteboardState extends EntityState<Whiteboard> {
  selectedWhiteboardId: number | null;
  createdWhiteboardId: number | null;
  whiteboardLoading: boolean;
  whiteboardError: any;
}

export const adapter: EntityAdapter<Whiteboard> = createEntityAdapter<Whiteboard>();

export const initialState: WhiteboardState = adapter.getInitialState({
  selectedWhiteboardId: null,
  createdWhiteboardId: null,
  whiteboardError: null,
  whiteboardLoading: false,
});

export const whiteboardReducer = createReducer(
  initialState,
  on(getWhiteboard, (state): WhiteboardState => ({ ...state, whiteboardLoading: true })),
  on(getWhiteboards, (state): WhiteboardState => ({...state, whiteboardLoading: true})),
  on(getWhiteboardSuccess, (state, { whiteboard }): WhiteboardState => {
    whiteboard = adjustExternalWhiteboard(whiteboard);
    return adapter.upsertOne(whiteboard, {
      ...state,
      selectedWhiteboardId: whiteboard.id,
      whiteboardLoading: false
    });
  }),
  on(getWhiteboardsSuccess, (state, { whiteboards }): WhiteboardState => {
    whiteboards = {...whiteboards, entities: whiteboards.entities.map(w => adjustExternalWhiteboard(w))};
    return adapter.addMany(whiteboards.entities, {...state, whiteboardLoading: false});
  }),
  on(getWhiteboardFailure, (state, { error }): WhiteboardState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
  on(getWhiteboardsFailure, (state, { error }): WhiteboardState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
  on(createWhiteboard, (state): WhiteboardState => ({...state, whiteboardLoading: true})),
  on(createWhiteboardSuccess, (state, { whiteboard }): WhiteboardState => adapter.upsertOne(whiteboard, {
    ...state,
    createdWhiteboardId: whiteboard.id,
    whiteboardLoading: false
  })),
  on(createWhiteboardFailure, (state, { error }): WhiteboardState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
  on(whiteboardUpdatedExternal, (state, { id, changes, groupId }): WhiteboardState => {
    if (!state.entities[id] || state.entities[id].groupId !== groupId) {
      return state;
    }

    changes = changes.map(c => adjustExternalElement(c));
    const updatedWhiteboardElements = [...state.entities[id].whiteboardElements, ...changes];
    const updatedWhiteboard = { ...state.entities[id], whiteboardElements: updatedWhiteboardElements };

    return adapter.updateOne({ id: id, changes: updatedWhiteboard }, state);
  }),
);

function adjustExternalWhiteboard(w: Whiteboard): Whiteboard {
  return {
    ...w,
    whiteboardElements: w.whiteboardElements.map(e => adjustExternalElement(e))
  };
}

function adjustExternalElement(e: WhiteboardElement): WhiteboardElement {
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
