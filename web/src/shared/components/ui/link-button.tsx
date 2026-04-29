import { createLink, type LinkComponent } from "@tanstack/react-router";
import { type VariantProps } from "class-variance-authority";
import type { ComponentProps, Ref } from "react";

import { buttonVariants } from "#/shared/components/ui/button";
import { cn } from "#/shared/lib/utils";

type LinkButtonBaseProps = ComponentProps<"a"> &
  VariantProps<typeof buttonVariants> & {
    ref?: Ref<HTMLAnchorElement>;
  };

function LinkButtonBase({
  className,
  variant = "default",
  size = "default",
  ...props
}: LinkButtonBaseProps) {
  return (
    <a data-slot="button" className={cn(buttonVariants({ variant, size }), className)} {...props} />
  );
}

const CreatedLinkButton = createLink(LinkButtonBase);

export const LinkButton: LinkComponent<typeof LinkButtonBase> = (props) => {
  return <CreatedLinkButton preload="intent" {...props} />;
};
