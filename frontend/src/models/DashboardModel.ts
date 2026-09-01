import z from "zod";
import { ChartSchema } from "./ChartModel";

export const DashboardSpecSchema = z.object({
  charts: z.array(ChartSchema),
  data: z.array(z.record(z.string(), z.string())),
});

export const DashboardStatus = {
  Processing: "processing",
  Done: "done",
  Failed: "failed",
} as const;

export const DashboardSchema = z.object({
  status: z.enum(DashboardStatus),
  spec: DashboardSpecSchema.nullable(),
});

export type Dashboard = z.infer<typeof DashboardSchema>;
