let miSistema = new Sistema()

miSistema.preCargarSelDepartamentoCensistas()
miSistema.preCargarSelDepartamentoInvitados()
miSistema.preCargarCensistas()
miSistema.preCargarCensosValidados()
miSistema.preCargarCensosNoValidados()



//** LOGIN / CERRAR SESIÓN -- CENCISTA / INVITADO **//
//Inicia sesion con el censista si los datos ingresados son correctos
function iniciarSesionCensistaUI() {
    let usuario = document.querySelector("#divLoginCensista--txtUsuario").value;
    usuario = usuario.toLowerCase()
    let contra = document.querySelector("#divLoginCensista--txtContrasena").value;
    let mensaje = ""
    if (miSistema.validarHayAlgo(usuario) && miSistema.validarHayAlgo(contra) && miSistema.validarContra(contra)) {
        let ingreso = miSistema.loginCencista(usuario, contra)
        if (ingreso) {
            displayFormularioCensista();
            miSistema.preCargarSelReasignarCenso()
            miSistema.preCargarSelValidarCenso();
        } else {
            mensaje = "Error datos incorrectos"
        }
    } else {
        mensaje = "Formato incorrecto de ingresos."
    }
    document.querySelector("#divLoginCensista--pError").innerHTML = mensaje
}
//"inicia sesion" al invitado si su cedula ingresada es correcta y no esta censado
function iniciarSesionInvitadoUI() {
    let cedula = document.querySelector("#divVerificarInvitado--txtCI").value
    if (cedula.trim().length == 9) {
        cedula = "0." + cedula
    }
    if (!miSistema.validarCensado(cedula)) {
        if (miSistema.validarFormatoCedula(cedula) && miSistema.validarCedula(cedula)) {
            datosSistema = miSistema.buscarDatos(cedula)
            if (datosSistema != null) {
                document.querySelector("#divPrecompletarCenso--txtNombre").value = datosSistema.nombre
                document.querySelector("#divPrecompletarCenso--txtApellido").value = datosSistema.apellido
                document.querySelector("#divPrecompletarCenso--txtEdad").value = datosSistema.edad
                document.querySelector("#divPrecompletarCenso--txtCedula").value = datosSistema.cedula
                document.querySelector("#divPrecompletarCenso--slcDepartamento").value = datosSistema.departamento
                document.querySelector("#divPrecompletarCenso--slcOcupacion").value = datosSistema.ocupacion
            }
            document.querySelector("#divPrecompletarCenso--txtCedula").value = cedula
            document.querySelector("#divPrecompletarCenso--txtCedula").disabled = true
            displayFormularioInvitado()
        } else {
            document.querySelector("#divVerificarInvitado--pError").innerHTML = "Formato de cedula erroneo debe contener el formato: 1.111.111-1<br>si este es el caso entonces cedula erronea, revise el numero verificador";
        }
    } else {
        document.querySelector("#divVerificarInvitado--pError").innerHTML = "Este usuario ya ha sido censado";
    }
}
//Se cierran y reinician elementos del UI
function cerrarSesionInvitadoUI() {
    //Reiniciar formulario invitados
    document.querySelector("#divPrecompletarCenso--txtNombre").value = ""
    document.querySelector("#divPrecompletarCenso--txtApellido").value = ""
    document.querySelector("#divPrecompletarCenso--txtEdad").value = ""
    document.querySelector("#divPrecompletarCenso--txtCedula").value = ""
    document.querySelector("#divPrecompletarCenso--slcDepartamento").value = "-1"
    document.querySelector("#divPrecompletarCenso--slcOcupacion").value = "-1"
    document.querySelector("#divVerificarInvitado--txtCI").value = ""
    document.querySelector("#divPrecompletarCenso--pErrorPrecompletarCenso").innerHTML = ""
    document.querySelector("#divVerificarInvitado--pError").innerHTML = ""
    hideAll()
}
//Se cierran y reinician elementos del UI
function cerrarSesionCensistaUI() {
    if (miSistema.logoutCensista()) {
        //Reiniciar formulario censistas
        document.querySelector("#divCompletarCenso--txtNombre").value = ""
        document.querySelector("#divCompletarCenso--txtApellido").value = ""
        document.querySelector("#divCompletarCenso--txtEdad").value = ""
        document.querySelector("#divCompletarCenso--txtCedula").value = ""
        document.querySelector("#divCompletarCenso--slcDepartamentoB").value = "-1"
        document.querySelector("#divCompletarCenso--slcOcupacion").value = "-1"
        document.querySelector("#divValidarPrecargas--slcMisAsignados").value = "-1"
        document.querySelector("#divValidarPrecargas--slcReasignar").value = "-1"
        document.querySelector("#divLoginCensista--txtUsuario").value = ""
        document.querySelector("#divLoginCensista--txtContrasena").value = ""
        document.querySelector("#divRegistroCensista--txtNombre").value = ""
        document.querySelector("#divRegistroCensista--txtUsuario").value = ""
        document.querySelector("#divRegistroCensista--txtContrasena").value = ""
        document.querySelector("#divRegistroCensista--txtRepContrasena").value = ""
        document.querySelector("#divVerificarInvitado--txtCI").value=""
        document.querySelector("#divCompletarCenso--pErrorCompletarCenso").innerHTML = ""
        document.querySelector("#divValidarPrecargas--pMostrarCenso").innerHTML = "";

        document.querySelector("#divValidarPrecargas--slcReasignar").disabled = true;
        document.querySelector("#divValidarPrecargas--slcReasignar").value = -1;
        document.querySelector("#divValidarPrecargas--btnReasignar").disabled = true;
        document.querySelector("#divValidarPrecargas--pErrorslcValidar").innerHTML = "";
        hideAll()
    } else {
        console.log("ERROR AL CERRAR SESION")
    }
}
//** FIN LOGIN / CERRAR SESIÓN -- CENCISTA / INVITADO **//


