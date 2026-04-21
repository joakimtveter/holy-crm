export const Gender = {
  unknown: "unknown",
  male: "male",
  female: "female",
} as const;

export type GenderEnum = (typeof Gender)[keyof typeof Gender];

export type MemberBrief = {
  id: string;
  firstName: string;
  middleNames: string | null;
  lastName: string;
  dateOfBirth: string;
  gender: GenderEnum;
  createdAt: string;
  updatedAt: string;
};

export type Member = {
  age: number;
} & MemberBrief;
