import { Link } from "react-router-dom";
import type { DashboardListItem } from "../../models/DashboardListItem";
import "./Sidebar.css";

type SidebarProps = {
  items: DashboardListItem[];
  activeId?: string;
};

function formatDate(value: string) {
  return new Date(value).toLocaleString(undefined, {
    day: "numeric",
    month: "short",
    hour: "2-digit",
    minute: "2-digit",
  });
}

const Sidebar = ({ items, activeId }: SidebarProps) => {
  return (
    <aside className="sidebar">
      <h2 className="sidebar-title">Your dashboards</h2>

      {items.length === 0 ? (
        <p className="sidebar-empty">
          Nothing here yet. Upload a file to build your first dashboard.
        </p>
      ) : (
        <ul className="sidebar-list">
          {items.map((item) => (
            <li key={item.id}>
              <Link
                to={`/dashboard/${item.id}`}
                className={
                  item.id === activeId
                    ? "sidebar-item sidebar-item-active"
                    : "sidebar-item"
                }
              >
                <span className="sidebar-item-name">{item.fileName}</span>
                <span className="sidebar-item-meta">
                  <span className={`sidebar-dot sidebar-dot-${item.status}`} />
                  {formatDate(item.createdAt)}
                </span>
              </Link>
            </li>
          ))}
        </ul>
      )}
    </aside>
  );
};

export default Sidebar;