//** INICIO FORMULARIO CENSO ADMIN **//
//Se busca los datos de la cedula ingresada y si los encuentra los ingresa en el value de los campos de texto y selects
function buscarPrecargaAdmin() {
    let cedula = document.querySelector("#divCompletarCenso--txtCedula").value;
    let mensaje = ""
    if (cedula.length == 9) {
        cedula = "0." + cedula
    }
            document.querySelector("#divCompletarCenso--txtNombre").value = ""
            document.querySelector("#divCompletarCenso--txtApellido").value = ""
            document.querySelector("#divCompletarCenso--txtEdad").value = ""
            document.querySelector("#divCompletarCenso--slcDepartamentoB").value = -1
            document.querySelector("#divCompletarCenso--slcOcupacion").value = -1
    if (miSistema.validarFormatoCedula(cedula) && miSistema.validarCedula(cedula)) {
        datosSistema = miSistema.buscarDatos(cedula)
        if (datosSistema != null) {
            document.querySelector("#divCompletarCenso--txtNombre").value = datosSistema.nombre
            document.querySelector("#divCompletarCenso--txtApellido").value = datosSistema.apellido
            document.querySelector("#divCompletarCenso--txtEdad").value = datosSistema.edad
            document.querySelector("#divCompletarCenso--txtCedula").value = datosSistema.cedula
            document.querySelector("#divCompletarCenso--slcDepartamentoB").value = datosSistema.departamento
            document.querySelector("#divCompletarCenso--slcOcupacion").value = datosSistema.ocupacion
            if(datosSistema.Censista.id!=miSistema.usuarioLogueado.id){
                mensaje=`Esta persona esta asignada al censista: ${datosSistema.Censista.nombre}<br>Aún así, puede validarla o no`
            }else{
                mensaje="Esta persona esta asignada a usted"
            }

        } else if (!miSistema.buscarCedula(cedula)) {
            mensaje = "Esta persona no tiene datos precargados"
        }
        if (miSistema.buscarCedula(cedula) && miSistema.validarCensado(cedula)) {
            mensaje = "Esta persona ya fue censada"
        }
    }else{
        mensaje="Cedula ingresada invalida"
    }
    document.querySelector("#divCompletarCenso--pErrorCompletarCenso").innerHTML = mensaje
}
//Se toman los valores y se validan uno por uno, una vez todos validos se envian como parametro a una funcion, si la cedula no esta en el sistema se crea el censo
//Si la cedula existia en el sistema entonces se actualizan sus datos, en ambos casos se valida automaticamente
function guardarCensoAdminUI() {
    let mensaje = ""
    let nombre = document.querySelector("#divCompletarCenso--txtNombre").value;
    let apellido = document.querySelector("#divCompletarCenso--txtApellido").value;
    let edad = document.querySelector("#divCompletarCenso--txtEdad").value;
    let cedula = document.querySelector("#divCompletarCenso--txtCedula").value;
    if (cedula.length == 9) {
        cedula = "0." + cedula
    }
    let departamento = document.querySelector("#divCompletarCenso--slcDepartamentoB").value;
    let ocupacion = document.querySelector("#divCompletarCenso--slcOcupacion").value;
    let validaciones = 0
    if (miSistema.validarHayAlgo(nombre) && miSistema.validarMayus(nombre)&&miSistema.validarHaySoloLetras(nombre)) {
        validaciones += 1
    } else {
        mensaje += "Ingrese un nombre válido, con mayúscula al inicio y solo letras (ignore tildes)<br>"
    }

    if (miSistema.validarHayAlgo(apellido) && miSistema.validarMayus(apellido)&&miSistema.validarHaySoloLetras(apellido)) {
        validaciones += 1
    } else {
        mensaje += "Ingrese un apellido válido, con mayúscula al inicio y solo letras (ignore tildes) <br>"
    }

    if (miSistema.validarHayAlgo(edad) && miSistema.validarEdad(edad)) {
        validaciones += 1
    } else {
        mensaje += "Ingrese una edad válida entre 0 y 130 <br>"
    }

    if (miSistema.validarHayAlgo(cedula) && miSistema.validarFormatoCedula(cedula) && miSistema.validarCedula(cedula)) {
        validaciones += 1
    } else {
        mensaje += "Ingrese una cedula válida como en el ingreso anterior <br>"
    }

    if (departamento != -1) {
        validaciones += 1
    } else {
        mensaje += "Seleccione un departamento válido <br>"
    }

    if (ocupacion != -1) {
        validaciones += 1
    } else {
        mensaje += "Seleccione su estado ocupacional <br>"
    }
    if (validaciones == 6) {

        if (miSistema.buscarCedula(cedula) && miSistema.validarCensado(cedula)) {
            mensaje = "Esta persona ya fue censada, no se han realizado cambios"
        } else if (!miSistema.buscarCedula(cedula)) {
            miSistema.crearCensoAdmin(cedula, nombre, apellido, edad, departamento, ocupacion)
            mensaje = "Censo creado exitosamente, ¡¡¡MUCHAS GRACIAS!!!"
        } else if (miSistema.buscarCedula(cedula) && !miSistema.validarCensado(cedula)) {
            miSistema.actualizarCensoAdmin(cedula, nombre, apellido, edad, departamento, ocupacion)
            mensaje = "Censo validado exitosamente, ¡¡¡MUCHAS GRACIAS!!!"
            document.querySelector("#divCompletarCenso--txtCedula").value=""
            document.querySelector("#divCompletarCenso--txtNombre").value=""
            document.querySelector("#divCompletarCenso--txtApellido").value=""
            document.querySelector("#divCompletarCenso--txtEdad").value=""
            document.querySelector("#divCompletarCenso--slcDepartamentoB").value=-1
            document.querySelector("#divCompletarCenso--slcOcupacion").value=-1
        }
    }
    document.querySelector("#divCompletarCenso--pErrorCompletarCenso").innerHTML = mensaje;
}
//** FIN FORMULARIO CENSO ADMIN **// 



