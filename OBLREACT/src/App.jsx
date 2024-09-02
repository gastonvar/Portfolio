import { useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import "./App.css";
import Header from "./componentes/Header/Header";
import LoginSignin from "./componentes/LoginSignin/LoginSignin";
import { Provider } from "react-redux";
import { store } from "./store/store";
import Dashboard from "./componentes/Dashboard/Dashboard";
import NoEncontrado from "./componentes/NoEncontrado/NoEncontrado";



const App = () => {
  return (
    <div className="container-fluid app-container">
      <div className="row mt-5 app-container-insider justify-content-center">
        <Provider store={store}>
          <ToastContainer />
          <BrowserRouter>
            <Header></Header>
            <Routes>
              <Route path="/" element={<LoginSignin />} />
              <Route path="/dashboard" element={<Dashboard />} />
              <Route path="*" element={<NoEncontrado/>} />
            </Routes>
          </BrowserRouter>
        </Provider>
      </div>
    </div>
  );
};

export default App;
