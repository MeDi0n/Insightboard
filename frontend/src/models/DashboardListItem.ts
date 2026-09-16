import z from "zod";
import { DashboardStatus } from "./DashboardModel";

export const DashboardListItemSchema = z.object({
  id: z.string(),
  fileName: z.string(),
  createdAt: z.string(),
  status: z.enum(DashboardStatus),
});

export type DashboardListItem = z.infer<typeof DashboardListItemSchema>;
