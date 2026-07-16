import { useQuery } from "@tanstack/react-query";
import type { DashboardSpec } from "../types/types";

export function useDashboard() {
  return useQuery<DashboardSpec>({
    queryKey: ["dashboard"],
    queryFn: () =>
      fetch("http://localhost:5029/dashboards").then((res) => res.json()),
  });
}
