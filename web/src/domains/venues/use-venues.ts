import { useQuery, type UseQueryOptions } from "@tanstack/react-query";

import { useVenuesQueryOptions } from "#/domains/venues/venues.api.ts";
import type { Pagination } from "#/shared/types/api.types.ts";

export function useVenues(pagination?: Pagination, options?: Omit<UseQueryOptions, "queryKey" | "queryFn">) {
  return useQuery({ ...useVenuesQueryOptions(pagination), ...options });
}
