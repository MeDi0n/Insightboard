import {
  CartesianGrid,
  Line,
  LineChart,
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
      <LineChart data={data} margin={{ top: 8, right: 8, bottom: 0, left: -16 }}>
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
          cursor={{ stroke: theme.axisLine, strokeDasharray: "3 3" }}
          contentStyle={{
            background: theme.tooltipBg,
            border: `1px solid ${theme.tooltipBorder}`,
            borderRadius: 8,
            boxShadow: theme.tooltipShadow,
            fontSize: 13,
          }}
          labelStyle={{ color: theme.text }}
        />
        <Line
          dataKey={chart.y}
          stroke={theme.accent}
          strokeWidth={2.5}
          dot={{ r: 4, fill: theme.accent, strokeWidth: 0 }}
          activeDot={{ r: 5 }}
        />
      </LineChart>
    </ResponsiveContainer>
  );
};

export default Chart;
