import React, { useState } from "react";

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
    <form onSubmit={handleSubmit}>
      <input type="file" accept=".csv" onChange={handleFileChange} />
      <button type="submit">Сгенерировать</button>
    </form>
  );
};

export default UploadForm;
