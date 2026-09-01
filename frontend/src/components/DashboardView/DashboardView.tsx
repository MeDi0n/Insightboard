import { useNavigate } from "react-router-dom";
import { useDashboard } from "../../hooks/useDashboard";
import { DashboardStatus } from "../../models/DashboardModel";
import Chart from "../Charts/Chart";
import ErrorState from "../ErrorState/ErrorState";
import GeneratingState from "../GeneratingState/GeneratingState";
import "./DashboardView.css";

export type DashboardViewProps = { id: string };

const DashboardView = ({ id }: DashboardViewProps) => {
  const { data, isLoading, isError } = useDashboard(id);
  const navigate = useNavigate();

  if (isLoading || data?.status === DashboardStatus.Processing)
    return <GeneratingState />;

  if (isError)
    return (
      <ErrorState
        actionLabel="Start over"
        title="Could not reach the server"
        text="The request failed. Check your connection and try again."
        onRetry={() => navigate("/")}
      />
    );

  if (!data) return null;

  if (data.status === DashboardStatus.Failed)
    return (
      <ErrorState
        actionLabel="Upload another file"
        title="Could not build the dashboard"
        text="AI failed to produce a valid result after 3 attempts. Check the file format and try again."
        onRetry={() => navigate("/")}
      />
    );

  const spec = data.spec;
  if (!spec) return null;

  return (
    <div className="page-width">
      <div className="dashboard-grid">
        {spec?.charts?.map((chart, i) => (
          <div className="chart-card" key={i}>
            <div className="chart-card-header">
              <h2 className="chart-title">{chart.title}</h2>
              <span className="chart-badge">{chart.type}</span>
            </div>
            <Chart chart={chart} data={spec.data} />
          </div>
        ))}
      </div>
    </div>
  );
};

export default DashboardView;
