import { TriangleAlert } from "lucide-react";
import "./ErrorState.css";

type Props = {
  onRetry: () => void;
};

const ErrorState = ({ onRetry }: Props) => {
  return (
    <div className="error-state">
      <div className="error-icon">
        <TriangleAlert />
      </div>
      <h2 className="error-title">Could not build the dashboard</h2>
      <p className="error-text">
        AI failed to produce a valid result after 3 attempts. Check the file
        format and try again.
      </p>
      <button className="error-button" onClick={onRetry}>
        Upload another file
      </button>
    </div>
  );
};

export default ErrorState;
