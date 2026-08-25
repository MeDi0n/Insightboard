import { useParams } from "react-router-dom";
import DashboardView from "../components/DashboardView/DashboardView";

const DashboardPage = () => {
  const { id } = useParams();

  if (!id) {
    return <div>Please try again!</div>;
  }

  return <DashboardView id={id} />;
};

export default DashboardPage;
