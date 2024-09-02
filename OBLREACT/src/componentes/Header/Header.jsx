import React from 'react';
import './Header.css';
import Logout from '../Logout/Logout';
import { useSelector } from 'react-redux';
import { NavLink, Outlet } from "react-router-dom"


const Header = () => {
  const alguienLoggeado = useSelector(state => state.logger.isLogged);
  let greeting;
  let navegacion

  if (alguienLoggeado) {
    greeting = <h1 className="mensajeBienvenida">Bienvenid@, <span>{localStorage.getItem('username')}</span></h1>;
  } else {
    greeting = <h1 className="mensajeBienvenida">Bienvenid@, inicia sesión o regístrate</h1>;
  }

  return (
    <header className="header">
      <div className='flexContainer'>
        <div className="logo" src="./media/logo.png" alt="BabyTracker_logo"></div>
        {greeting}
      </div>
      <Logout />
    </header>
  );
};

export default Header;
