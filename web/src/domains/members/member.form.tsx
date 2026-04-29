import type { ReactFormExtendedApi } from "@tanstack/react-form";
import type { SubmitEvent } from "react";

import { Gender } from "#/domains/members/member.types.ts";
import BirthdayPicker from "#/shared/components/forms/birthday-picker.tsx";
import Form from "#/shared/components/forms/form.tsx";
import SingleSelect from "#/shared/components/forms/single-select.tsx";
import TextField from "#/shared/components/forms/text-field.tsx";
import { Button } from "#/shared/components/ui/button.tsx";

type MemberFormProps = {
  form: ReactFormExtendedApi<any, any, any, any, any, any, any, any, any, any, any, any>;
  isPending: boolean;
  submitLabel?: string;
};

export default function MemberForm(props: MemberFormProps) {
  const { form, isPending, submitLabel = "Submit" } = props;

  const handleSubmit = (e: SubmitEvent<HTMLFormElement>) => {
    console.log(e);
    e.preventDefault();
    form.handleSubmit();
  };

  return (
    <Form onSubmit={handleSubmit} data-component="MemberForm">
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
      </form.Field>
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
        {(field) => {
          const firstError = field.state.meta.errors[0];
          const errorMessage = typeof firstError === "string" ? firstError : firstError?.message;
          return (
            <BirthdayPicker
              label="Date of birth"
              value={field.state.value}
              onChange={field.handleChange}
              onBlur={field.handleBlur}
              helpText={""}
              errorMessage={errorMessage}
            />
          );
        }}
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

        <form.Subscribe selector={(state) => state.isSubmitting}>
          {(isSubmitting) => (
            <Button type="submit" variant="default" disabled={isPending || isSubmitting}>
              {submitLabel}
            </Button>
          )}
        </form.Subscribe>
      </div>
    </Form>
  );
}
