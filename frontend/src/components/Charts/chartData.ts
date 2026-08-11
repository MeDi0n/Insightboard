// Бэкенд отдаёт отсутствующие ячейки пустой строкой, а Recharts превращает её
// в ноль — на графике это читается как настоящий ноль, хотя значения просто нет.
// Меняем пустые строки на null: тогда точка не рисуется, а линия рвётся.
export function withGaps(
  data: Record<string, string>[],
  key: string,
): Record<string, string | null>[] {
  return data.map((row) => ({
    ...row,
    [key]: row[key] === "" || row[key] === undefined ? null : row[key],
  }));
}
