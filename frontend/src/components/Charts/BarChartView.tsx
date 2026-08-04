import {
  Bar,
  BarChart,
  CartesianGrid,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import type { ChartProps } from "../../types/types";
import { useChartTheme } from "./chartTheme";

const Chart = ({ chart, data }: ChartProps) => {
  const theme = useChartTheme();

  return (
    <ResponsiveContainer width="100%" height={300}>
      <BarChart
        data={data}
        margin={{ top: 8, right: 8, bottom: 0, left: -16 }}
        barCategoryGap="25%"
      >
        <CartesianGrid
          stroke={theme.grid}
          strokeDasharray="3 3"
          vertical={false}
        />
        <XAxis
          dataKey={chart.x}
          stroke={theme.axisLine}
          tickLine={false}
          tick={{ fill: theme.axisText, fontSize: 12 }}
        />
        <YAxis
          stroke={theme.axisLine}
          axisLine={false}
          tickLine={false}
          tick={{ fill: theme.axisText, fontSize: 11 }}
        />
        <Tooltip
          cursor={{ fill: theme.grid, opacity: 0.45 }}
          contentStyle={{
            background: theme.tooltipBg,
            border: `1px solid ${theme.tooltipBorder}`,
            borderRadius: 8,
            boxShadow: theme.tooltipShadow,
            fontSize: 13,
          }}
          labelStyle={{ color: theme.text }}
        />
        <Bar
          dataKey={chart.y}
          fill={theme.accent}
          radius={[4, 4, 0, 0]}
          maxBarSize={48}
        />
      </BarChart>
    </ResponsiveContainer>
  );
};

export default Chart;
