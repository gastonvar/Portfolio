import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import './Panales.css';
import Cargando from '../Cargando/Cargando';

const Panales = () => {
    const eventos = useSelector(state => state.eventos.eventos);
    const categorias = useSelector(state => state.categorias.categorias);
    const eventosPedidos = useSelector(state => state.eventos.eventosPedidos);

    const [contadorPañales, setContadorPañales] = useState(0);
    const [tiempoTranscurrido, setTiempoTranscurrido] = useState("");
    const [fechaHoy, setFechaHoy] = useState("");
    const [cargando, setCargando] = useState(true);

    useEffect(() => {
        if(eventosPedidos){
            setCargando(false);
        }
        const categoriaPañal = categorias.find(categoria => categoria.tipo === "Pañal");
        if (categoriaPañal) {
            const eventosPañalesDeHoy = eventos.filter(evento =>
                new Date(evento.fecha).getDate() === new Date().getDate() &&
                evento.idCategoria === categoriaPañal.id
            );

            setContadorPañales(eventosPañalesDeHoy.length);

            const eventosPañal = eventos.filter(evento =>
                evento.idCategoria === categoriaPañal.id
            );

            if (eventosPañal.length > 0) {
                const ultimoEvento = eventosPañal.reduce((ultimo, evento) =>
                    new Date(evento.fecha) > new Date(ultimo.fecha) ? evento : ultimo
                    , eventosPañal[0]);

                if (ultimoEvento) {
                    const ahora = new Date();
                    const ultimoEventoFecha = new Date(ultimoEvento.fecha);
                    const diferencia = ahora - ultimoEventoFecha;

                    const horas = Math.floor(diferencia / (1000 * 60 * 60));
                    const minutos = Math.floor((diferencia % (1000 * 60 * 60)) / (1000 * 60));
                    const segundos = Math.floor((diferencia % (1000 * 60)) / 1000);

                    if (horas >= 24) {
                        setTiempoTranscurrido("+1 día");
                    } else {
                        setTiempoTranscurrido(`${horas}h ${minutos}m ${segundos}s`);
                    }
                } else {
                    setTiempoTranscurrido("N/A");
                }
            } else {
                setTiempoTranscurrido("N/A");
            }

            const hoy = new Date();
            const fechaFormateada = `${hoy.getDate()}/${hoy.getMonth() + 1}/${hoy.getFullYear()}`;
            setFechaHoy(fechaFormateada);
        }
    }, [eventos, categorias, eventosPedidos]);

    return (
        <div className="contadorPanales">
            <div className="row">
                <h2><img src={"../../../media/panal.png"} alt="" /></h2>
                {cargando ? (
                    <Cargando />
                ) : (
                    <>
                        <p className='fechaHoy'>{fechaHoy}</p>
                        <div className='col-12 text-start'>
                            <p><strong>Cambios:</strong> {contadorPañales}</p>
                        </div>
                        <div className='col-12 text-start'>
                            <p className='tiempoP'><strong>Último hace:</strong> {tiempoTranscurrido}</p>
                        </div>
                    </>
                )}
            </div>
        </div>
    );
    
};

export default Panales;