//** INICIO FORMULARIO CENSO PRECOMPLETAR  **//
//Se toman los valores y se validan uno por uno, una vez todos validos se envian como parametro a una funcion, si la cedula no esta en el sistema se crea el censo
//Si la cedula existia en el sistema entonces se actualizan sus datos, en ambos casos no se valida
function guardarCensoInvitadoUI() {
    let mensaje = ""
    let nombre = document.querySelector("#divPrecompletarCenso--txtNombre").value;
    let apellido = document.querySelector("#divPrecompletarCenso--txtApellido").value;
    let edad = document.querySelector("#divPrecompletarCenso--txtEdad").value;
    let cedula = document.querySelector("#divPrecompletarCenso--txtCedula").value;
    if (cedula.length == 9) {
        cedula = "0." + cedula
    }
    let departamento = document.querySelector("#divPrecompletarCenso--slcDepartamento").value;
    let ocupacion = document.querySelector("#divPrecompletarCenso--slcOcupacion").value;
    let validaciones = 0
    if (miSistema.validarHayAlgo(nombre) && miSistema.validarMayus(nombre)&&miSistema.validarHaySoloLetras(nombre)) {
        validaciones += 1
    } else {
        mensaje += "Ingrese un nombre válido, con mayúscula al inicio y solo letras (ignore tildes) <br>"
    }

    if (miSistema.validarHayAlgo(apellido) && miSistema.validarMayus(apellido)&&miSistema.validarHaySoloLetras(apellido)) {
        validaciones += 1
    } else {
        mensaje += "Ingrese un apellido válido, con mayúscula al inicio y solo letras (ignore tildes) <br>"
    }

    if (miSistema.validarHayAlgo(edad) && miSistema.validarEdad(edad)) {
        validaciones += 1
    } else {
        mensaje += "Ingrese una edad válida entre 0 y 130 <br>"
    }

    if (miSistema.validarFormatoCedula(cedula) && miSistema.validarCedula(cedula)) {
        validaciones += 1
    } else {
        mensaje += "Ingrese una cedula válida como en el ingreso anterior <br>"
    }

    if (departamento != -1) {
        validaciones += 1
    } else {
        mensaje += "Seleccione un departamento válido <br>"
    }

    if (ocupacion != -1) {
        validaciones += 1
    } else {
        mensaje += "Seleccione su estado ocupacional <br>"
    }

    if (validaciones == 6) {

        if (miSistema.buscarCedula(cedula)) {
            miSistema.editarCenso(cedula, nombre, apellido, edad, departamento, ocupacion)
            let censistaAsignado = miSistema.obtenerNombreCensistaAsignado(cedula)
            mensaje = `Datos actualizados exitosamente, próximamente ${censistaAsignado} se acercará a su casa para validar su ingreso ¡¡¡MUCHAS GRACIAS!!!`
        } else {
            miSistema.crearCenso(cedula, nombre, apellido, edad, departamento, ocupacion)
            let censistaAsignado = miSistema.obtenerNombreCensistaAsignado(cedula)
            mensaje = `Censo creado exitosamente, próximamente ${censistaAsignado} se acercará a su casa para validar su ingreso ¡¡¡MUCHAS GRACIAS!!!`
        }
    }
    document.querySelector("#divPrecompletarCenso--pErrorPrecompletarCenso").innerHTML = mensaje;
}
//Se envia la cedula para buscar si existe en el sistema, si se encuentra se borra el objeto con ese identificador, sino entonces se muestra un error
function borrarCensoPrecompletado() {
    let cedula = document.querySelector("#divPrecompletarCenso--txtCedula").value;
    let mensaje = ""
    if (cedula != "") {
        if (miSistema.buscarCedula(cedula)) {
            let censoBorrado = miSistema.borrarCensoInvitado(cedula)
            console.log(censoBorrado)
            if (censoBorrado != null) {
                mensaje = "Formulario eliminado exitosamente"
                document.querySelector("#divPrecompletarCenso--txtNombre").value=""
                document.querySelector("#divPrecompletarCenso--txtApellido").value=""
                document.querySelector("#divPrecompletarCenso--txtEdad").value=""
                document.querySelector("#divPrecompletarCenso--slcDepartamento").value=-1
                document.querySelector("#divPrecompletarCenso--slcOcupacion").value=-1
            } else {
                mensaje = "Error al borrar (sistema)"
            }
        } else {
            mensaje = "Error al borrar"
        }
    } else {
        mensaje = "Ingrese algo en la cedula"
    }
    document.querySelector("#divPrecompletarCenso--pErrorPrecompletarCenso").innerHTML = mensaje
}
//** FIN FORMULARIO CENSO PRECOMEPLTAR **// 

