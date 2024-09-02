import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';
import '../Analisis/Analisis.css';

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

export const options = {
    responsive: true,
    plugins: {
        legend: {
            position: 'top',
        },
        title: {
            display: true,
            text: 'Comidas de los últimos 7 días',
        },
    },
    scales: {
        y: {
            grid: {
                display: false,
            },
            ticks: {
                stepSize: 1,
            },
        },
    },
};

const ultimos7dias = () => {
    const dates = [];
    for (let i = 6; i >= 0; i--) {
        const date = new Date();
        date.setDate(date.getDate() - i);
        dates.push(date.toISOString().split('T')[0]); 
    }
    return dates;
};

const labels = ultimos7dias();

const GrafComidasUltSemana = () => {
    const categorias = useSelector(state => state.categorias.categorias);
    const eventos = useSelector(state => state.eventos.eventos);
    const eventosPedidos = useSelector(state => state.eventos.eventosPedidos);
    const [comidas, setComidas] = useState([]);

    useEffect(() => {
        const categoriaComida = categorias.find(categoria => categoria.tipo === "Comida");
        if (categoriaComida) {
            const eventosComida = eventos.filter(evento => evento.idCategoria === categoriaComida.id);
            setComidas(eventosComida);
        }

    }, [eventos, categorias, eventosPedidos]);

    const contarComidasPorDia = () => {
        const conteo = labels.map(label => {
            const comidasEnElDia = comidas.filter(comida => {
                const fechaEvento = comida.fecha.split(' ')[0];
                return fechaEvento === label;
            }).length;
            return comidasEnElDia;
        });
        return conteo;
    };

    const datosComidas = contarComidasPorDia();

    console.log(categorias);
    console.log(eventos);

    return (
        <Bar
            options={options}
            data={{
                labels: labels,
                datasets: [
                    {
                        label: 'Comidas',
                        data: datosComidas,
                        backgroundColor: 'rgba(255, 99, 132, 1)',
                    }
                ],
            }}
        />
    );
};

export default GrafComidasUltSemana;