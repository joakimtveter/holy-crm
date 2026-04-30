import type { VenuePayload } from "#/domains/venues/venue.schema.ts";
import type { Venue, VenueBrief } from "#/domains/venues/venue.types.ts";
import { apiFetch } from "#/shared/lib/fetch/api-fetch.ts";
import type { PaginatedList, Pagination } from "#/shared/types/api.types.ts";

export async function getVenues(pagination?: Pagination) {
  return await apiFetch<PaginatedList<VenueBrief>>("/venues", { query: pagination });
}

export async function getVenueById(venueId: string) {
  return await apiFetch<Venue>(`/venues/${venueId}`);
}

export async function createVenue(payload: VenuePayload) {
  return await apiFetch<Venue>("/venues", { method: "POST", body: payload });
}

export async function updateVenue(venueId: string, payload: VenuePayload) {
  return await apiFetch(`/venues/${venueId}`, { method: "PUT", body: payload });
}
