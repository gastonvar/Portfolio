import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    eventos: [],
    eventosPedidos:false
}

export const eventosSlice = createSlice({
    name:"eventos",
    initialState,
    reducers:{
        guardarEventos: (state,action) => {
            state.eventos = action.payload
            console.log(state.eventos)
        },
        agregarEvento:(state,action)=>{
            console.log("Payload:", action.payload);
            state.eventos.push(action.payload)
            console.log("Console log del store")
            console.log(state.eventos)
        },
        eliminarEvento:(state,action)=>{
            state.eventos = state.eventos.filter(evento => evento.id !== action.payload)
        },
        Pedidos:(state)=>{
            state.eventosPedidos = true
    }}
})

export const {guardarEventos, agregarEvento, eliminarEvento, Pedidos} = eventosSlice.actions;
export default eventosSlice.reducer;