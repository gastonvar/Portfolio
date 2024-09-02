import { configureStore } from "@reduxjs/toolkit";
import loggerReducer from "../features/loggerSlice"
import categoriasReducer from "../features/categoriasSlice"
import eventosReducer from "../features/eventosSlice"
export const store = configureStore({
    reducer:{
        logger:loggerReducer,
        categorias: categoriasReducer,
        eventos: eventosReducer,
    }
})