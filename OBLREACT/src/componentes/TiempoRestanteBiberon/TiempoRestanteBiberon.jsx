import React, { useEffect, useState } from 'react';
import './TiempoRestanteBiberon.css';
import { useSelector } from 'react-redux';
import Cargando from '../Cargando/Cargando';

const TiempoRestanteBiberon = () => {
    const eventos = useSelector(state => state.eventos.eventos);
    const categorias = useSelector(state => state.categorias.categorias);
    const eventosPedidos = useSelector(state => state.eventos.eventosPedidos);

    const [fechaHoy, setFechaHoy] = useState("");
    const [tiempoRestante, setTiempoRestante] = useState(null);
    const [tiempoExcedido, setTiempoExcedido] = useState(false);
    const [cargando, setCargando] = useState(true);

    useEffect(() => {
        if(eventosPedidos){
            setCargando(false);
        }
        const categoriaBiberon = categorias.find(categoria => categoria.tipo === "Biberón");
        if (categoriaBiberon) {
            const eventosBiberon = eventos.filter(evento => evento.idCategoria === categoriaBiberon.id);

            if (eventosBiberon.length > 0) {
                const ultimoEvento = eventosBiberon.reduce((ultimo, evento) =>
                    new Date(evento.fecha) > new Date(ultimo.fecha) ? evento : ultimo
                , eventosBiberon[0]);

                if (ultimoEvento) {
                    const ahora = new Date();
                    const ultimoEventoFecha = new Date(ultimoEvento.fecha);
                    const proximoEventoFecha = new Date(ultimoEventoFecha.getTime() + 4 * 60 * 60 * 1000);
                    const diferencia = proximoEventoFecha - ahora;

                    if (diferencia >= 0) {
                        const horas = Math.floor(diferencia / (1000 * 60 * 60));
                        const minutos = Math.floor((diferencia % (1000 * 60 * 60)) / (1000 * 60));
                        const segundos = Math.floor((diferencia % (1000 * 60)) / 1000);
                        setTiempoRestante(`${horas}h ${minutos}m ${segundos}s`);
                        setTiempoExcedido(false);
                    } else {
                        setTiempoRestante(`0h 0m 0s`);
                        setTiempoExcedido(true);
                    }
                } else {
                    setTiempoRestante("N/A");
                }
            } else {
                setTiempoRestante("N/A");
            }

            const hoy = new Date();
            const fechaFormateada = `${hoy.getDate()}/${hoy.getMonth() + 1}/${hoy.getFullYear()}`;
            setFechaHoy(fechaFormateada);
        }
    }, [eventos, categorias, eventosPedidos]);

    return (
        <div className="tiemporestantebiberon-container">
            <div className="row justify-content-center">
                <h2><img src={"../../../media/biberon.png"} alt="" /></h2>
                {cargando ? (
                    <Cargando />
                ) : (
                    <>
                        <p className='fechaHoy'>{fechaHoy}</p>
                        <div className='col-12'>
                            <p className={tiempoExcedido ? 'tiempoP tiempo-excedido' : 'tiempoP tiempo-restante'}>
                                <strong>Próximo en:</strong> {tiempoRestante}
                            </p>
                        </div>
                    </>
                )}
            </div>
        </div>
    );
    
};

export default TiempoRestanteBiberon;

