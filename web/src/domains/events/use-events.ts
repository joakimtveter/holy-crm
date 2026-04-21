import { useQuery } from "@tanstack/react-query";

import { useEventByIdQueryOptions, useEventsQueryOptions } from "#/domains/events/events.api.ts";
import type { Pagination } from "#/shared/types/api.types.ts";

export function useEvents(pagination?: Pagination) {
  return useQuery(useEventsQueryOptions(pagination));
}

export function useEventById(memberId: string) {
  return useQuery(useEventByIdQueryOptions(memberId));
}
