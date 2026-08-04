import { useSyncExternalStore } from "react";

type ChartTheme = {
  accent: string;
  grid: string;
  axisText: string;
  axisLine: string;
  text: string;
  tooltipBg: string;
  tooltipBorder: string;
  tooltipShadow: string;
};

// Зеркало токенов --chart-* из index.css. Recharts берёт цвета из JS-пропсов,
// подставить туда var(--...) нельзя, поэтому значения продублированы здесь.
const light: ChartTheme = {
  accent: "#4f46e5",
  grid: "#e5e7eb",
  axisText: "#64748b",
  axisLine: "#cbd5e1",
  text: "#0f172a",
  tooltipBg: "#ffffff",
  tooltipBorder: "#e5e7eb",
  tooltipShadow: "0 4px 16px rgba(15, 23, 42, 0.12)",
};

const dark: ChartTheme = {
  accent: "#818cf8",
  grid: "#232830",
  axisText: "#9aa4b2",
  axisLine: "#3a3f4a",
  text: "#e5e7eb",
  tooltipBg: "#1c2029",
  tooltipBorder: "#2c313b",
  tooltipShadow: "0 8px 24px rgba(0, 0, 0, 0.5)",
};

const subscribe = (onChange: () => void) => {
  const observer = new MutationObserver(onChange);
  observer.observe(document.documentElement, {
    attributes: true,
    attributeFilter: ["data-theme"],
  });
  return () => observer.disconnect();
};

const getSnapshot = () => document.documentElement.getAttribute("data-theme");

export function useChartTheme(): ChartTheme {
  const theme = useSyncExternalStore(subscribe, getSnapshot);
  return theme === "dark" ? dark : light;
}
