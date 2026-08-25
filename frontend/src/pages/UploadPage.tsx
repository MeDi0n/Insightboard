import { useNavigate } from "react-router-dom";
import UploadForm from "../components/UploadForm/UploadForm";

const UploadPage = () => {
  const navigate = useNavigate();

  return <UploadForm onCreated={(id) => navigate(`/dashboard/${id}`)} />;
};

export default UploadPage;
