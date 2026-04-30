import { createFileRoute } from "@tanstack/react-router";
import * as z from "zod";

import EventList from "#/domains/events/components/event-list.tsx";
import { useEventsQueryOptions } from "#/domains/events/events.api.ts";
import { useEvents } from "#/domains/events/use-events.ts";
import PageWrapper from "#/shared/components/page-wrapper.tsx";

const eventsSearchSchema = z.object({
  page: z.number().default(1),
  pageSize: z.number().default(12),
});

export const Route = createFileRoute("/events/")({
  component: EventsPage,
  validateSearch: eventsSearchSchema,
  loaderDeps: ({ search: { page, pageSize } }) => ({ page, pageSize }),
  loader: ({ context: { queryClient }, deps: { page, pageSize } }) =>
    queryClient.ensureQueryData(
      useEventsQueryOptions({ page: Number(page), pageSize: Number(pageSize) }),
    ),
});

function EventsPage() {
  const { data: events = [] } = useEvents();

  return (
    <PageWrapper title="Events">
      <EventList events={events} />
    </PageWrapper>
  );
}
