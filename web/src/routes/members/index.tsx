import { createFileRoute, Link } from "@tanstack/react-router";
import {
  createColumnHelper,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";

import type { MemberBrief } from "#/domains/members/member.type.ts";
import { useMembers } from "#/domains/members/useMembers.ts";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "#/shared/components/ui/table";

export const Route = createFileRoute("/members/")({
  component: RouteComponent,
});

const columnHelper = createColumnHelper<MemberBrief>();

const columns = [
  columnHelper.accessor(
    (row) => [row.firstName, row.middleName, row.lastName].filter(Boolean).join(" "),
    {
      id: "name",
      header: "Name",
      cell: ({ row }) => (
        <Link
          to="/members/$id"
          params={{ id: row.original.id }}
          className="font-medium hover:underline"
        >
          {[row.original.firstName, row.original.middleName, row.original.lastName]
            .filter(Boolean)
            .join(" ")}
        </Link>
      ),
    },
  ),
  columnHelper.accessor("dateOfBirth", {
    header: "Date of birth",
    cell: ({ getValue }) =>
      new Date(getValue()).toLocaleDateString(undefined, {
        year: "numeric",
        month: "short",
        day: "numeric",
      }),
  }),
];

function RouteComponent() {
  const { data: members = [] } = useMembers();

  const table = useReactTable({
    data: members,
    columns,
    getCoreRowModel: getCoreRowModel(),
  });

  return (
    <div className="p-6">
      <h1 className="mb-6 text-2xl font-semibold">Members</h1>
      <div className="bg-card rounded-lg border">
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
      </div>
    </div>
  );
}
