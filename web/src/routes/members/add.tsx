import { useForm } from "@tanstack/react-form";
import { createFileRoute } from "@tanstack/react-router";
import type { SubmitEvent } from "react";

import { Gender } from "#/domains/members/member.types.ts";
import {
  createMemberFormValidationSchema,
  type CreateMemberFormValues,
} from "#/domains/members/members.schema.ts";
import { useCreateMember } from "#/domains/members/use-members.ts";
import { DatePicker } from "#/shared/components/forms/date-picker.tsx";
import Form from "#/shared/components/forms/form.tsx";
import SingleSelect from "#/shared/components/forms/single-select.tsx";
import TextField from "#/shared/components/forms/text-field.tsx";
import PageWrapper from "#/shared/components/page-wrapper.tsx";
import { Button } from "#/shared/components/ui/button.tsx";

export const Route = createFileRoute("/members/add")({
  component: RouteComponent,
});

function RouteComponent() {
  const { mutate, isPending } = useCreateMember();
  const form = useForm({
    defaultValues: {
      firstName: "",
      middleNames: undefined,
      lastName: "",
      dateOfBirth: "",
      gender: Gender.unknown,
    } as CreateMemberFormValues,
    validators: {
      onSubmit: createMemberFormValidationSchema,
    },
    onSubmit: async ({ value }) => {
      if (isPending) return;
      mutate(value);
    },
  });

  const handleSubmit = (e: SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();
    form.handleSubmit();
  };

  return (
    <PageWrapper title="Add member">
      <Form onSubmit={handleSubmit}>
        <form.Field name="firstName">
          {(field) => (
            <TextField
              label="First name"
              autoComplete="given-name"
              value={field.state.value}
              onChange={field.handleChange}
              onBlur={field.handleBlur}
            />
          )}
        </form.Field>
        <form.Field name="middleNames">
          {(field) => (
            <TextField
              label="Middle names"
              autoComplete="additional-name"
              value={field.state.value ?? ""}
              onChange={field.handleChange}
              onBlur={field.handleBlur}
            />
          )}
        </form.Field>{" "}
        <form.Field name="lastName">
          {(field) => (
            <TextField
              label="Last name"
              autoComplete="family-name"
              value={field.state.value}
              onChange={field.handleChange}
              onBlur={field.handleBlur}
            />
          )}
        </form.Field>
        <form.Field name="dateOfBirth">
          {(field) => (
            <DatePicker
              label="Date of birth"
              value={field.state.value}
              onChange={field.handleChange}
              onBlur={field.handleBlur}
              errorMessage={field.state.meta.errors[0]?.toString()}
            />
          )}
        </form.Field>
        <form.Field name="gender">
          {(field) => (
            <SingleSelect
              label="Legal gender"
              placeholder="Select gender"
              value={field.state.value}
              options={[
                { value: Gender.unknown, label: "Unknown" },
                { value: Gender.male, label: "Male" },
                { value: Gender.female, label: "Female" },
              ]}
              onChange={field.handleChange}
              onBlur={field.handleBlur}
            />
          )}
        </form.Field>
        <div className="flex justify-end gap-2">
          <Button type="reset" variant="outline">
            Reset
          </Button>

          <Button type="submit" variant="default" disabled={isPending}>
            Create member
          </Button>
        </div>
      </Form>
    </PageWrapper>
  );
}
