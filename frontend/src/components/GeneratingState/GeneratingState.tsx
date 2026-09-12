import DashboardSkeleton from "../DashboardSkeleton/DashboardSkeleton";
import "./GeneratingState.css";

const GeneratingState = () => {
  return (
    <div className="generating">
      <span className="generating-badge">
        <span className="generating-dot" />
        Generating dashboard...
      </span>
      <p className="generating-text">
        AI is analysing the data structure and picking chart types
      </p>

      <DashboardSkeleton />
    </div>
  );
};

export default GeneratingState;