//Registra un nuevo censista en el sistema y lo loguea
//** REGISTRO CENCISTA **//
function registroCencistaUI() {
    let nombre = document.querySelector("#divRegistroCensista--txtNombre").value
    let usuario = document.querySelector("#divRegistroCensista--txtUsuario").value
    usuario = usuario.toLowerCase()
    let contraseña = document.querySelector("#divRegistroCensista--txtContrasena").value
    let contraseñaRepe = document.querySelector("#divRegistroCensista--txtRepContrasena").value
    let mensaje = ""
    if (miSistema.validarHayAlgo(nombre) && miSistema.validarHayAlgo(usuario) && miSistema.validarHayAlgo(contraseña)) {
        if (miSistema.validarHaySoloLetras(nombre)) {
            if (miSistema.validarContra(contraseña) && miSistema.validarMayus(nombre)) {
                if (contraseña === contraseñaRepe) {
                    let registrado = miSistema.buscarUsuario(usuario)
                    if (!registrado) {
                        miSistema.registroCencista(usuario, nombre, contraseña)
                        if (miSistema.loginCencista(usuario, contraseña)) {
                            displayFormularioCensista()
                            miSistema.preCargarSelReasignarCenso()
                        }
                    } else {
                        mensaje = "ERROR: Usuario ya registrado, cambie su username"
                    }
                } else {
                    mensaje = "ERROR: contraseñas no coinciden"
                }
            } else {
                mensaje = "ERROR: La primera letra del nombre ha de ser mayuscula o contraseña invalida"
            }
        } else {
            mensaje = "ERROR: El nombre solo debe contener letras (ignorar tildes)"
        }

    } else { mensaje = "ERROR: Rellene todos los campos" }
    document.querySelector("#divRegistroCensista--pError").innerHTML = mensaje;
}
//** FIN REGISTRO CENCISTA **//



