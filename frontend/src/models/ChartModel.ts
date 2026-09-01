import z from "zod";

export const ChartSchema = z.object({
  type: z.string(),
  title: z.string(),
  x: z.string(),
  y: z.string(),
});

export type ChartSpec = z.infer<typeof ChartSchema>;
