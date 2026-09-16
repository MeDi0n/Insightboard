import { Route, Routes, useMatch } from "react-router-dom";
import Header from "./components/Header/Header";
import Sidebar from "./components/Sidebar/Sidebar";
import { useGetDashboardList } from "./hooks/useGetDashboardList";
import useTheme from "./hooks/useTheme";
import DashboardPage from "./pages/DashboardPage";
import UploadPage from "./pages/UploadPage";

function App() {
  const [colorTheme, setColorTheme] = useTheme();
  const match = useMatch("/dashboard/:id");
  const activeId = match?.params.id;
  const { data } = useGetDashboardList();

  return (
    <>
      <div className="app-shell">
        <Header colorTheme={colorTheme} setColorTheme={setColorTheme} />
        <div className="app-layout">
          <Sidebar items={data ?? []} activeId={activeId} />
          <main className="app-main">
            <Routes>
              <Route path="/" element={<UploadPage />} />
              <Route path="/dashboard/:id" element={<DashboardPage />} />
            </Routes>
          </main>
        </div>
      </div>
    </>
  );
}

export default App;
