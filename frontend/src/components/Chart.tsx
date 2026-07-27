import { Bar, BarChart, Tooltip, XAxis, YAxis } from "recharts";
import type { ChartSpec } from "../types/types";

type ChartProps = { chart: ChartSpec; data: Record<string, string>[] };

const Chart = ({ chart, data }: ChartProps) => {
  return (
    <BarChart width={500} height={300} data={data}>
      <XAxis dataKey={chart.x} />
      <YAxis />
      <Tooltip />
      <Bar dataKey={chart.y} fill="#8884d8" />
    </BarChart>
  );
};

export default Chart;
