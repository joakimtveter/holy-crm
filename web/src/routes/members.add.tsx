import { useForm } from "@tanstack/react-form";
import { createFileRoute, useNavigate } from "@tanstack/react-router";
import type { SubmitEvent } from "react";
import { toast } from "sonner";

import MemberForm from "#/domains/members/member.form.tsx";
import { Gender } from "#/domains/members/member.types.ts";
import {
  memberFormValidationSchema,
  type MemberFormValues,
} from "#/domains/members/members.schema.ts";
import { useCreateMember } from "#/domains/members/use-members.ts";
import PageWrapper from "#/shared/components/page-wrapper.tsx";

export const Route = createFileRoute("/members/add")({
  component: RouteComponent,
});

function RouteComponent() {
  const { mutate, isPending } = useCreateMember();
  const navigate = useNavigate();
  const form = useForm({
    defaultValues: {
      firstName: "",
      middleNames: undefined,
      lastName: "",
      dateOfBirth: "",
      gender: Gender.unknown,
    } as MemberFormValues,
    validators: {
      onSubmit: memberFormValidationSchema,
    },
    onSubmit: async ({ value }) => {
      if (isPending) return;
      mutate(value, {
        onSuccess: (member) => {
          form.reset();
          toast.success("Member created successfully.", {
            action: {
              label: "View member",
              onClick: () =>
                navigate({ to: "/members/$memberId", params: { memberId: member.id } }),
            },
          });
        },
      });
    },
  });

  const handleSubmit = (e: SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();
    form.handleSubmit();
  };

  return (
    <PageWrapper title="Add member">
      <MemberForm
        handleSubmit={handleSubmit}
        form={form}
        isPending={isPending}
        submitLabel={"Add member"}
      />
    </PageWrapper>
  );
}
