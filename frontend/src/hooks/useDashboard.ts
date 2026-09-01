import { useQuery } from "@tanstack/react-query";
import api from "../api/httpClient";
import {
  DashboardSchema,
  DashboardStatus,
  type Dashboard,
} from "../models/DashboardModel";

export function useDashboard(id: string) {
  return useQuery<Dashboard>({
    queryKey: ["dashboard", id],
    queryFn: async () => {
      const res = await api.get(`/dashboards/${id}`);
      return DashboardSchema.parse(res.data);
    },
    refetchInterval: (query) => {
      const status = query.state.data?.status;

      return status === DashboardStatus.Processing ? 1000 : false;
    },
  });
}