//** INICIO VISUALIZAR CENSOS ASIGNADOS **//
//Pide la seleccion de una cedula y muestra los datos asociados a esta
function visualizarCensoPrecompletadoUI() {
    let cedula = document.querySelector("#divValidarPrecargas--slcMisAsignados").value
    if (cedula != -1) {
        document.querySelector("#divValidarPrecargas--slcReasignar").disabled = false;
        document.querySelector("#divValidarPrecargas--btnReasignar").disabled = false;
        document.querySelector("#divValidarPrecargas--pMostrarCenso").innerHTML = miSistema.previsualizarCensoValidar(cedula)
    } else {
        document.querySelector("#divValidarPrecargas--pErrorslcValidar").innerHTML = "ERROR, SELECCIONE ALGUIEN A REASIGNAR"
    }
}

//** FIN VISUALIZAR CENSOS ASIGNADOS **//



//** INICIO REASIGNAR CENSO PRECOMPLETADO **//
//Con la cedula seleccionada se le asigna un censista elegido de un select en el que no aparece el usuario logueado actual
function reasignarCensoPrecompletadoUI() {
    let censoReasignar = document.querySelector("#divValidarPrecargas--slcMisAsignados").value
    let reasignarA = document.querySelector("#divValidarPrecargas--slcReasignar").value
    let mensaje = ""
    if (censoReasignar != -1 && reasignarA != -1) {
        miSistema.reasignarCensoSeleccionado(censoReasignar, reasignarA)
        mensaje = "¡Censo reasignado exitosamente!"
        miSistema.preCargarSelValidarCenso()
        document.querySelector("#divValidarPrecargas--pMostrarCenso").innerHTML = "";

        document.querySelector("#divValidarPrecargas--slcReasignar").disabled = true;
        document.querySelector("#divValidarPrecargas--slcReasignar").value = -1;
        document.querySelector("#divValidarPrecargas--btnReasignar").disabled = true;
    } else {
        mensaje = "Error al reasignar censo"
    }
    document.querySelector("#divValidarPrecargas--pErrorslcValidar").innerHTML = mensaje
}
//** FIN REASIGNAR CENSO PRECOMPLETADO **//

