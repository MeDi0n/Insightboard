import { useDashboard } from "../../hooks/useDashboard";
import type { DashboardViewProps } from "../../types/types";
import Chart from "../Charts/Chart";
import ErrorState from "../ErrorState/ErrorState";
import GeneratingState from "../GeneratingState/GeneratingState";
import "./DashboardView.css";

const DashboardView = ({ id, onReset }: DashboardViewProps) => {
  const { data, isLoading, isError } = useDashboard(id);

  if (isLoading || data?.status === "processing") return <GeneratingState />;

  if (isError)
    return (
      <ErrorState
        actionLabel="Start over"
        title="Could not reach the server"
        text="The request failed. Check your connection and try again."
        onRetry={onReset}
      />
    );

  if (!data) return null;

  if (data.status === "failed")
    return (
      <ErrorState
        actionLabel="Upload another file"
        title="Could not build the dashboard"
        text="AI failed to produce a valid result after 3 attempts. Check the file format and try again."
        onRetry={onReset}
      />
    );

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
