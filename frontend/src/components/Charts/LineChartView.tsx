import {
  Line,
  LineChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import type { ChartProps } from "../../types/types";
import { CHART_COLOR } from "./chartTheme";

const Chart = ({ chart, data }: ChartProps) => {
  return (
    <ResponsiveContainer width="100%" height={300}>
      <LineChart data={data}>
        <XAxis dataKey={chart.x} />
        <YAxis />
        <Tooltip />
        <Line dataKey={chart.y} stroke={CHART_COLOR} />
      </LineChart>
    </ResponsiveContainer>
  );
};

export default Chart;
