import React, { useState, useEffect, useRef, useId } from "react";
import { useDispatch } from 'react-redux';
import { LoggedBoolTrue } from '../../features/loggerSlice';
import { Link, useNavigate } from 'react-router-dom'
import { toast, Zoom  } from 'react-toastify';
import "./SignIn.css";

const SignIn = () => {
  const username = useRef("");
  const password = useRef("");
  const department = useRef(0);
  const city = useRef(0);
  
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [departments, setDepartments] = useState([]);
  const [cities, setCities] = useState([]);
  const [selectedDepartment, setSelectedDepartment] = useState("");

  const usernameInputId = useId();
  const passwordInputId = useId();
  const departmentInputId = useId();
  const cityInputId = useId();

  useEffect(() => {
    const fetchDepartments = async () => {
      try {
        const response = await fetch("https://babytracker.develotion.com/departamentos.php", {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
        });
        if (!response.ok) {
          throw new Error("Error al obtener los departamentos");
        }
        const data = await response.json();
        setDepartments(data.departamentos);
      } catch (error) {
        toast.error(error.message, {
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
    };
    fetchDepartments();
  }, []);

  useEffect(() => {
    const fetchCities = async () => {
      try {
        const response = await fetch(
          `https://babytracker.develotion.com/ciudades.php?idDepartamento=${selectedDepartment}`,
          {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
            },
          }
        );
        if (!response.ok) {
          throw new Error("Error al obtener las ciudades");
        }
        const data = await response.json();
        setCities(data.ciudades);
      } catch (error) {
        toast.error(error.message, {
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
    };
    if (selectedDepartment) {
      fetchCities();
    }
  }, [selectedDepartment]);

  const handleDepartmentChange = (event) => {
    setSelectedDepartment(event.target.value);
  };

  const Registrer = async () => {
    if(city.current.value == "" || department.current.value == ""){
      toast.error("Seleccione correctamente departamento y/o ciudad", {
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
      return;
    }
    if(username.current.value == "" || password.current.value == ""){
      toast.error("Ingrese usuario y/o contraseña", {
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
      return;
    }
    try {
      const response = await fetch("https://babytracker.develotion.com/usuarios.php", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          usuario: username.current.value,
          password: password.current.value,
          idDepartamento: department.current.value,
          idCiudad: city.current.value
        }),
      });
      const data = await response.json();
      if (data.codigo === 200) {
        localStorage.setItem("apikey", data.apiKey);
        localStorage.setItem("username", username.current.value);
        localStorage.setItem("iduser", data.id);
        dispatch(LoggedBoolTrue());
        navigate("/dashboard");
        console.log("Inicio de sesión exitoso");
      }else{
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
    } catch (error) {
      toast.error(error.message, {
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
  };

  return (
    <div className="signin-container">
      <h2 className="signin-title">Registrarse</h2>
      <div>
        <div className="form-group">
          <label className="signin-label" htmlFor={usernameInputId}>
            Usuario
          </label>
          <input
            type="text"
            className="form-control"
            id={usernameInputId}
            placeholder="Ingresa tu usuario"
            ref={username}
            required
          />
        </div>
        <div className="form-group">
          <label className="signin-label" htmlFor={passwordInputId}>
            Contraseña
          </label>
          <input
            type="text"
            className="form-control"
            id={passwordInputId}
            placeholder="Ingresa tu contraseña"
            ref={password}
            required
          />
        </div>
        <div className="form-group">
          <label className="signin-label" htmlFor={departmentInputId}>
            Departamento
          </label>
          <select
            className="form-control"
            id={departmentInputId}
            ref={department}
            onChange={handleDepartmentChange}
            defaultValue=""
            required
          >
            <option value="" selected disabled>
              Seleccione su departamento
            </option>
            {departments.map((dept) => (
              <option key={dept.id} value={dept.id}>{dept.nombre}</option>
            ))}
          </select>
        </div>
        <div className="form-group">
          <label className="signin-label" htmlFor={cityInputId}>
            Ciudad
          </label>
          <select
            className="form-control"
            id={cityInputId}
            ref={city}
            defaultValue=""
          >
            <option value="" selected disabled>
              Seleccione su ciudad
            </option>
            {cities.map((city) => (
              <option key={city.id} value={city.id}>{city.nombre}</option>
            ))}
          </select>
        </div>
      </div>

      <button className="btn btn-primary signin-button" onClick={Registrer}>Registrarme</button>
    </div>
  );
};

export default SignIn;
