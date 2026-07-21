import { useState } from "react";
import "./App.css";
import DashboardView from "./components/DashboardView";
import UploadForm from "./components/UploadForm";

function App() {
  const [jobId, setJobId] = useState<string | null>(null);

  if (!jobId) return <UploadForm onCreated={setJobId} />;
  return <DashboardView id={jobId} />;
}

export default App;
