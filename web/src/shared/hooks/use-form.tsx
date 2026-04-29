import { createFormHook } from "@tanstack/react-form";

import { DatetimePicker } from "#/shared/components/forms/datetime-picker.tsx";
import TextField from "#/shared/components/forms/text-field.tsx";
import VenuePicker from "#/shared/components/forms/venue-picker.tsx";
import { fieldContext, formContext } from "#/shared/hooks/form-context";

export const { useAppForm } = createFormHook({
  fieldContext,
  formContext,
  fieldComponents: {
    TextField,
    DatetimePicker,
    VenuePicker,
  },
  formComponents: {},
});
