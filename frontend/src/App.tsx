import { Route, Routes } from "react-router-dom";
import Header from "./components/Header/Header";
import useTheme from "./hooks/useTheme";
import DashboardPage from "./pages/DashboardPage";
import UploadPage from "./pages/UploadPage";

function App() {
  const [colorTheme, setColorTheme] = useTheme();

  return (
    <>
      <div className="app-shell">
        <Header colorTheme={colorTheme} setColorTheme={setColorTheme} />
        <Routes>
          <Route path="/" element={<UploadPage />} />
          <Route path="/dashboard/:id" element={<DashboardPage />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
