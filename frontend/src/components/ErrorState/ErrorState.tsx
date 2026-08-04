import { TriangleAlert } from "lucide-react";
import "./ErrorState.css";

type Props = {
  title: string;
  text: string;
  actionLabel: string;
  onRetry: () => void;
};

const ErrorState = ({ title, text, actionLabel, onRetry }: Props) => {
  return (
    <div className="error-state">
      <div className="error-icon">
        <TriangleAlert />
      </div>
      <h2 className="error-title">{title}</h2>
      <p className="error-text">{text}</p>
      <button className="error-button" onClick={onRetry}>
        {actionLabel}
      </button>
    </div>
  );
};

export default ErrorState;
