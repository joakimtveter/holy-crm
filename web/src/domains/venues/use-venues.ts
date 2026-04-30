import { queryOptions, useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";

import type { VenuePayload } from "#/domains/venues/venue.schema.ts";
import { createVenue, getVenueById, getVenues, updateVenue } from "#/domains/venues/venues.api.ts";
import { ALL_VENUES, SINGLE_VENUE } from "#/shared/constants/query-keys.ts";
import type { Pagination } from "#/shared/types/api.types.ts";

export function useVenuesQueryOptions(pagination?: Pagination) {
  return queryOptions({
    queryKey: [ALL_VENUES, pagination],
    queryFn: () => getVenues(pagination),
  });
}

type VenuesQueryOptions = ReturnType<typeof useVenuesQueryOptions>;

export function useVenues(
  pagination?: Pagination,
  options?: Omit<VenuesQueryOptions, "queryKey" | "queryFn">,
) {
  return useQuery({ ...useVenuesQueryOptions(pagination), ...options });
}

export function useMemberByIdQueryOptions(venueId: string) {
  return queryOptions({
    queryKey: [SINGLE_VENUE, { venueId }],
    queryFn: () => getVenueById(venueId),
  });
}

export function useVenueById(venueId: string) {
  return useQuery(useMemberByIdQueryOptions(venueId));
}

export function useCreateVenue() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationKey: ["create-venue"],
    mutationFn: createVenue,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [ALL_VENUES],
      });
    },
  });
}

export function useUpdateVenue(venueId: string) {
  const queryClient = useQueryClient();
  return useMutation({
    mutationKey: ["update-venue", { venueId }],
    mutationFn: (data: VenuePayload) => updateVenue(venueId, data),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [ALL_VENUES],
      });
      queryClient.invalidateQueries({
        queryKey: [SINGLE_VENUE, { venueId }],
      });
    },
    onError: (error) => {
      toast.error(error.message);
    },
  });
}
