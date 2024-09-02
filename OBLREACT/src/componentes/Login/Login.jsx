import React, { useId, useState, useEffect } from 'react';
import './Login.css';
import { useDispatch } from 'react-redux';
import { LoggedBoolTrue } from '../../features/loggerSlice';
import { Link, useNavigate } from 'react-router-dom'
import { toast, Zoom  } from 'react-toastify';

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isButtonDisabled, setIsButtonDisabled] = useState(true);
  const [mensajeBotonLogin, setMensajeBotonLogin] = useState("Debe ingresar usuario y contraseña");
  const usernameInputId = useId();
  const passwordInputId = useId();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    const tienenDatos = username&&password;
    setIsButtonDisabled(!(tienenDatos));
    if (tienenDatos) {
      setMensajeBotonLogin("Iniciar Sesión");
    } else {
      setMensajeBotonLogin("Ingresar usuario y contraseña");
    }
  }, [username, password]);


  const IniciarSesion = () => {
    fetch("https://babytracker.develotion.com/login.php", {
      method: 'POST', 
      headers: {
        'Content-Type': 'application/json', 
      },
      body: JSON.stringify({ usuario: username, password: password })
    })
      .then(response => response.json())
      .then(data => {
        if (data.codigo == 200) {
          localStorage.setItem('apikey', data.apiKey);
          localStorage.setItem('username', username);
          localStorage.setItem('iduser', data.id);
          dispatch(LoggedBoolTrue());
          navigate("/dashboard");
          console.log("Inicio de sesión exitoso");
        } else {
          toast.error(data.mensaje, {
            position: "top-right",
            autoClose: 2000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Zoom,
            });
        }
      })
      .catch(error => {
        
      });
  };

  return (
    <div className="login-container">
      <h2 className="login-title">Iniciar Sesión</h2>
      <div>
        <div className="form-group">
          <label className='login-label' htmlFor={usernameInputId}>Usuario</label>
          <input 
            type="text" 
            className="form-control" 
            id={usernameInputId} 
            placeholder="Ingresa tu usuario" 
            value={username}
            onChange={(e) => setUsername(e.target.value)} autoComplete="off"
          />
        </div>
        <div className="form-group">
          <label className="login-label" htmlFor={passwordInputId}>Contraseña</label>
          <input 
            type="password" 
            className="form-control" 
            id={passwordInputId} 
            placeholder="Ingresa tu contraseña" 
            value={password}
            onChange={(e) => setPassword(e.target.value)} autoComplete="off"
          />
        </div>
      </div>
      <button 
        className="btn btn-primary login-button" 
        onClick={IniciarSesion} 
        disabled={isButtonDisabled}
      >
        {mensajeBotonLogin}
      </button>
      <div className="logo"></div>
    </div>
  );
};

export default Login;
