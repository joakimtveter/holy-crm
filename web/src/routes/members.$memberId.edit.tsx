import { useForm } from "@tanstack/react-form";
import { createFileRoute, useNavigate } from "@tanstack/react-router";
import { toast } from "sonner";

import MemberForm from "#/domains/members/member.form.tsx";
import { Gender } from "#/domains/members/member.types.ts";
import {
  memberFormValidationSchema,
  type MemberFormValues,
} from "#/domains/members/members.schema.ts";
import { useMemberById, useUpdateMember } from "#/domains/members/use-members.ts";
import PageWrapper from "#/shared/components/page-wrapper.tsx";
import ErrorPage from "#/shared/pages/error-page.tsx";
import LoadingPage from "#/shared/pages/loading-page.tsx";

export const Route = createFileRoute("/members/$memberId/edit")({
  component: RouteComponent,
});

function RouteComponent() {
  const { memberId } = Route.useParams();
  const { data: member, isPending, error, isError } = useMemberById(memberId);
  const { mutate, isPending: isMutationPending } = useUpdateMember(memberId);
  const navigate = useNavigate();
  const form = useForm({
    defaultValues: {
      firstName: member?.firstName ?? "",
      middleNames: member?.middleNames ?? "",
      lastName: member?.lastName ?? "",
      dateOfBirth: member?.dateOfBirth ?? "",
      gender: member?.gender ?? Gender.unknown,
    } as MemberFormValues,
    validators: {
      onSubmit: memberFormValidationSchema,
    },
    onSubmit: async ({ value }) => {
      if (isMutationPending) return;
      mutate(value, {
        onSuccess: (member) => {
          form.reset();
          toast.success("Member updated successfully.", {
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

  if (isPending) return <LoadingPage />;
  if (isError) return <ErrorPage error={error} />;

  return (
    <PageWrapper title={`Edit member: ${member?.firstName}`}>
      <MemberForm form={form} isPending={isMutationPending} submitLabel="Update member" />
      <pre>{JSON.stringify(form.state, null, 2)}</pre>
    </PageWrapper>
  );
}
