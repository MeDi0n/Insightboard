import { useQuery } from "@tanstack/react-query";
import z from "zod";
import api from "../api/httpClient";
import { DashboardListItemSchema } from "../models/DashboardListItem";

export function useGetDashboardList() {
  return useQuery({
    queryKey: ["dashboards"],
    queryFn: async () => {
      const res = await api.get(`/dashboards`);
      return z.array(DashboardListItemSchema).parse(res.data);
    },
  });
}
