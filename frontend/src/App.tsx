import "./App.css";
import { useDashboard } from "./hooks/useDashboard";

function App() {
  const { data, isLoading, isError } = useDashboard();
  if (isLoading) return <p>Loading...</p>;
  if (isError) return <p>Error</p>;
  if (!data) return null;

  return data.charts.map((chart, i) => (
    <div key={i}>
      <h2>{chart.title}</h2>
      <p>
        {chart.type}: {chart.x} / {chart.y}{" "}
      </p>
    </div>
  ));
}

export default App;
