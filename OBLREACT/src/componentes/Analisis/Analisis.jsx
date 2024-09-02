import "./Analisis.css";
import React, { useEffect} from "react";
import { useSelector } from "react-redux";
import { useNavigate } from 'react-router-dom';
import GrafCantidadPorCategoria from '../GrafCantidadPorCategoria/GrafCantidadPorCategoria.jsx'
import GrafComidasUltSemana from '../GrafComidasUltSemana/GrafComidasUltSemana.jsx'


const Analisis = () => {
    const alguienLoggeado = useSelector((state) => state.logger.isLogged);
    const navigate = useNavigate();

    useEffect(() => {
        if (!alguienLoggeado) {
            navigate('/');
        }
    }, [alguienLoggeado, navigate]);

    return (
        <>
            <div className="cantidadCatContainer col-6">
                <GrafCantidadPorCategoria />
            </div>
            <div className="comidasContainer col-4">
                <GrafComidasUltSemana />
            </div>
        </>
    );
};

export default Analisis;