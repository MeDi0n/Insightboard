import { useNavigate } from "react-router-dom";
import { useGetDashboard } from "../../hooks/useGetDashboard";
import { DashboardStatus } from "../../models/DashboardModel";
import Chart from "../Charts/Chart";
import DashboardSkeleton from "../DashboardSkeleton/DashboardSkeleton";
import ErrorState from "../ErrorState/ErrorState";
import GeneratingState from "../GeneratingState/GeneratingState";
import "./DashboardView.css";
import { getDashboardError } from "./getDashboardError";

export type DashboardViewProps = { id: string };

const DashboardView = ({ id }: DashboardViewProps) => {
  const { data, isLoading, error } = useGetDashboard(id);
  const navigate = useNavigate();
  const errorInfo = getDashboardError(error, data);

  if (isLoading) return <DashboardSkeleton />;

  if (data?.status === DashboardStatus.Processing) return <GeneratingState />;

  if (errorInfo)
    return (
      <ErrorState
        title={errorInfo.title}
        text={errorInfo.text}
        actionLabel="Upload another file"
        onRetry={() => navigate("/")}
      />
    );

  if (!data?.spec)
    return (
      <ErrorState
        title="Dashboard is empty"
        text="Something went wrong while building it. Try uploading the file again."
        actionLabel="Upload another file"
        onRetry={() => navigate("/")}
      />
    );

  const spec = data.spec;

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
