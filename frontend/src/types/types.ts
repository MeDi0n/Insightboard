export interface ChartSpec {
  type: string;
  title: string;
  x: string;
  y: string;
}

export interface DashboardSpec {
  charts: ChartSpec[];
  data: Record<string, string>[];
}

export interface DashboardJob {
  status: string;
  spec: DashboardSpec | null;
}

export type DashboardViewProps = { id: string };

export type ChartProps = { chart: ChartSpec; data: Record<string, string>[] };
