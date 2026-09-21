import z from "zod";

export const UsageSummarySchema = z.object({
  totalCalls: z.number(),
  invalidCalls: z.number(),
  totalInputTokens: z.number(),
  totalOutputTokens: z.number(),
  wastedTokens: z.number(),
});

export type UsageSummary = z.infer<typeof UsageSummarySchema>;
