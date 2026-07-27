import { Line, LineChart, Tooltip, XAxis, YAxis } from "recharts";
import type { ChartProps } from "../../types/types";

const Chart = ({ chart, data }: ChartProps) => {
  return (
    <LineChart width={500} height={300} data={data}>
      <XAxis dataKey={chart.x} />
      <YAxis />
      <Tooltip />
      <Line dataKey={chart.y} stroke="#8884d8" />
    </LineChart>
  );
};

export default Chart;
