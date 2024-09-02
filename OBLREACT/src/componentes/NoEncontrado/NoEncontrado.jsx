import React, { useState } from 'react'
import './NoEncontrado.css'
import { useSelector } from 'react-redux'

const NoEncontrado = () => {
    const alguienLoggeado = useSelector(state => state.logger.isLogged);

  return (
    <div className='noencontrado-container'>
        <h1>404</h1>
        <h2>Página no encontrada</h2>
        <div className='bubble-chat-container1'>
            <img src="../../../media/shhhh.png" alt="Imagen no encontrada" />
            <div className="bubble-chat">
                <p>Andas perdid@? Yo te ayudo</p>
                {alguienLoggeado ? <a href="/dashboard">Dashboard</a> : <a href="/">Login&Signin</a>}
            </div>
        </div>
        <div className='bubble-chat-container2'>
            <img src="../../../media/shhhh2.png" alt="Imagen no encontrada"/>
            <div className="bubble-chat">
                <p>Esta vez te dejaremos ir...</p>
            </div>
        </div>
    </div>
  )
}

export default NoEncontrado
