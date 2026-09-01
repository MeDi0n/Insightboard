import type { ChartSpec } from "../../models/ChartModel";
import BarChartView from "./BarChartView";
import LineChartView from "./LineChartView";

export type ChartProps = { chart: ChartSpec; data: Record<string, string>[] };

const Chart = ({ chart, data }: ChartProps) => {
  return chart.type === "line" ? (
    <LineChartView chart={chart} data={data} />
  ) : (
    <BarChartView chart={chart} data={data} />
  );
};

export default Chart;
