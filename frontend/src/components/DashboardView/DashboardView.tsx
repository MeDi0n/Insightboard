import { useDashboard } from "../../hooks/useDashboard";
import type { DashboardViewProps } from "../../types/types";
import Chart from "../Charts/Chart";
import ErrorState from "../ErrorState/ErrorState";
import "./DashboardView.css";

const DashboardView = ({ id, onReset }: DashboardViewProps) => {
  const { data, isLoading, isError } = useDashboard(id);

  if (isLoading) return <p>Loading...</p>;
  if (isError) return <p>Error</p>;
  if (!data) return null;

  if (data.status === "processing") return <p>генерируем...</p>;
  if (data.status === "failed") return <ErrorState onRetry={onReset} />;

  const spec = data.spec;
  if (!spec) return null;

  return (
    <div className="page-width">
      <div className="dashboard-grid">
        {spec?.charts?.map((chart, i) => (
          <div className="chart-card" key={i}>
            <h2 className="chart-title">{chart.title}</h2>
            <Chart chart={chart} data={spec.data} />
          </div>
        ))}
      </div>
    </div>
  );
};

export default DashboardView;
