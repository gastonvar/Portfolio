import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import './ListarEventosDia.css'
import { eliminarEvento } from '../../features/eventosSlice';
import { toast, Zoom  } from 'react-toastify';
import { guardarEventos, Pedidos } from "../../features/eventosSlice";
import Cargando from '../Cargando/Cargando';
const ListarEventosDia = () => {

const eventos = useSelector(state => state.eventos.eventos);
const eventosOrdenados = [...eventos].sort((a, b) => new Date(b.fecha) - new Date(a.fecha)).filter(evento => new Date(evento.fecha).getDate() == new Date().getDate());
const categorias = useSelector(state=>state.categorias.categorias);
const [cargando, setCargando] = useState(true);
const dispatch = useDispatch();

const obtenerImagenEvento = (idCategoria) => {
  let categoria = categorias.find(categoria => categoria.id == idCategoria);
  return categoria == null ? "N/A" : categoria.imagen;
};

useEffect(()=>{
  fetch(
    `https://babytracker.develotion.com/eventos.php?idUsuario=${localStorage.getItem(
        "iduser"
    )}`,
    {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            apikey: localStorage.getItem("apikey"),
            iduser: localStorage.getItem("iduser"),
        },
    }
)
    .then((response) => {
        if (!response.ok) {
            throw new Error("Error en la red");
        }
        return response.json();
    })
    .then((data) => {
        dispatch(guardarEventos(data.eventos));
        dispatch(Pedidos());
        setCargando(false);
    })
    .catch((error) => {
        console.error("Error:", error);
    });
},[])

const eliminar = (id) => {
    fetch(`https://babytracker.develotion.com/eventos.php?idEvento=${id}`, {
        method: 'DELETE', 
        headers: {
          'Content-Type': 'application/json', 
          'apikey': localStorage.getItem('apikey'),
          'iduser': localStorage.getItem('iduser')
        }
      })
        .then(response => {
          if (!response.ok) {
            throw new Error('Error en la red');
          }
          return response.json();
        })
        .then(data => {
        dispatch(eliminarEvento(id));
        toast.success("Evento eliminado con éxito", {
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
          console.error('Error:', error);
        });
}

let retorno = "";
if(eventosOrdenados.length>0){
  retorno = <>
  <table className='listareventosdia-tabla'>
    <thead>
      <tr>
        <th>Categoría</th>
        <th>Detalle</th>
        <th>Fecha</th>
        <th></th>
      </tr>
    </thead>
    <tbody>
      {eventosOrdenados.map((evento) => (
        <tr key={evento.id}>
          <td><img src={`https://babytracker.develotion.com/imgs/${obtenerImagenEvento(evento.idCategoria)}.png`}/></td>
          <td>{evento.detalle!="" ? evento.detalle : "N/A"}</td>
          <td>{evento.fecha}</td>
          <td>
            <button className='delete-button' onClick={() => eliminar(evento.id)}>X</button>
          </td>
        </tr>
      ))}
    </tbody>
  </table>
  </>
}else{
  retorno = <div><h2>No tienes eventos :(</h2>
  <p>Pero puedes agregarlos en el panel correspondiente!</p></div>
}


  return (
    <div className='listareventosdia-container'>
        <h2>HOY</h2>
    {cargando ? <Cargando/> : retorno}
  </div>
  )
}

export default ListarEventosDia