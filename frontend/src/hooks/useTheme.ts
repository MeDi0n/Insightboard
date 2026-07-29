import { useEffect, useState } from "react";

type Сolors = "light" | "dark";

const useTheme = () => {
  const [colorTheme, setColorTheme] = useState<Сolors>(() => {
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
