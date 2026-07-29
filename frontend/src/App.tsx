import { useState } from "react";
import DashboardView from "./components/DashboardView";
import Header from "./components/Header/Header";
import UploadForm from "./components/UploadForm";
import useTheme from "./hooks/useTheme";

function App() {
  const [jobId, setJobId] = useState<string | null>(null);
  const [colorTheme, setColorTheme] = useTheme();

  return (
    <>
      <Header colorTheme={colorTheme} setColorTheme={setColorTheme} />

      {!jobId ? (
        <UploadForm onCreated={setJobId} />
      ) : (
        <DashboardView id={jobId} />
      )}
    </>
  );
}

export default App;
