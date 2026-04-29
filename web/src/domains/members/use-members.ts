import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import {
  createMember,
  updateMember,
  useMemberByIdQueryOptions,
  useMembersQueryOptions,
} from "#/domains/members/members.api.ts";
import type { MemberPayload } from "#/domains/members/members.schema.ts";
import { ALL_MEMBERS, SINGLE_MEMBER } from "#/shared/constants/query-keys.ts";
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
    mutationKey: ["create-member"],
    mutationFn: createMember,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [ALL_MEMBERS],
      });
    },
  });
}

export function useUpdateMember(memberId: string) {
  const queryClient = useQueryClient();
  return useMutation({
    mutationKey: ["update-member", { memberId }],
    mutationFn: (data: MemberPayload) => updateMember(memberId, data),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [ALL_MEMBERS],
      });
      queryClient.invalidateQueries({
        queryKey: [SINGLE_MEMBER, { memberId }],
      });
    },
  });
}
