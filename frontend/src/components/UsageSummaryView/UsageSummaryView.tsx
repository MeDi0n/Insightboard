import type { UsageSummary } from "../../models/UsageSummary";
import "./UsageSummaryView.css";

type UsageSummaryViewProps = {
  summary: UsageSummary;
};

const compact = new Intl.NumberFormat(undefined, {
  notation: "compact",
  maximumFractionDigits: 1,
});

function formatPercent(part: number, whole: number) {
  if (whole === 0) return "0%";
  return `${Math.round((part / whole) * 100)}%`;
}

const UsageSummaryView = ({ summary }: UsageSummaryViewProps) => {
  const totalTokens = summary.totalInputTokens + summary.totalOutputTokens;

  return (
    <section className="page-width usage">
      <h1 className="usage-title">AI usage</h1>
      <p className="usage-subtitle">
        Every request to the model since the database was created
      </p>

      <div className="usage-grid">
        <div className="usage-tile">
          <span className="usage-label">Requests to the model</span>
          <span className="usage-value">{compact.format(summary.totalCalls)}</span>
          <span className="usage-note">including retries</span>
        </div>

        <div className="usage-tile">
          <span className="usage-label">Tokens used</span>
          <span className="usage-value">{compact.format(totalTokens)}</span>
          <span className="usage-note">
            {compact.format(summary.totalInputTokens)} in ·{" "}
            {compact.format(summary.totalOutputTokens)} out
          </span>
        </div>

        <div className="usage-tile">
          <span className="usage-label">Tokens spent on rejected answers</span>
          <span className="usage-value">{compact.format(summary.wastedTokens)}</span>
          <span className="usage-note">
            {formatPercent(summary.wastedTokens, totalTokens)} of all tokens
          </span>
        </div>

        <div className="usage-tile">
          <span className="usage-label">Rejected answers</span>
          <span className="usage-value">{compact.format(summary.invalidCalls)}</span>
          <span className="usage-note">
            {formatPercent(summary.invalidCalls, summary.totalCalls)} of requests
            failed validation
          </span>
        </div>
      </div>
    </section>
  );
};

export default UsageSummaryView;
