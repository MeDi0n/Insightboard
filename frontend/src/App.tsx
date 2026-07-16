import { useEffect, useState } from "react";
import "./App.css";
import type { DashboardSpec } from "./types/types";

function App() {
  const [data, setData] = useState<DashboardSpec | null>(null);

  useEffect(() => {
    fetch("http://localhost:5029/dashboards")
      .then((res) => res.json())
      .then((spec) => setData(spec));
  }, []);

  if (data === null) return <p>Loading...</p>;

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
