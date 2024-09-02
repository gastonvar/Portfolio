import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import './Biberones.css'
import Cargando from '../Cargando/Cargando';

const Biberones = () => {
    const eventos = useSelector(state => state.eventos.eventos);
    const categorias = useSelector(state => state.categorias.categorias);
    const eventosPedidos = useSelector(state => state.eventos.eventosPedidos);

    const [contadorBiberones, setContadorBiberones] = useState(0);
    const [tiempoTranscurrido, setTiempoTranscurrido] = useState("");
    const [fechaHoy, setFechaHoy] = useState("");
    const [cargando, setCargando] = useState(true);

    useEffect(() => {
        if(eventosPedidos){
            setCargando(false);
        }
        const categoriaBiberon = categorias.find(categoria => categoria.tipo === "Biberón");
        if (categoriaBiberon) {
            const eventosBiberonesDeHoy = eventos.filter(evento =>
                new Date(evento.fecha).getDate() === new Date().getDate() &&
                evento.idCategoria === categoriaBiberon.id
            );
            setContadorBiberones(eventosBiberonesDeHoy.length);

            const eventosBiberon = eventos.filter(evento =>
                evento.idCategoria === categoriaBiberon.id
            );

            if (eventosBiberon.length > 0) {
                const ultimoEvento = eventosBiberon.reduce((ultimo, evento) =>
                    new Date(evento.fecha) > new Date(ultimo.fecha) ? evento : ultimo
                    , eventosBiberon[0]);

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
    }, [eventos, categorias,eventosPedidos]);

    return (
        <div className="contadorBiberones">
            <div className="row">
                <h2><img src={"../../../media/biberon.png"} alt="" /></h2>
                {cargando ? (
                    <Cargando />
                ) : (
                    <>
                        <p className='fechaHoy'>{fechaHoy}</p>
                        <div className='col-12 text-start'>
                            <p><strong>Biberones:</strong> {contadorBiberones}</p>
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

export default Biberones;
