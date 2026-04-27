import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import {
  createMember,
  useMemberByIdQueryOptions,
  useMembersQueryOptions,
} from "#/domains/members/members.api.ts";
import { MEMBERS } from "#/shared/constants/query-keys.ts";
import type { Pagination } from "#/shared/types/api.types.ts";

export function useMembers(pagination?: Pagination) {
  return useQuery(useMembersQueryOptions(pagination));
}

export function useMemberById(memberId: string) {
  return useQuery(useMemberByIdQueryOptions(memberId));
}

export function useCreateMember() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: createMember,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [MEMBERS],
      });
    },
  });
}
