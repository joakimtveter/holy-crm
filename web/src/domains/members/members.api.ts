import { queryOptions } from "@tanstack/react-query";

import type { MemberBrief } from "#/domains/members/member.type.ts";
import { MEMBERS } from "#/shared/constants/query-keys.ts";
import type { Pagination } from "#/shared/types/api.types.ts";

const BASE_URL = import.meta.env.VITE_BASE_URL;

export function useMembersQueryOptions(pagination?: Pagination) {
  return queryOptions({
    queryKey: [MEMBERS, pagination],
    queryFn: () => getMembers(pagination),
  });
}

async function getMembers(pagination?: Pagination): Promise<MemberBrief[]> {
  const url = new URL(`${BASE_URL}/members`);

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
  if (!response.ok) throw new Error("Failed to fetch members");

  return response.json();
}
