export type Pagination = {
  page: number;
  pageSize: number;
};

export type PaginatedList<T> = {
  items: Array<T>;
  currentPage: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPrevious: boolean;
  hasNext: boolean;
};
