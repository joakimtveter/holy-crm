import type { VenuePayload } from "#/domains/venues/venue.schema.ts";
import type { Venue, VenueBrief } from "#/domains/venues/venue.types.ts";
import type { PaginatedList, Pagination } from "#/shared/types/api.types.ts";

const BASE_URL = import.meta.env.VITE_BASE_URL;

export async function getVenues(pagination?: Pagination): Promise<PaginatedList<VenueBrief>> {
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

export async function getVenueById(venueId: string): Promise<Venue> {
  const url = new URL(`${BASE_URL}/venues/${venueId}`);

  const response = await fetch(url.toString(), {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  });
  if (!response.ok) throw new Error(`Failed to fetch venue with id: ${venueId}`);

  return response.json();
}

export async function createVenue(payload: VenuePayload): Promise<Venue> {
  const response = await fetch(`${BASE_URL}/venues`, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });

  if (!response.ok) throw new Error(`Failed to create venue: ${JSON.stringify(payload)}`);
  return response.json();
}

export async function updateVenue(venueId: string, payload: VenuePayload): Promise<Venue> {
  const response = await fetch(`${BASE_URL}/venues/${venueId}`, {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });

  if (!response.ok) throw new Error(`Failed to update venue: ${JSON.stringify(payload)}`);
  return response.json();
}
