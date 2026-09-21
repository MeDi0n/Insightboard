import { ChartNoAxesColumnIncreasing, Moon, Sun } from "lucide-react";
import type { Dispatch, SetStateAction } from "react";
import { Link, NavLink } from "react-router-dom";
import type { Colors } from "../../hooks/useTheme";
import "./Header.css";

type Props = {
  colorTheme: Colors;
  setColorTheme: Dispatch<SetStateAction<Colors>>;
};

const Header = ({ colorTheme, setColorTheme }: Props) => {
  const toggleTheme = () => {
    setColorTheme((prev) => (prev === "light" ? "dark" : "light"));
  };

  return (
    <header className="header">
      <div className="page-width">
        <Link to="/" className="logo">
          <ChartNoAxesColumnIncreasing />
          Insightboard
        </Link>
        <div className="header-actions">
          <NavLink to="/usage" className="header-link">
            Usage
          </NavLink>
          <button className="switch-button" onClick={toggleTheme}>
            {colorTheme === "light" ? <Moon /> : <Sun />}
          </button>
        </div>
      </div>
    </header>
  );
};

export default Header;