//** MOSTRAR / OCULTAR ELEMENTOS UI **//
//Oculta los divs ingresados por el array parametro
function ocultarUI(pListaDivs) {
    for (let div of pListaDivs) {
        document.querySelector(`#${div}`).style.display = "none";
    }
};
//Muestra los divs ingresados por el array parametro
function mostrarUI(pListaDivs) {
    for (let div of pListaDivs) {
        document.querySelector(`#${div}`).style.display = "block";
    }
};


// Pantalla de Inicio
//Muestra y oculta divs para manejarnos por la interfaz
function hideAll() {
    document.querySelector("#divVerificarInvitado--txtCI").innerHTML=""
    document.querySelector("#divVerificarInvitado--pEstadisticas").innerHTML=""
    document.querySelector("#divVerificarInvitado--pError").innerHTML=""
    let txtdivsCerrar = "divInicio--divVerificarInvitado, divInicio--divInicioCensista, divInicio--divLoginCensista, divInicio--divRegistroCensista, divInterfazInvitado, divInterfazCensista"
    let txtdivsMostrar = "divInicio, divInicio--divBienvenida"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
};
hideAll();

// Pantalla donde el nvitado ingresa su cédula
//Muestra y oculta divs para manejarnos por la interfaz
function displayLoginInvitado() {
    let txtdivsCerrar = "divInicio--divBienvenida, divInicio--divInicioCensista, divInicio--divLoginCensista, divInicio--divRegistroCensista, divInterfazInvitado, divInterfazCensista"
    let txtdivsMostrar = "divInicio, divInicio--divVerificarInvitado"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
};

// Pantalla donde el Censista elije si registrarse o loguearse
//Muestra y oculta divs para manejarnos por la interfaz
function displayInicioCensista() {
    let txtdivsCerrar = "divInicio--divBienvenida, divInicio--divVerificarInvitado, divInicio--divLoginCensista, divInicio--divRegistroCensista, divInterfazInvitado, divInterfazCensista"
    let txtdivsMostrar = "divInicio, divInicio--divInicioCensista"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
    document.querySelector("#divRegistroCensista--txtNombre").value=""
    document.querySelector("#divRegistroCensista--txtUsuario").value=""
    document.querySelector("#divRegistroCensista--txtContrasena").value=""
    document.querySelector("#divRegistroCensista--txtRepContrasena").value=""
    document.querySelector("#divLoginCensista--txtUsuario").value=""
    document.querySelector("#divLoginCensista--txtContrasena").value=""
    document.querySelector("#divRegistroCensista--pError").innerHTML=""
};

// Pantalla para que se loguee el cencista
//Muestra y oculta divs para manejarnos por la interfaz
function displayLoginCensista() {
    let txtdivsCerrar = "divInicio--divBienvenida, divInicio--divVerificarInvitado, divInicio--divInicioCensista, divInicio--divRegistroCensista, divInterfazInvitado, divInterfazCensista"
    let txtdivsMostrar = "divInicio, divInicio--divLoginCensista"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
};

// Pantalla para que se registre el censista
//Muestra y oculta divs para manejarnos por la interfaz
function displayRegistroCensista() {
    let txtdivsCerrar = "divInicio--divBienvenida, divInicio--divVerificarInvitado, divInicio--divInicioCensista, divInicio--divLoginCensista, divInterfazInvitado, divInterfazCensista"
    let txtdivsMostrar = "divInicio, divInicio--divRegistroCensista"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
};

// Pantalla de formulario para precompletar censo
//Muestra y oculta divs para manejarnos por la interfaz
function displayFormularioInvitado() {
    let txtdivsCerrar = "divInicio, divInterfazInvitado--divEstadisticas, divInterfazCensista"
    let txtdivsMostrar = "divInterfazInvitado, divInterfazInvitado--divOpcionesInvitado, divInterfazInvitado--divPrecompletarCenso"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
};

