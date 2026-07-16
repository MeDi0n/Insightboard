export interface ChartSpec {
  type: string;
  title: string;
  x: string;
  y: string;
}

export interface DashboardSpec {
  charts: ChartSpec[];
}
