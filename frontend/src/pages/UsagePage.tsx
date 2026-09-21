import { useNavigate } from "react-router-dom";
import ErrorState from "../components/ErrorState/ErrorState";
import UsageSummaryView from "../components/UsageSummaryView/UsageSummaryView";
import { useGetUsageSummary } from "../hooks/useGetUsageSummary";

const UsagePage = () => {
  const { data, isLoading, error } = useGetUsageSummary();
  const navigate = useNavigate();

  if (isLoading) {
    return null;
  }

  if (error) {
    return (
      <ErrorState
        title="Could not load usage"
        text="The server did not return the usage summary. Try again in a moment."
        actionLabel="Back to upload"
        onRetry={() => navigate("/")}
      />
    );
  }

  if (!data) {
    return null;
  }

  return <UsageSummaryView summary={data} />;
};
export default UsagePage;
