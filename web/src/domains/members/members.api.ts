import type { MemberBrief } from "#/domains/members/member.type.ts";
import type { Pagination } from "#/shared/types/api.types.ts";

const BASE_URL = process.env.VITE_BASE_URL;

async function getMembers(pagination?: Pagination): Promise<MemberBrief> {
  const response = await fetch(`${BASE_URL}/members`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  });
  if (!response.ok) throw new Error("Failed to fetch members");

  return response.json();
}
