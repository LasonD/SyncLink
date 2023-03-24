import { Group } from "../group.model";


export interface State {
  groups: Group[];
  isLoading: boolean;
}
