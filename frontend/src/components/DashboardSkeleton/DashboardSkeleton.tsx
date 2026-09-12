import "./DashboardSkeleton.css";

const DashboardSkeleton = () => {
  return (
    <div className="dashboard-skeleton" aria-busy="true">
      <div className="skeleton-grid">
        {[0, 1].map((i) => (
          <div className="skeleton-card" key={i}>
            <div className="skeleton-line" />
            <div className="skeleton-block" />
          </div>
        ))}
      </div>
    </div>
  );
};

export default DashboardSkeleton;
