import {
  Bar,
  BarChart,
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
      <BarChart data={data}>
        <XAxis dataKey={chart.x} />
        <YAxis />
        <Tooltip />
        <Bar dataKey={chart.y} fill={CHART_COLOR} />
      </BarChart>
    </ResponsiveContainer>
  );
};

export default Chart;
