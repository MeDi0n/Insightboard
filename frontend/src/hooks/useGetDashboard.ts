import { useQuery } from "@tanstack/react-query";
import { isAxiosError } from "axios";
import { ZodError } from "zod";
import api from "../api/httpClient";
import {
  DashboardSchema,
  DashboardStatus,
  type Dashboard,
} from "../models/DashboardModel";

export function useGetDashboard(id: string) {
  return useQuery<Dashboard>({
    queryKey: ["dashboard", id],
    queryFn: async () => {
      const res = await api.get(`/dashboards/${id}`);
      return DashboardSchema.parse(res.data);
    },
    retry: (failureCount, error) => {
      if (error instanceof ZodError) return false;
      if (isAxiosError(error) && error.response && error.response.status < 500)
        return false;
      return failureCount < 3;
    },
    refetchInterval: (query) => {
      const status = query.state.data?.status;

      return status === DashboardStatus.Processing ? 1000 : false;
    },
  });
}
