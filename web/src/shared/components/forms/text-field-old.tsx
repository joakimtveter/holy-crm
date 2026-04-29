import { useId } from "react";

import { Field, FieldDescription, FieldLabel } from "#/shared/components/ui/field.tsx";
import { Input } from "#/shared/components/ui/input.tsx";
import type { AutoCompleteTokens } from "#/shared/types/form.types.ts";

type TextFieldProps = {
  type?: "text" | "password" | "email" | "tel" | "url";
  id?: string;
  label: string;
  placeholder?: string;
  autoComplete?: AutoCompleteTokens;
  value: string;
  onChange: (value: string) => void;
  onBlur: () => void;
  helpText?: string;
  errorMessage?: string;
};
export default function TextField(props: TextFieldProps) {
  const {
    label,
    type = "text",
    id: providedId,
    placeholder,
    autoComplete = "on",
    value,
    onChange,
    onBlur,
    helpText,
    errorMessage,
  } = props;
  const generatedId = useId();
  const id = providedId ?? generatedId;

  const hasError = !!errorMessage;
  const renderDescription = !!helpText || hasError;

  return (
    <Field data-component="TextField">
      <FieldLabel htmlFor={id}>{label}</FieldLabel>
      <Input
        id={id}
        type={type}
        placeholder={placeholder}
        autoComplete={autoComplete}
        value={value}
        onChange={(e) => onChange?.(e.target.value)}
        onBlur={onBlur}
        aria-labelledby={id}
        aria-invalid={hasError}
      />
      {renderDescription && <FieldDescription>{errorMessage ?? helpText}</FieldDescription>}
    </Field>
  );
}
