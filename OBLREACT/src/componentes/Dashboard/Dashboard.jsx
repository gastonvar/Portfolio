import "./Dashboard.css";
import React, { useState, useEffect, useRef, useId } from "react";
import AgregarEvento from "../AgregarEvento/AgregarEvento";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from 'react-router-dom';
import ListarEventosAnteriores from "../ListarEventosAnteriores/ListarEventosAnteriores";
import ListarEventosDia from "../ListarEventosDia/ListarEventosDia";
import { toast, Zoom  } from 'react-toastify';
import Biberones from "../Biberones/Biberones";
import Panales from "../Panales/Panales";
import TiempoRestanteBiberon from "../TiempoRestanteBiberon/TiempoRestanteBiberon";
import GrafCantidadPorCategoria from "../GrafCantidadPorCategoria/GrafCantidadPorCategoria";
import GrafComidasUltSemana from "../GrafComidasUltSemana/GrafComidasUltSemana";
import Consejos from "../Consejos/Consejos";
const Dashboard = () => {
    const alguienLoggeado = useSelector((state) => state.logger.isLogged);
    const cargando = useState(false);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        if (!alguienLoggeado) {
            navigate('/');
        }
    }, [alguienLoggeado, navigate]);

    return (
        <div className="row col-12 dashboard-container justify-content-center">

                <div className="col-2 mt-5">
                    <AgregarEvento />
                </div>
                <div className="col-5 mt-5">
                    <ListarEventosDia />
                </div>
                <div className="col-5 mt-5">
                    <ListarEventosAnteriores />
                </div>
                <div className="col-2 mt-5">
                    <Panales></Panales>
                </div>
                <div className="col-2 mt-5">
                    <Biberones></Biberones>
                </div>
                <div className="col-2 mt-5">
                    <TiempoRestanteBiberon></TiempoRestanteBiberon>
                </div>
                <div className="col-6 mt-5">
                    <Consejos></Consejos>
                </div>
                <div className="col-5 mt-5 cantidadCatContainer">
                    <GrafCantidadPorCategoria></GrafCantidadPorCategoria>
                </div>
                <div className="col-5 mt-5 comidasContainer">
                    <GrafComidasUltSemana></GrafComidasUltSemana>
                </div>
        </div>
    );
};

export default Dashboard;
