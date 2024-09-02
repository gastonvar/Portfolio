import React, { useEffect } from "react";
import Login from "../Login/Login.jsx";
import "./LoginSignin.css";
import SignIn from "../SignIn/SignIn.jsx";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

const LoginSignin = () => {
  const navigate = useNavigate();
  const logged = useSelector(state=>state.logger.isLogged);
  useEffect(() => {
    if(logged){
     navigate("/dashboard"); 
    }
  },[]);
  return (
    <div className="row pt-4 mt-5 justify-content-center">
      <div className="col-md-5 mb-5">
        <Login />
      </div>
      <div className="col-md-5 mb-5">
        <SignIn />
      </div>
    </div>
  );
};

export default LoginSignin;
