import { useQuery } from "@tanstack/react-query";
import type { DashboardJob } from "../types/types";

export function useDashboard(id: string) {
  return useQuery<DashboardJob>({
    queryKey: ["dashboard", id],
    queryFn: () =>
      fetch(`http://localhost:5029/dashboards/${id}`).then((res) => res.json()),
    refetchInterval: (query) => {
      const status = query.state.data?.status;

      return status === "processing" ? 1000 : false;
    },
  });
}
