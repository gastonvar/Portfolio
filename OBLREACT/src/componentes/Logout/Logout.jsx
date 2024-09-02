import React from 'react';
import './Logout.css'
import { useDispatch, useSelector } from 'react-redux';
import { LoggedBoolFalse } from '../../features/loggerSlice';
import { Link, useNavigate } from "react-router-dom"

const Logout = () => {


  const alguienLoggeado = useSelector(state => state.logger.isLogged);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const cerrarSesion = () => {
    localStorage.clear();
    dispatch(LoggedBoolFalse());
    navigate("/")
    console.log("Logout exitoso");
  };

if(alguienLoggeado){
  return (
    <input
      type="button"
      className="logout-button"
      value="Cerrar Sesión"
      onClick={cerrarSesion}
    />
      );
}
return (
<></>
  );
};

export default Logout;
