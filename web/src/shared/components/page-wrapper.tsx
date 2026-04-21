import type { ReactNode } from "react";

import Heading from "#/shared/components/heading.tsx";

type PageWrapperProps = {
  title: string;
  children?: ReactNode;
};

export default function PageWrapper(props: PageWrapperProps) {
  const { title, children } = props;
  return (
    <main className="m-auto w-full px-8 py-6">
      <Heading level={1} size="xl">
        {title}
      </Heading>
      {children}
    </main>
  );
}
