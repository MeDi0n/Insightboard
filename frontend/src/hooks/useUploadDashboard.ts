import { useMutation } from "@tanstack/react-query";
import api from "../api/httpClient";

export function useUploadDashboard() {
  return useMutation({
    mutationFn: async (file: File) => {
      const formData = new FormData();

      formData.append("file", file);

      const res = await api.post<{ id: string }>("/dashboards", formData);
      return res.data;
    },
  });
}
