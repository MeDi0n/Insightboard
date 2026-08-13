import { useQuery } from "@tanstack/react-query";
import { API_URL } from "../config";
import type { DashboardJob } from "../types/types";

export function useDashboard(id: string) {
  return useQuery<DashboardJob>({
    queryKey: ["dashboard", id],
    queryFn: () =>
      fetch(`${API_URL}/dashboards/${id}`).then((res) => res.json()),
    refetchInterval: (query) => {
      const status = query.state.data?.status;

      return status === "processing" ? 1000 : false;
    },
  });
}
