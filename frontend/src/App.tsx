import { useState } from "react";
import DashboardView from "./components/DashboardView";
import UploadForm from "./components/UploadForm";
import useTheme from "./hooks/useTheme";

function App() {
  const [jobId, setJobId] = useState<string | null>(null);
  const [colorTheme, setColorTheme] = useTheme();

  return (
    <>
      <header>
        <button onClick={() => setColorTheme("dark")}>dark</button>
        <button onClick={() => setColorTheme("light")}>light</button>
        {colorTheme}
      </header>

      {!jobId ? (
        <UploadForm onCreated={setJobId} />
      ) : (
        <DashboardView id={jobId} />
      )}
    </>
  );
}

export default App;
