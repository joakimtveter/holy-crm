import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/members/add")({
  component: RouteComponent,
});

function RouteComponent() {
  return <div>Hello "/members/add"!</div>;
}
