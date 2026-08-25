import axios from "axios";
import { Upload } from "lucide-react";
import React, { useState } from "react";
import api from "../../api/httpClient";
import "./UploadForm.css";

type UploadFormProps = {
  onCreated: (id: string) => void;
};

const UploadForm = ({ onCreated }: UploadFormProps) => {
  const [file, setFile] = useState<File | null>(null);
  const [error, setError] = useState<string | null>(null);

  function handleFileChange(e: React.ChangeEvent<HTMLInputElement>) {
    setFile(e.target.files?.[0] ?? null);
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError(null);

    if (!file) return;

    const formData = new FormData();
    formData.append("file", file);

    try {
      const res = await api.post<{ id: string }>("/dashboards", formData);

      onCreated(res.data.id);
    } catch (err) {
      if (axios.isAxiosError(err)) {
        setError(err.response?.data?.error ?? "Upload failed");
      } else {
        setError("Server is not available right now");
      }
    }
  }

  return (
    <div className="page-width">
      <div className="upload-card">
        <h1 className="title">Build a dashboard from your data</h1>
        <p className="subtitle">
          Upload a CSV or Excel file — AI picks the charts and builds the
          dashboard
        </p>
        <form onSubmit={handleSubmit}>
          <label className="dropzone">
            <Upload className="upload-icon" />
            <span>Drag your file here</span>
            <span className="or">or</span>
            <span className="choose">Choose your file</span>
            <input
              type="file"
              accept=".csv,.xlsx,.pdf"
              onChange={handleFileChange}
              hidden
            />
            {file && <span className="filename">{file.name}</span>}
          </label>
          {error && <span className="upload-error">{error}</span>}
          <button type="submit" className="submit">
            Build dashboard
          </button>
        </form>
      </div>
    </div>
  );
};

export default UploadForm;
