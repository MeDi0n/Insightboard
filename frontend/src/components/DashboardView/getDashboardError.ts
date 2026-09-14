import { isAxiosError } from "axios";
import { DashboardStatus, type Dashboard } from "../../models/DashboardModel";

export function getDashboardError(
  error: Error | null,
  dashboard: Dashboard | undefined,
): { title: string; text: string } | null {
  if (error) {
    if (isAxiosError(error)) {
      if (!error.response) {
        return {
          title: "Could not reach the server",
          text: "Check your connection and try again.",
        };
      }
      if (error.response.status === 404) {
        return {
          title: "Dashboard not found",
          text: "This link may have expired. Upload the file again.",
        };
      }

      return {
        title: "Something went wrong",
        text: "The server returned an error. Try again later.",
      };
    }

    return {
      title: "Unexpected response",
      text: "The server sent data we could not read.",
    };
  }

  if (dashboard?.status === DashboardStatus.Failed) {
    return {
      title: "Could not build the dashboard",
      text: "AI failed to produce a valid result after 3 attempts. Check the file format and try again.",
    };
  }
  return null;
}
