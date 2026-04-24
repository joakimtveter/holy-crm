import * as z from "zod";

import { Gender } from "#/domains/members/member.types.ts";

export const createMemberFormValidationSchema = z.object({
  firstName: z.string().min(1),
  middleNames: z.string().optional(),
  lastName: z.string().min(1),
  dateOfBirth: z.iso.date(),
  gender: z.enum(Gender),
});

export type CreateMemberFormValues = z.input<typeof createMemberFormValidationSchema>;
export type CreateMemberPayload = z.output<typeof createMemberFormValidationSchema>;