// Pantalla para que el invitado vea estadísticas del censo
//Muestra y oculta divs para manejarnos por la interfaz y carga estadisticas
function displayEstadisticasInvitado() {
    let txtdivsCerrar = "divInicio, divInterfazInvitado--divPrecompletarCenso, divInterfazCensista"
    let txtdivsMostrar = "divInterfazInvitado, divInterfazInvitado--divOpcionesInvitado, divInterfazInvitado--divEstadisticas"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
    //llamamos a la tabla para actualizar la info cada vez que entramos a esta sección
    miSistema.cargarEstadisticasInvitado()
};

// Pantalla para que el censista registre los datos del censado
//Muestra y oculta divs para manejarnos por la interfaz
function displayFormularioCensista() {
    let txtdivsCerrar = "divInicio, divInterfazInvitado, divInterfazCensista--divValidarPrecargas, divInterfazCensista--divVerEstadisticas"
    let txtdivsMostrar = "divInterfazCensista, divInterfazCensista--divOpcionesCensista, divInterfazCensista--divCompletarCenso"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
};

// Pantalla para que el censita consulte que formularios tiene para verificar y los reasigne
//Muestra y oculta divs para manejarnos por la interfaz y habilita select y boton
function displayFormulariosPorVerificar() {
    let txtdivsCerrar = "divInicio, divInterfazInvitado, divInterfazCensista--divCompletarCenso, divInterfazCensista--divVerEstadisticas"
    let txtdivsMostrar = "divInterfazCensista, divInterfazCensista--divOpcionesCensista, divInterfazCensista--divValidarPrecargas"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
    document.querySelector("#divValidarPrecargas--slcReasignar").disabled = true;
    document.querySelector("#divValidarPrecargas--btnReasignar").disabled = true;
    miSistema.preCargarSelValidarCenso()
    document.querySelector("#divValidarPrecargas--slcMisAsignados").value=-1
    document.querySelector("#divValidarPrecargas--pMostrarCenso").innerHTML=""
    document.querySelector("#divValidarPrecargas--pErrorslcValidar").innerHTML=""
};

// Pantalla para que el censista consulte estadísticas
//Muestra y oculta divs para manejarnos por la interfaz y carga las estadisticas del censista
function displayEstadisticasCensista() {
    let txtdivsCerrar = "divInicio, divInterfazInvitado, divInterfazCensista--divCompletarCenso, divInterfazCensista--divValidarPrecargas"
    let txtdivsMostrar = "divInterfazCensista, divInterfazCensista--divOpcionesCensista, divInterfazCensista--divVerEstadisticas"
    let divsCerrar = txtdivsCerrar.split(", ")
    let divsMostrar = txtdivsMostrar.split(", ")
    ocultarUI(divsCerrar)
    mostrarUI(divsMostrar)
    //llamamos a la tabla para actualizar la info cada vez que entramos a esta sección
    miSistema.cargarEstadisticasAdmin()
    document.querySelector("#divVerEstadisticas--slcDepartamentos").value =-1
    document.querySelector("#divVerEstadisticas--pFranjaEtaria").innerHTML=""
};


//Esta funcion fue necesaria para hacer el array iterable (propuesta por el profesor Gastón Mateo)
function porcentajeFranjaEtariaUI() {
    miSistema.porcentajeFranjaEtaria()
}


