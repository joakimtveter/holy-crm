import { CalendarIcon } from "lucide-react";
import { useId, useState } from "react";

import { Calendar } from "#/shared/components/ui/calendar.tsx";
import { Field, FieldDescription, FieldLabel } from "#/shared/components/ui/field.tsx";
import {
  InputGroup,
  InputGroupAddon,
  InputGroupButton,
  InputGroupInput,
} from "#/shared/components/ui/input-group.tsx";
import { Popover, PopoverContent, PopoverTrigger } from "#/shared/components/ui/popover.tsx";
import { formatDate } from "#/shared/lib/datetime.ts";

type DatePickerProps = {
  label: string;
  id?: string;
  value: string;
  placeholder?: string;
  onChange: (isoDate: string) => void;
  onBlur?: () => void;
  helpText?: string;
  errorMessage?: string;
};

function toISODate(date: Date): string {
  return date.toISOString().split("T")[0];
}

function parseDate(value: string): Date | undefined {
  if (!value) return undefined;
  const d = new Date(value);
  return isNaN(d.getTime()) ? undefined : d;
}

export function DatePicker(props: DatePickerProps) {
  const {
    label,
    id: providedId,
    placeholder = "dd.mm.yyyy",
    value,
    onChange,
    onBlur,
    helpText,
    errorMessage,
  } = props;
  const [open, setOpen] = useState(false);
  const [inputValue, setInputValue] = useState(() => formatDate(value));
  const [month, setMonth] = useState<Date | undefined>(() => parseDate(value));

  const generatedId = useId();
  const id = providedId ?? generatedId;

  const selectedDate = parseDate(value);
  const hasError = !!errorMessage;
  const renderDescription = !!helpText || hasError;

  return (
    <Field className="w-44" data-component="DatePicker">
      <FieldLabel htmlFor={id}>{label}</FieldLabel>
      <InputGroup>
        <InputGroupInput
          id={id}
          value={inputValue}
          placeholder={placeholder}
          onChange={(e) => {
            setInputValue(e.target.value);
            const parsed = parseDate(e.target.value);
            if (parsed) {
              setMonth(parsed);
              onChange(toISODate(parsed));
            } else {
              onChange("");
            }
          }}
          onBlur={onBlur}
          onKeyDown={(e) => {
            if (e.key === "ArrowDown") {
              e.preventDefault();
              setOpen(true);
            }
          }}
          aria-invalid={hasError}
        />
        <InputGroupAddon align="inline-end">
          <Popover open={open} onOpenChange={setOpen}>
            <PopoverTrigger
              render={
                <InputGroupButton variant="ghost" size="icon-xs" aria-label="Select date">
                  <CalendarIcon />
                  <span className="sr-only">Select date</span>
                </InputGroupButton>
              }
            />
            <PopoverContent
              className="w-auto overflow-hidden p-0"
              align="end"
              alignOffset={-8}
              sideOffset={10}
            >
              <Calendar
                mode="single"
                captionLayout="dropdown"
                startMonth={new Date(1900, 0)}
                endMonth={new Date(new Date().getFullYear() + 5, 11)}
                selected={selectedDate}
                month={month}
                onMonthChange={setMonth}
                onSelect={(date) => {
                  if (date) {
                    const iso = toISODate(date);
                    onChange(iso);
                    setInputValue(formatDate(iso));
                    setMonth(date);
                  }
                  setOpen(false);
                }}
              />
            </PopoverContent>
          </Popover>
        </InputGroupAddon>
      </InputGroup>
      {renderDescription && <FieldDescription>{errorMessage ?? helpText}</FieldDescription>}
    </Field>
  );
}
