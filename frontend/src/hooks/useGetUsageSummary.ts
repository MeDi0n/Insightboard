import { useQuery } from "@tanstack/react-query";
import api from "../api/httpClient";
import { UsageSummarySchema } from "../models/UsageSummary";

export function useGetUsageSummary() {
  return useQuery({
    queryKey: ["usage"],
    queryFn: async () => {
      const res = await api.get("/usage");
      return UsageSummarySchema.parse(res.data);
    },
  });
}
