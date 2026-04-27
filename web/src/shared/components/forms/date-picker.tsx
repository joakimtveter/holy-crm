import { format, parse, isValid } from "date-fns";
import { CalendarIcon } from "lucide-react";
import { useEffect, useId, useState } from "react";

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

const ISO_FORMAT = "yyyy-MM-dd";
const DISPLAY_FORMAT = "dd.MM.yyyy";

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
  return format(date, ISO_FORMAT);
}

function parseISODate(value: string): Date | undefined {
  if (!value) return undefined;
  const date = parse(value, ISO_FORMAT, new Date());
  return isValid(date) ? date : undefined;
}

function parseDisplayDate(value: string): Date | undefined {
  if (!value) return undefined;
  const date = parse(value, DISPLAY_FORMAT, new Date());
  return isValid(date) ? date : undefined;
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
  const [month, setMonth] = useState<Date | undefined>(() => parseISODate(value));

  const generatedId = useId();
  const id = providedId ?? generatedId;

  const selectedDate = parseISODate(value);
  const hasError = !!errorMessage;
  const renderDescription = !!helpText || hasError;

  useEffect(() => {
    setInputValue(formatDate(value));
    const parsed = parseISODate(value);
    if (parsed) setMonth(parsed);
  }, [value]);

  const handleInputChange = (next: string) => {
    setInputValue(next);
    if (!next) {
      onChange("");
      return;
    }
    const parsed = parseDisplayDate(next);
    if (parsed) {
      setMonth(parsed);
      onChange(toISODate(parsed));
    }
  };

  const handleBlur = () => {
    if (inputValue && !parseDisplayDate(inputValue)) {
      setInputValue(formatDate(value));
    }
    onBlur?.();
  };

  return (
    <Field className="w-44" data-component="DatePicker">
      <FieldLabel htmlFor={id}>{label}</FieldLabel>
      <InputGroup>
        <InputGroupInput
          id={id}
          value={inputValue}
          placeholder={placeholder}
          onChange={(e) => handleInputChange(e.target.value)}
          onBlur={handleBlur}
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
                    setInputValue(format(date, DISPLAY_FORMAT));
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
