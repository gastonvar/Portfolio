import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    isLogged: localStorage.getItem("username")!=null
}

export const loggerSlice = createSlice({
    name:"logged",
    initialState,
    reducers:{
        LoggedBoolTrue: state => {
            state.isLogged = true
        },
        LoggedBoolFalse: state => {
            state.isLogged = false
        }
    }
})

export const {LoggedBoolTrue} = loggerSlice.actions;
export const {LoggedBoolFalse} = loggerSlice.actions;
export default loggerSlice.reducer;