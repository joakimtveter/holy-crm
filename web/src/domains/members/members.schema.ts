import * as z from "zod";

import { Gender } from "#/domains/members/member.types.ts";

export const memberFormValidationSchema = z.object({
  firstName: z.string().min(1),
  middleNames: z.string().optional(),
  lastName: z.string().min(1),
  dateOfBirth: z.iso.date().refine(
    (value) => {
      const tomorrow = new Date();
      tomorrow.setUTCDate(tomorrow.getUTCDate() + 1);
      return value <= tomorrow.toISOString().slice(0, 10);
    },
    { message: "Date of birth cannot be in the future" },
  ),
  gender: z.enum(Gender),
});

export type MemberFormValues = z.input<typeof memberFormValidationSchema>;
export type MemberPayload = z.output<typeof memberFormValidationSchema>;
