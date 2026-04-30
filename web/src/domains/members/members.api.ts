import { queryOptions } from "@tanstack/react-query";

import type { Member, MemberBrief } from "#/domains/members/member.types.ts";
import type { MemberPayload } from "#/domains/members/members.schema.ts";
import { ALL_MEMBERS, SINGLE_MEMBER } from "#/shared/constants/query-keys.ts";
import { BASE_URL } from "#/shared/constants/strings.constants.ts";
import type { PaginatedList, Pagination } from "#/shared/types/api.types.ts";

export function useMembersQueryOptions(pagination?: Pagination) {
  return queryOptions({
    queryKey: [ALL_MEMBERS, pagination],
    queryFn: () => getMembers(pagination),
  });
}

async function getMembers(pagination?: Pagination): Promise<PaginatedList<MemberBrief>> {
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

export function useMemberByIdQueryOptions(memberId: string) {
  return queryOptions({
    queryKey: [SINGLE_MEMBER, { memberId }],
    queryFn: () => getMemberById(memberId),
  });
}

async function getMemberById(memberId: string): Promise<Member> {
  const url = new URL(`${BASE_URL}/members/${memberId}`);

  const response = await fetch(url.toString(), {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  });
  if (!response.ok) throw new Error(`Failed to fetch member with id: ${memberId}`);

  return response.json();
}

export async function createMember(payload: MemberPayload): Promise<Member> {
  const response = await fetch(`${BASE_URL}/members`, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });

  if (!response.ok) throw new Error(`Failed to create member: ${JSON.stringify(payload)}`);
  return response.json();
}

export async function updateMember(memberId: string, payload: MemberPayload): Promise<Member> {
  const response = await fetch(`${BASE_URL}/members/${memberId}`, {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });

  if (!response.ok) throw new Error(`Failed to update member: ${JSON.stringify(payload)}`);
  return response.json();
}
