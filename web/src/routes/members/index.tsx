import { createFileRoute, Link } from "@tanstack/react-router";
import {
  createColumnHelper,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";

import type { MemberBrief } from "#/domains/members/member.types.ts";
import { useMembers } from "#/domains/members/use-members.ts";
import PageWrapper from "#/shared/components/page-wrapper.tsx";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "#/shared/components/ui/table";
import { formatDate } from "#/shared/lib/datetime.ts";
import ErrorPage from "#/shared/pages/error-page.tsx";
import LoadingPage from "#/shared/pages/loading-page.tsx";

export const Route = createFileRoute("/members/")({
  component: RouteComponent,
});

const columnHelper = createColumnHelper<MemberBrief>();

const columns = [
  columnHelper.accessor(
    (row) => [row.firstName, row.middleNames, row.lastName].filter(Boolean).join(" "),
    {
      id: "name",
      header: "Name",
      cell: ({ row }) => (
        <Link
          to="/members/$memberId"
          params={{ memberId: row.original.id }}
          className="font-medium hover:underline"
        >
          {[row.original.firstName, row.original.middleNames, row.original.lastName]
            .filter(Boolean)
            .join(" ")}
        </Link>
      ),
    },
  ),
  columnHelper.accessor("dateOfBirth", {
    header: "Date of birth",
    cell: ({ getValue }) => formatDate(getValue()),
  }),
];

function RouteComponent() {
  const { data, isError, error } = useMembers();

  const table = useReactTable({
    data: data?.items ?? [],
    columns,
    getCoreRowModel: getCoreRowModel(),
  });

  if (data) {
    return (
      <PageWrapper title="Members">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <TableHead key={header.id}>
                    {flexRender(header.column.columnDef.header, header.getContext())}
                  </TableHead>
                ))}
              </TableRow>
            ))}
          </TableHeader>
          <TableBody>
            {table.getRowModel().rows.length > 0 ? (
              table.getRowModel().rows.map((row) => (
                <TableRow key={row.id}>
                  {row.getVisibleCells().map((cell) => (
                    <TableCell key={cell.id}>
                      {flexRender(cell.column.columnDef.cell, cell.getContext())}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell
                  colSpan={columns.length}
                  className="text-muted-foreground py-10 text-center"
                >
                  No members found.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </PageWrapper>
    );
  }

  if (isError) return <ErrorPage error={error} />;

  return <LoadingPage title="Members" />;
}
