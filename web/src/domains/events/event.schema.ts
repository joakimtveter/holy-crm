import * as z from "zod";

export const createEventSchema = z.object({
  title: z.string().min(3).max(100),
  description: z.string().max(250),
  startsAt: z.iso.datetime({ local: true }),
  endsAt: z.iso.datetime({ local: true }),
  venueId: z.uuid(),
});

export type EventFormValues = z.input<typeof createEventSchema>;
export type EventPayload = z.output<typeof createEventSchema>;
