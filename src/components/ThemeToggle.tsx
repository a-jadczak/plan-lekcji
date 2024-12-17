import React, { useState, useEffect } from "react";

const ThemeToggle: React.FC = () => {
  const [root, setRoot] = useState<Element | null>();

  const [theme, setTheme] = useState<"dark" | "light">("light");

  const [themeToggle, setThemeToggle] = useState<boolean>(() => {
    // Pobierz dane z localStorage podczas inicjalizacji
    const savedData = localStorage.getItem("themeData");
    return savedData ? JSON.parse(savedData) : "light";
  });

  const toggleTheme = () => 
  {
    setThemeToggle(prevTheme => !prevTheme);
    toggleEffect();
  };

  const toggleEffect = () =>
  { 
    if (themeToggle)
    {
      setTheme("dark");
      // @ts-ignore
      root.style.setProperty('--head-table-color', '#131313');
      // @ts-ignore
      root.style.setProperty('--navbar-color', '#131313');
    }
    else
    {
      setTheme("light");
  
      // @ts-ignore
      root.style.setProperty('--head-table-color', '#c8c8c8');
      // @ts-ignore
      root.style.setProperty('--navbar-color', 'white');
    }
  }

  useEffect(() => {
    document.body.setAttribute("data-bs-theme", theme);
  }, [theme]);

  useEffect(() => {
    var r = document.querySelector(':root');
    setRoot(r);

    // Ze względu na asynchroniczność przy podstawianiu r do zmiennej setRoot
    if (r) {
      if (themeToggle) {
        setTheme("dark");
        //@ts-ignore
        r.style.setProperty('--head-table-color', '#131313');
        //@ts-ignore
        r.style.setProperty('--navbar-color', '#131313');
      } else {
        setTheme("light");
        //@ts-ignore
        r.style.setProperty('--head-table-color', '#c8c8c8');
        //@ts-ignore
        r.style.setProperty('--navbar-color', 'white');
      }
    }
  });

  useEffect(() =>{
    localStorage.setItem("themeData", JSON.stringify(themeToggle));
  }, [themeToggle])

  return (
    <button
      onClick={toggleTheme}
      className="btn btn-secondary"
      aria-label="Toggle theme"
    >
      {theme === "dark" ? (
        <i className="fas fa-sun"></i>
      ) : (
        <i className="fas fa-moon"></i>
      )}
    </button>
  );
};

export default ThemeToggle;
