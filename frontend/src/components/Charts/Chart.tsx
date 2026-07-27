import type { ChartProps } from "../../types/types";
import BarChartView from "./BarChartView";
import LineChartView from "./LineChartView";

const Chart = ({ chart, data }: ChartProps) => {
  return chart.type === "line" ? (
    <LineChartView chart={chart} data={data} />
  ) : (
    <BarChartView chart={chart} data={data} />
  );
};

export default Chart;
