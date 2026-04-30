import { queryOptions } from "@tanstack/react-query";

import type { EventBrief } from "#/domains/events/events.types.ts";
import type { Member } from "#/domains/members/member.types.ts";
import { ALL_EVENTS, SINGLE_EVENT } from "#/shared/constants/query-keys.ts";
import { BASE_URL } from "#/shared/constants/strings.constants.ts";
import type { Pagination } from "#/shared/types/api.types.ts";

export function useEventsQueryOptions(pagination?: Pagination) {
  return queryOptions({
    queryKey: [ALL_EVENTS, pagination],
    queryFn: () => getEvents(pagination),
  });
}

async function getEvents(pagination?: Pagination): Promise<EventBrief[]> {
  const url = new URL(`${BASE_URL}/events`);

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
  if (!response.ok) throw new Error("Failed to fetch events");

  return response.json();
}

export function useEventByIdQueryOptions(eventId: string) {
  return queryOptions({
    queryKey: [SINGLE_EVENT, { eventId }],
    queryFn: () => getMemberById(eventId),
  });
}

async function getMemberById(eventId: string): Promise<Member> {
  const url = new URL(`${BASE_URL}/events/${eventId}`);

  const response = await fetch(url.toString(), {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  });
  if (!response.ok) throw new Error(`Failed to fetch with id: ${eventId}`);

  return response.json();
}
