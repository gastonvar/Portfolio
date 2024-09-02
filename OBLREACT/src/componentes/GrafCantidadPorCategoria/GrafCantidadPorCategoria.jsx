import React from 'react';
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
            text: 'Cantidad de eventos por Categoría',
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

const GrafCantidadPorCategoria = () => {
    const categorias = useSelector(state => state.categorias.categorias);
    const eventos = useSelector(state => state.eventos.eventos);

    const contarEventosPorCategoria = () => {
        const conteo = [];
        categorias.forEach(categoria => {
            const cantidad = eventos.filter(evento => evento.idCategoria === categoria.id).length;
            if (cantidad > 0) {
                conteo.push({
                    categoria: categoria.tipo,
                    cantidad: cantidad
                });
            }
        });
        return conteo;
    };

    const datosCategorias = contarEventosPorCategoria();
    const labels = datosCategorias.map(dato => dato.categoria);
    const data = datosCategorias.map(dato => dato.cantidad);

    return (
        <Bar
            options={options}
            data={{
                labels: labels,
                datasets: [
                    {
                        label: 'Eventos',
                        data: data,
                        backgroundColor: 'rgba(75, 192, 192, 1)',
                    }
                ],
            }}
        />
    );
};

export default GrafCantidadPorCategoria;