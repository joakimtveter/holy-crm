import { useState, useEffect } from "react";

import { type Theme, DEFAULT_THEME } from "#/shared/lib/theme.ts";

export function useTheme() {
  const [theme, setThemeState] = useState<Theme>(() => {
    if (typeof window === "undefined") return DEFAULT_THEME;
    return (localStorage.getItem("theme") as Theme) ?? DEFAULT_THEME;
  });

  function setTheme(next: Theme) {
    setThemeState(next);
    document.documentElement.setAttribute("data-theme", next);
    localStorage.setItem("theme", next);
  }

  useEffect(() => {
    document.documentElement.setAttribute("data-theme", theme);
  }, []);

  return { theme, setTheme };
}
