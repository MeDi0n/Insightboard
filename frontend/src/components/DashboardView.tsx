import { useDashboard } from "../hooks/useDashboard";
import type { DashboardViewProps } from "../types/types";
import Chart from "./Chart";

const DashboardView = ({ id }: DashboardViewProps) => {
  const { data, isLoading, isError } = useDashboard(id);

  if (isLoading) return <p>Loading...</p>;
  if (isError) return <p>Error</p>;
  if (!data) return null;

  if (data.status === "processing") return <p>генерируем...</p>;
  if (data.status === "failed") return <p>попробуйте еще раз</p>;

  const spec = data.spec;
  if (!spec) return null;

  return spec?.charts.map((chart, i) => (
    <div key={i}>
      <h2>{chart.title}</h2>
      <Chart chart={chart} data={spec.data} />
    </div>
  ));
};

export default DashboardView;
