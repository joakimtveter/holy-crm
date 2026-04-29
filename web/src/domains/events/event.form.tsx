import { Button } from "@base-ui/react";
import { addHours, formatISO } from "date-fns";

import { createEventSchema, type EventFormValues } from "#/domains/events/event.schema.ts";
import Form from "#/shared/components/forms/form.tsx";
import { useAppForm } from "#/shared/hooks/use-form.tsx";

type EventFormProps = {
  action: "create" | "edit";
  defaultValues?: EventFormValues;
};

export default function EventForm(props: EventFormProps) {
  const { action, defaultValues } = props;
  const form = useAppForm({
    defaultValues: {
      title: defaultValues?.title ?? "",
      description: defaultValues?.description ?? "",
      startsAt: formatISO(new Date(defaultValues?.startsAt ?? Date.now())),
      endsAt: defaultValues?.endsAt
        ? formatISO(new Date(defaultValues?.endsAt))
        : formatISO(addHours(Date.now(), 1)),
      venueId: defaultValues?.venueId ?? "",
    },
    validators: {
      onSubmit: createEventSchema,
    },
    onSubmit: () => {
      if (action === "edit") {
        form.reset();
      } else {
        form.reset();
      }
    },
  });

  return (
    <Form
      onSubmit={(e) => {
        e.preventDefault();
        e.stopPropagation();
        form.handleSubmit();
      }}
    >
      <form.AppField name="title" children={(field) => <field.TextField label="Event title" />} />
      <form.AppField
        name="description"
        children={(field) => (
          <field.TextField label="Event description" helpText="Max 255 characters" />
        )}
      />
      <form.AppField
        name="startsAt"
        children={(field) => <field.DatetimePicker label="Event start" />}
      />
      <form.AppField
        name="endsAt"
        children={(field) => <field.DatetimePicker label="Event end" />}
      />
      <form.AppField name="venueId" children={(field) => <field.VenuePicker />} />

      <Button type="submit"> Submit</Button>
    </Form>
  );
}