//Eventos de botones
function bloqueEventosBotonesInicio() {
    //** UI INICIO **//
    document.querySelector("#divBienvenida--btnInvitado").addEventListener("click", displayLoginInvitado);
    document.querySelector("#verificarInvitado--btnRegresar").addEventListener("click", hideAll);
    document.querySelector("#divBienvenida--btnCensista").addEventListener("click", displayInicioCensista);
    document.querySelector("#divInicioCensista--btnRegresar").addEventListener("click", hideAll);
    document.querySelector("#divInicioCensista--btnLogin").addEventListener("click", displayLoginCensista);
    document.querySelector("#loginCensista--btnRegresar").addEventListener("click", displayInicioCensista);
    document.querySelector("#divInicioCensista--btnRegistro").addEventListener("click", displayRegistroCensista);
    document.querySelector("#divRegistroCensista--btnRegistrar").addEventListener("click", registroCencistaUI);
    document.querySelector("#divRegistroCensista--btnRegresar").addEventListener("click", displayInicioCensista);
    //** FIN UI INICIO **//


    //** UI INVITADO **//
    document.querySelector("#divOpcionesInvitado--btnPrecompletarCenso").addEventListener("click", displayFormularioInvitado);
    document.querySelector("#divOpcionesInvitado--btnEstadisticas").addEventListener("click", displayEstadisticasInvitado);
    document.querySelector("#divPrecompletarCenso--btnGuardar").addEventListener("click", guardarCensoInvitadoUI);
    document.querySelector("#divPrecompletarCenso--btnEliminar").addEventListener("click", borrarCensoPrecompletado)
    document.querySelector("#divVerificarInvitado--btnEstadisticas").addEventListener("click", cargarEstadisticasInvitadoInicioUI)

    function cargarEstadisticasInvitadoInicioUI(){
        miSistema.cargarEstadisticasInvitadoInicio()
    }
    //** FIN UI INVITADO **//


    //** UI CENSITA **//
    document.querySelector("#divOpcionesCensista--btnCompletarCenso").addEventListener("click", displayFormularioCensista);
    document.querySelector("#divOpcionesCensista--btnPreCompletados").addEventListener("click", displayFormulariosPorVerificar);
    document.querySelector("#divOpcionesCensista--btnEstadisticas").addEventListener("click", displayEstadisticasCensista);
    document.querySelector("#divCompletarCenso--btnGuardar").addEventListener("click", guardarCensoAdminUI);
    document.querySelector("#divdivValidarPrecargas--btnBuscar").addEventListener("click", visualizarCensoPrecompletadoUI);
    document.querySelector("#divValidarPrecargas--btnReasignar").addEventListener("click", reasignarCensoPrecompletadoUI);
    document.querySelector("#divCompletarCenso--btnBuscarCedula").addEventListener("click", buscarPrecargaAdmin);
    document.querySelector("#divVerEstadisticas--btnMostrar").addEventListener("click", porcentajeFranjaEtariaUI)
    //** FIN UI CENSITA **//



    //** LOGIN / LOG-OUT **//
    //Censista
    document.querySelector("#divLoginCensista--btnIngresar").addEventListener("click", iniciarSesionCensistaUI);
    document.querySelector("#divOpcionesCensista--btnCerrarSesion").addEventListener("click", cerrarSesionCensistaUI);
    //Invitado
    document.querySelector("#divVerificarInvitado--btnIngresar").addEventListener("click", iniciarSesionInvitadoUI);
    document.querySelector("#divOpcionesInvitado--btnCerrarSesion").addEventListener("click", cerrarSesionInvitadoUI);
    //** FIN LOGIN / LOG-OUT **//
};
bloqueEventosBotonesInicio();
//** FIN MOSTRAR / OCULTAR ELEMENTOS UI **//


//Funciones para consolear datos utiles
//** INICIO CONSOLA TEST ARRAY**//
function consolearArray() {
    console.log("Contenido del arrayCensos:", miSistema.arrayCensos)
};

function bloqueEventosConsolaTest() {
    document.querySelector("#divInicioCensista--btnLogin").addEventListener("click", consolearArray)
    document.querySelector("#divVerificarInvitado--btnIngresar").addEventListener("click", consolearArray)
    document.querySelector("#divPrecompletarCenso--btnGuardar").addEventListener("click", consolearArray)
    document.querySelector("#divPrecompletarCenso--btnEliminar").addEventListener("click", consolearArray)
    document.querySelector("#divCompletarCenso--btnGuardar").addEventListener("click", consolearArray)
}
bloqueEventosConsolaTest();
//** FIN CONSOLA TEST ARRAY**//