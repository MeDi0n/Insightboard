import { useEffect, useState } from "react";

export type Colors = "light" | "dark";

const useTheme = () => {
  const [colorTheme, setColorTheme] = useState<Colors>(() => {
    const savedColor = localStorage.getItem("colorTheme");

    if (savedColor === "dark") {
      return "dark";
    } else {
      return "light";
    }
  });

  useEffect(() => {
    document.documentElement.setAttribute("data-theme", colorTheme);
    localStorage.setItem("colorTheme", colorTheme);
  }, [colorTheme]);

  return [colorTheme, setColorTheme] as const;
};

export default useTheme;
