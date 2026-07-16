import { useEffect, useState } from "react";
import "./App.css";

function App() {
  const [data, setData] = useState<string>("");

  useEffect(() => {
    fetch("http://localhost:5029/dashboards")
      .then((res) => res.text())
      .then((text) => setData(text));
  }, []);

  return <h1>{data}</h1>;
}

export default App;
