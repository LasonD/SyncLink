export interface Page<T> {
  entities: T[];
  page: number;
  nextPage?: number | null;
  previousPage?: number | null;
  lastPage: number;
  itemCount: number;
  pageSize: number;
  pageCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
