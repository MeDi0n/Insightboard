import { ChartNoAxesColumnIncreasing, Moon, Sun } from "lucide-react";
import type { Dispatch, SetStateAction } from "react";
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
      <div className="logo">
        <ChartNoAxesColumnIncreasing />
        Insightboard
      </div>
      <button className="switch-button" onClick={toggleTheme}>
        {colorTheme === "light" ? <Moon /> : <Sun />}
      </button>
    </header>
  );
};

export default Header;
