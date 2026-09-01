import z from "zod";

export const ChartType = {
  Line: "line",
  Bar: "bar",
} as const;

export const ChartSchema = z.object({
  type: z.enum(ChartType),
  title: z.string(),
  x: z.string(),
  y: z.string(),
});

export type ChartSpec = z.infer<typeof ChartSchema>;
