import { useQuery } from "@tanstack/react-query";
import api from "../api/httpClient";
import type { DashboardJob } from "../types/types";

export function useDashboard(id: string) {
  return useQuery<DashboardJob>({
    queryKey: ["dashboard", id],
    queryFn: async () => {
      const res = await api.get<DashboardJob>(`/dashboards/${id}`);
      const data = res.data;
      return data;
    },
    refetchInterval: (query) => {
      const status = query.state.data?.status;

      return status === "processing" ? 1000 : false;
    },
  });
}
