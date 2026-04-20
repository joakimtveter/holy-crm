import { useQuery } from "@tanstack/react-query";

import { useMembersQueryOptions } from "#/domains/members/members.api.ts";
import type { Pagination } from "#/shared/types/api.types.ts";

export function useMembers(pagination?: Pagination) {
  return useQuery(useMembersQueryOptions(pagination));
}
