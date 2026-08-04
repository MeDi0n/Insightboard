import { useState } from "react";
import DashboardView from "./components/DashboardView/DashboardView";
import Header from "./components/Header/Header";
import UploadForm from "./components/UploadForm/UploadForm";
import useTheme from "./hooks/useTheme";

function App() {
  const [jobId, setJobId] = useState<string | null>(null);
  const [colorTheme, setColorTheme] = useTheme();
  const reset = () => {
    setJobId(null);
  };

  return (
    <>
      <div className="app-shell">
        <Header colorTheme={colorTheme} setColorTheme={setColorTheme} />
        {!jobId ? (
          <UploadForm onCreated={setJobId} />
        ) : (
          <DashboardView id={jobId} onReset={reset} />
        )}
      </div>
    </>
  );
}

export default App;
