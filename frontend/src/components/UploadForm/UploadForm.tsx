import { useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { Upload } from "lucide-react";
import React, { useState } from "react";
import { useUploadDashboard } from "../../hooks/useUploadDashboard";
import "./UploadForm.css";

type UploadFormProps = {
  onCreated: (id: string) => void;
};

function getUploadErrorMessage(error: Error | null): string | null {
  if (error == null) return null;

  if (axios.isAxiosError(error)) {
    if (error.response) {
      return error.response?.data?.error ?? "Upload failed";
    }

    return "Server is not available right now";
  } else {
    return "Upload failed";
  }
}

const UploadForm = ({ onCreated }: UploadFormProps) => {
  const [file, setFile] = useState<File | null>(null);
  const { mutate, isPending, error } = useUploadDashboard();
  const errorMessage = getUploadErrorMessage(error);
  const queryClient = useQueryClient();

  function handleFileChange(e: React.ChangeEvent<HTMLInputElement>) {
    setFile(e.target.files?.[0] ?? null);
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    if (!file) return;

    mutate(file, {
      onSuccess: (data) => {
        queryClient.invalidateQueries({ queryKey: ["dashboards"] });
        onCreated(data.id);
      },
    });
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
          {errorMessage && <span className="upload-error">{errorMessage}</span>}

          <button type="submit" className="submit" disabled={isPending}>
            {isPending ? "Uploading..." : "Build dashboard"}
          </button>
        </form>
      </div>
    </div>
  );
};

export default UploadForm;
