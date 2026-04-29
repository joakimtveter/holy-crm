import { queryOptions } from "@tanstack/react-query";

import type { VenueBrief } from "#/domains/venues/venue.types.ts";
import { ALL_VENUES } from "#/shared/constants/query-keys.ts";
import type { PaginatedList, Pagination } from "#/shared/types/api.types.ts";

const BASE_URL = import.meta.env.VITE_BASE_URL;

export function useVenuesQueryOptions(pagination?: Pagination) {
  return queryOptions({
    queryKey: [ALL_VENUES, pagination],
    queryFn: () => getVenues(pagination),
  });
}

async function getVenues(pagination?: Pagination): Promise<PaginatedList<VenueBrief>> {
  const url = new URL(`${BASE_URL}/venues`);

  if (pagination) {
    Object.entries(pagination).forEach(([key, value]) => {
      if (value !== undefined && value !== null) {
        url.searchParams.set(key, String(value));
      }
    });
  }

  const response = await fetch(url.toString(), {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  });
  if (!response.ok) throw new Error("Failed to fetch venues");

  return response.json();
}
