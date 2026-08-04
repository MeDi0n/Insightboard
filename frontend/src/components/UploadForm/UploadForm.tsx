import { Upload } from "lucide-react";
import React, { useState } from "react";
import "./UploadForm.css";

type UploadFormProps = {
  onCreated: (id: string) => void;
};

const UploadForm = ({ onCreated }: UploadFormProps) => {
  const [file, setFile] = useState<File | null>(null);

  function handleFileChange(e: React.ChangeEvent<HTMLInputElement>) {
    setFile(e.target.files?.[0] ?? null);
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    if (!file) return;

    const formData = new FormData();
    formData.append("file", file);

    const res = await fetch("http://localhost:5029/dashboards", {
      method: "POST",
      body: formData,
    });
    const data = await res.json();

    onCreated(data.id);
  }

  return (
    <div className="page-width">
      <div className="upload-card">
        <h1 className="title">Build a dashboard from CSV</h1>
        <p className="subtitle">
          Upload a table AI picks the charts and builds the dashboard
        </p>
        <form onSubmit={handleSubmit}>
          <label className="dropzone">
            <Upload className="upload-icon" />
            <span>Drag your CSV here</span>
            <span className="or">or</span>
            <span className="choose">Choose your file</span>
            <input
              type="file"
              accept=".csv"
              onChange={handleFileChange}
              hidden
            />
            {file && <span className="filename">{file.name}</span>}
          </label>

          <button type="submit" className="submit">
            Build dashboard
          </button>
        </form>
      </div>
    </div>
  );
};

export default UploadForm;
