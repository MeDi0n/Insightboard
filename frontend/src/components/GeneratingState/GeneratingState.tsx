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

      <div className="generating-grid">
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

export default GeneratingState;
