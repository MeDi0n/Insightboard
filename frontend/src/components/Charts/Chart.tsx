import { ChartType, type ChartSpec } from "../../models/ChartModel";
import BarChartView from "./BarChartView";
import LineChartView from "./LineChartView";

export type ChartProps = { chart: ChartSpec; data: Record<string, string>[] };

const Chart = ({ chart, data }: ChartProps) => {
  switch (chart.type) {
    case ChartType.Line:
      return <LineChartView chart={chart} data={data} />;
    case ChartType.Bar:
      return <BarChartView chart={chart} data={data} />;
    default: {
      const unhandled: never = chart.type;
      console.error("Unknown chart type:", unhandled);
      return null;
    }
  }
};

export default Chart;
