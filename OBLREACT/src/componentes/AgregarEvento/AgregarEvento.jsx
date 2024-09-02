import React, { useEffect, useId, useRef, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import './AgregarEvento.css';
import { agregarEvento } from '../../features/eventosSlice';
import { toast, Zoom  } from 'react-toastify';
import { guardarCategorias } from "../../features/categoriasSlice";
import Cargando from '../Cargando/Cargando';
const AgregarEvento = () => {

    let categoriaSeleccionada = useRef(0);
    let fechaSeleccionada = useRef("");
    let detalles = useRef("");

    const selectId = useId();
    const dispatch = useDispatch();
    const categorias = useSelector(state => state.categorias.categorias);

    const estiloSelect = () => {
        document.getElementById(selectId).style.borderEndEndRadius = 0;
        document.getElementById(selectId).style.borderEndStartRadius = 0;
    }

    const resetEstiloSelect = () => {
        const selectElement = document.getElementById(selectId);
        selectElement.style.borderEndEndRadius = '15px';
        selectElement.style.borderEndStartRadius = '15px';
      };

      useEffect(() => {
        const ahora = new Date();
        const utc3 = new Date(ahora.getTime() - 3 * 60 * 60 * 1000);
        const fechaISO = utc3.toISOString().substring(0, 16);
        if (fechaSeleccionada.current) {
            fechaSeleccionada.current.value = fechaISO;
        }
        fetch("https://babytracker.develotion.com/categorias.php", {
          method: "GET",
          headers: {
              "Content-Type": "application/json",
              apikey: localStorage.getItem("apikey"),
              iduser: localStorage.getItem("iduser"),
          },
      })
          .then((response) => {
              if (!response.ok) {
                  throw new Error("Error en la red");
              }
              let respuesta = response.json();
              return respuesta;
          })
          .then((data) => {
              dispatch(guardarCategorias(data.categorias));
          })
          .catch((error) => {
              console.error("Error:", error);
          });
    }, []);

    const AddEvent = () => {

      let eventoAAgregar = {idCategoria: categoriaSeleccionada.current.value,fecha: fechaSeleccionada.current.value,detalle: detalles.current.value, idUsuario: localStorage.getItem('iduser')
      }
        if(eventoAAgregar.idCategoria == -100){
          toast.error("Seleccione una categoria", {
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
        if(new Date(eventoAAgregar.fecha) > new Date()){
          toast.error("La fecha no puede ser futura", {
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
        fetch("https://babytracker.develotion.com/eventos.php", {
            method: 'POST', 
            headers: {
              'Content-Type': 'application/json', 
              'apikey': localStorage.getItem('apikey'),
              'iduser': localStorage.getItem('iduser')
            },
            body: JSON.stringify(eventoAAgregar)
          })
            .then(response => {
              if (!response.ok) {
                throw new Error("Error en la red");
              } 
              return response.json();
            })
            .then(data => {
              let eventoAAgregarFormateadoParaQueReduxNoMeRompaLasPelotasPorqueEstuve2HorasDebugeando = {
                detalle: detalles.current.value,
                fecha: fechaSeleccionada.current.value.replace('T', ' '),
                id: data.idEvento,
                idCategoria: Number(categoriaSeleccionada.current.value),
                idUsuario: Number(localStorage.getItem('iduser'))};
              dispatch(agregarEvento(eventoAAgregarFormateadoParaQueReduxNoMeRompaLasPelotasPorqueEstuve2HorasDebugeando));
              toast.success("Evento agregado con éxito", {
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
            })
            .catch(error => {
              toast.error(error, {
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
            });
    }
    
    return (
      <div className="agregarevento-container">
          <h2 className="agregarevento-title">Agregar evento</h2>
              <div>
                  <div className="form-group">
                      <select className='' id={selectId} ref={categoriaSeleccionada} onClick={estiloSelect} onBlur={resetEstiloSelect} defaultValue={-100}>
                          <option key={-100} value={-100} disabled>Categorias</option>
                          {categorias.map((categoria) => (
                              <option key={categoria.id} value={categoria.id}>{categoria.tipo}</option>
                          ))}
                      </select>
                  </div>
                  <div className="form-group">
                      <input className='agregarevento-fecha' type='datetime-local' ref={fechaSeleccionada} />
                  </div>
                  <div className='form-group'>
                      <textarea placeholder='Descripción' ref={detalles}></textarea>
                  </div>
              </div>
          
          <button className="btn btn-primary agregarevento-button" onClick={AddEvent}>Agregar</button>
      </div>
  );
};

export default AgregarEvento