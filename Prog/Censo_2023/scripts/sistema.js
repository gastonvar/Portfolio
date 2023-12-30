class Sistema {

    constructor() {
        //El usuario logueado
        this.usuarioLogueado = null
        //las listas
        this.arrayAdmins = new Array()
        this.arrayCensos = new Array()
    }


    //** LOGIN / CERRAR SESIÓN -- CENCISTA / INVITADO **//
    //La siguiente funcion elimina el usuario logueado en el sistema y retorna la variable "cerro" en true si el logout fue exitoso
    logoutCensista() {
        let cerro = false;
        if (this.usuarioLogueado !== null) { //Si hay algun usuario logueado
            this.usuarioLogueado = null; //Se cambia el valor de la variable a null
            cerro = true;
        };
        return cerro //Devuelve si fue posible cerrar sesion
    }

    //Inicia sesion en el sistema (como censista)
    loginCencista(pUsu, pContra) {
        let exitoso = false;
        let usuario = this.obtenerCensista(pUsu, pContra) //Obtiene el objeto censista
        if (usuario !== null) {
            if (usuario.contraseña === pContra) { //Si el objeto no es vacio y coincide la contra ingresada con la guardada en el sistema se loguea
                exitoso = true;
                this.usuarioLogueado = usuario;
                console.log(this.usuarioLogueado)
                this.preCargarSelValidarCenso() //Carga los censos pre completados por validar asignados al censista que se acaba de loguear
                document.querySelector("#divOpcionesCensista--usuarioLogueado").innerHTML = `<strong>Usuario Logueado:</strong> ${this.usuarioLogueado.usuario}` //Un mensaje en un div que muestra el usuario del censista logueado
            }
        }
        return exitoso //Devuelve si fue posible iniciar sesion
    }
    //Devuelve el objeto censista que fue encontrado mediante el usuario y la contraseña con el objetivo de iniciar sesion si el ingreso coincide con los datos en el array
    obtenerCensista(pUsu, pContra) {
        let encontrado = null //Funcion que recibe un usuario y contraseña como parametros
        let i = 0
        while (i < this.arrayAdmins.length && encontrado == null) {
            let censista = this.arrayAdmins[i] //Se empieza a buscar censista por censista hasta encontrar uno coincidente
            if (censista.usuario == pUsu) { //Si el usuario coincide con el parametro pUsu
                if (censista.contraseña == pContra) { //Y si la contraseña coincide con el parametro pContra
                    encontrado = censista //Se encontro el censista coincidente con esos datos y se guarda en encontrado
                }
            }
            i++
        }
        return encontrado //Se devuleve el objeto censista encontrado o null

    }
    //Busca el usuario ingresado del censista en el sistema y retorna true si lo encuentra y false si no
    buscarUsuario(pUsu) {
        let encontrado = false
        let i = 0
        while (i < this.arrayAdmins.length && !encontrado) {
            let censista = this.arrayAdmins[i]          //Se empieza a iterar por el array de censistas
            if (censista.usuario == pUsu) {     //Si existe un censista con ese nombre de usuario entonces encontrado=true
                encontrado = true
            }
            i++
        }
        return encontrado                   //Se devuleve encontrado (booleano)
    }
    //Busca si la cedula ingresada está en el sistema
    buscarCedula(pCedula) {
        let encontrado = false
        let i = 0
        while (i < this.arrayCensos.length && !encontrado) { //Se empieza a iterar por el array de censos
            let censo = this.arrayCensos[i]
            if (censo.cedula == pCedula) {                      //Si existe un censo con esa cedula entonces encontrado=true
                encontrado = true
            }
            i++
        }

        return encontrado //Se devuelve encontrado(booleano)
    }
    //** FIN LOGIN / CERRAR SESIÓN -- CENCISTA / INVITADO **//



    //** INICIO REGISTRO CENSISTA **//
    //Registra un nuevo censista
    registroCencista(pUsu, pNombre, pContra) {
        let censistaNuevo = this.crearCensista(pUsu, pNombre, pContra);        //Funcion que crea objetos censistas con datos cargados desde UI o precarga (validados desde la UI)
        if (censistaNuevo != null) {
            this.arrayAdmins.push(censistaNuevo);
        } else {
            console.log("Error en registroCensista")
        }

    }
    //** FIN REGISTRO CENSISTA **//

    //**INICIO BUSCAR DATOS PRECARGADOS**//
    //Busca al objeto invitado con la cedula ingresada y devuelve el objeto si lo encuentra
    buscarDatos(pCedula) {
        let encontrado = null
        let i = 0
        while (encontrado == null && i < this.arrayCensos.length) {
            let invitado = this.arrayCensos[i]
            if (invitado.cedula == pCedula) {                                       //Si encuentra una cedula coincidente con el parametro el objeto encontrado se carga en "encontrado"
                encontrado = invitado
            }
            i++
        }
        return encontrado   //Se devuelve un objeto o null
    }

    //**FIN BUSCAR DATOS PRECARGADOS**//

    //** VALIDACIONES **//

    //La siguiente funcion valida si en el parametro hay numeros
    validarHaySoloLetras(pNombre) {
        let soloLetras = true
        let i = 0
        while (i < pNombre.length && soloLetras) {
            let car = pNombre.charAt(i)
            let validacion1 = car.charCodeAt(0) >= 65 && car.charCodeAt(0) <= 90          //Valida caracter por caracter si es una letra, sino entonces se devuelve falso
            let validacion2 = car.charCodeAt(0) >= 97 && car.charCodeAt(0) <= 122
            if (!validacion1 && !validacion2) {
                soloLetras = false
            }
            i++
        }
        return soloLetras //Se devuelve un booleano
    }
    //La siguiente funcion valida si en el ingreso hay algo
    validarHayAlgo(i) {
        let algo = false;
        if (i.trim().length > 0) {
            algo = true;
        }
        return algo;
    };
    //Esta funcion valida el formato de la cedula, en el cual se considera la posicion de los "." y "-", en el caso de que la cedula no tenga millon se le agrega "0." con la finalidad de no 
    //hacer dos validaciones mas por tipo de cedula, a efectos finales esto es lo mismo pues el 0 al multiplicar no se toma en cuenta.
    validarFormatoCedula(pCedulas) {
        if (pCedulas.trim().length == 9) {
            pCedulas = "0." + pCedulas
        }
        let formatoValido = false
        if (pCedulas.trim().length == 11 && !isNaN(pCedulas.charAt(0)) && pCedulas.charAt(1) == "." && !isNaN(pCedulas.charAt(2)) && !isNaN(pCedulas.charAt(3)) && !isNaN(pCedulas.charAt(4)) && pCedulas.charAt(5) == "." && !isNaN(pCedulas.charAt(6)) && !isNaN(pCedulas.charAt(7)) && !isNaN(pCedulas.charAt(8)) && pCedulas.charAt(9) == "-" && !isNaN(pCedulas.charAt(10))) {
            formatoValido = true
        }
        return formatoValido
    }


    //Esta funcion realiza el calculo del numero verificador para validar la cedula, si la cedula no presenta el millon se le agrega "0." con el fin de optimizar los calculos
    //pues el 0 no se toma en cuenta, luego se le quitan los . y al final se compara el resultado de los calculos con el numero verificador de la cedula ingresada
    validarCedula(i) {
        if (i.trim().length == 9) {
            i = "0." + i
        }
        let cedulaValida = false
        let cedulaSinPtos = ""
        let numerosValidacion = "2987634"
        let AcumuladorNV = 0
        for (let q = 0; q <= i.trim().length - 3; q++) {
            if (i.charAt(q) == ".") {
                cedulaSinPtos += i.charAt(q + 1)
                q++
            } else { cedulaSinPtos += i.charAt(q) }
        }
        for (let o = 0; o <= 6; o++) {
            let carCedulaSP = cedulaSinPtos.charAt(o)
            let carnumVal = numerosValidacion.charAt(o)
            carCedulaSP = Number(carCedulaSP)
            carnumVal = Number(carnumVal)
            AcumuladorNV += carCedulaSP * carnumVal
        }
        let AcumuladorNV2 = AcumuladorNV
        while (AcumuladorNV2 % 10 != 0) {
            AcumuladorNV2++
        }
        let nroVerificador = AcumuladorNV2 - AcumuladorNV
        if (nroVerificador == i.charAt(10)) {
            cedulaValida = true
        }
        return cedulaValida
    }

    //Esta funcion valida que la edad este entre 0 y 130
    validarEdad(i) {
        let edadValida = false
        if (i >= 0 && i <= 130) {
            edadValida = true
        }
        return edadValida
    }

    //Esta funcion valida si el nombre/apellido ingresado empiezan con mayusculas
    validarMayus(i) {
        return i.charAt(0) === i.charAt(0).toUpperCase()
    }

    //Esta funcion valida si el ingreso no tiene numeros
    noNros(i) {
        let nros = "0123456789"
        let HayNros = false
        for (let p = 0; p < i.trim.length; p++) {
            carX = i.charAt(p)
            for (o = 0; o < nros.length; o++) {
                nroX = nros.charAt(o)
                if (carX == nroX) {
                    HayNros = true
                }
            }
        }
        return HayNros
    }


    //Esta funcion valida si la contraseña ingresada es valida
    validarContra(i) {
        let mayus = false
        let minus = false
        let numero = false
        if (i.trim().length >= 5) {
            for (let o = 0; o <= i.trim().length; o++) {
                let contraCode = i.charCodeAt(o)
                if (48 <= contraCode && contraCode <= 57) {
                    numero = true
                } else {
                    if (65 <= contraCode && contraCode <= 90) {
                        mayus = true
                    } else {
                        if (97 <= contraCode && contraCode <= 122) {
                            minus = true
                        }
                    }
                }
            }
        }
        return mayus && minus && numero         //Devuelve un booleano falso si no se cumple alguna de las 3 condiciones
    }

    //Esta funcion retorna true o false si la cedula esta censada ya
    validarCensado(pCedula) {
        let censadoVal = false
        let i = 0

        while (i < this.arrayCensos.length && censadoVal == false) {
            let censo = this.arrayCensos[i]
            if (censo.cedula == pCedula && censo.censado == true) {                     //Se itera por el array de censos y si se encuentra el censo cuya cedula coincide con el pCedula
                censadoVal = true                                                       //Se pregunta si el censo esta validado, si ese es el caso entonces se devuelve true
            }
            i++
        }

        return censadoVal                               //Se devuelve un booleano
    }
    //Con la id del censista se busca si existe en el sistema
    validarIDcensista(pCensistaID) {
        let encontrado = false
        let i = 0
        while (i < this.arrayAdmins.length && encontrado == false) {
            let admin = this.arrayAdmins[i]
            if (admin.id == pCensistaID) {
                encontrado = true                                       //Itera por el array de censistas y si se encuentra un censista cuya id coincide con el pCensistaID encontrado se torna true
            }
            i++
        }
        return encontrado                           //Devuelve un booleano
    }
    //** FIN VALIDACIONES **//



    //**INICIO BUSCAR CENSISTA ASIGNADO**//
    //Se busca mediante la cedula ingresada el censista asignado a ese objeto invitado
    buscarCensistaAsignado(pCedula) {
        let encontrado = false
        let nombreCensista = null
        let i = 0
        while (!encontrado && i < this.arrayCensos.length) {
            let censo = this.arrayCensos[i]
            if (censo.cedula == pCedula) {
                encontrado = true
                nombreCensista = `${censo.Censista.nombre} de ususario ${censo.Censista.usuario}`           //Se itera por el array de censos y se busca mediante la cedula al censista asignado a ese
            }                                                                                               //Objeto invitado
            i++
        }
        return nombreCensista
    }
    //**INICIO BUSCAR CENSISTA ASIGNADO**//



    //** PRECARGA DE SELECTORES **//
    //Devuelve un array con todos los departamentos
    arrayDepartamentos() {
        let departamentostxt = "Artigas, Canelones, Cerro Largo, Colonia, Durazno, Flores, Florida, Lavalleja, Maldonado, Montevideo, Paysandú, Río Negro, Rivera, Rocha, Salto, San José, Soriano, Tacuarembó, Treinta y Tres."
        let departamentos = departamentostxt.split(", ")
        return departamentos
    }
    //Con el array de departamentos se carga el select para los invitados
    preCargarSelDepartamentoInvitados() {
        let departamentos = this.arrayDepartamentos()
        let sel = ` <option value="-1">Seleccione departamento</option>`
        for (let depar of departamentos) {
            sel += `<option value="${depar}">${depar}</option>`
        }
        document.querySelector("#divPrecompletarCenso--slcDepartamento").innerHTML = sel
    }

    //Con el array de departamentos se carga el select para los censistas
    preCargarSelDepartamentoCensistas() {
        let departamentos = this.arrayDepartamentos()
        let sel = ` <option value="-1">Seleccione departamento</option>`
        for (let depar of departamentos) {
            sel += `<option value="${depar}">${depar}</option>`
        }
        document.querySelector("#divCompletarCenso--slcDepartamentoB").innerHTML = sel
    }
    //Se carga el select del censista para reasignar a una persona
    preCargarSelValidarCenso() {
        let sel = `<select id="divValidarPrecargas--slcMisAsignados"><option value=-1>Seleccionar...</option>`
        for (let censo of this.arrayCensos) {
            if (censo.Censista.id == this.usuarioLogueado.id) {
                if (censo.censado == false) {
                    sel += `<option value="${censo.cedula}">${censo.cedula} + ${censo.apellido}, ${censo.nombre}</option>`
                }
            }
        }
        sel += `</select>`
        document.querySelector("#divValidarPrecargas--pSlcMisAsignados").innerHTML = sel
    }
    //Se carga el select de usuarios censistas para reasignar a la persona elegida en el select de la funcion anterior
    preCargarSelReasignarCenso() {
        let sel = `<select id="divValidarPrecargas--slcReasignar">
        <option value="-1">Seleccione a quien reasignar</option>`
        for (let censista of this.arrayAdmins) {
            if (censista.id != this.usuarioLogueado.id) {
                sel += `<option value="${censista.id}">${censista.usuario} - ${censista.nombre}`
            }
        }
        sel += `</select>`
        document.querySelector("#divValidarPrecargas--pSlcReasignar").innerHTML = sel
    }

    //** FIN PRECARGA DE SELECTORES **//



    //** INICIO PRECARGA DE TABLAS ESTADÍSTICAS **//
    //Se cargan las estadisticas de los invitados
    cargarEstadisticasInvitado() {
        let tabla = `<table border="1"><thead><tr><th>Departamento</th><th>Estudian</th><th>NO Trabajan</th><th>Trabajan(Dependientes o independientes)</th><th>Porcentaje del total de censados</th></tr></thead>`
        let departamentos = this.arrayDepartamentos();
        for (let dpto of departamentos) {
            tabla += `<tr><td>${dpto}</td><td>${this.contarEstudian(dpto)}</td><td>${this.contarNoTrabajan(dpto)}</td><td>${this.contarTrabajan(dpto)}</td><td>${this.contarPorcentaje(dpto)}</td></tr>`
        }
        tabla += `</table>`
        document.querySelector("#divEstadisticas--pEstadisticas").innerHTML = tabla
    }
    cargarEstadisticasInvitadoInicio() {
        let tabla = `<table border="1"><thead><tr><th>Departamento</th><th>Estudian</th><th>NO Trabajan</th><th>Trabajan(Dependientes o independientes)</th><th>Porcentaje del total de censados</th></tr></thead>`
        let departamentos = this.arrayDepartamentos();
        for (let dpto of departamentos) {
            tabla += `<tr><td>${dpto}</td><td>${this.contarEstudian(dpto)}</td><td>${this.contarNoTrabajan(dpto)}</td><td>${this.contarTrabajan(dpto)}</td><td>${this.contarPorcentaje(dpto)}</td></tr>`
        }
        tabla += `</table>`
        document.querySelector("#divVerificarInvitado--pEstadisticas").innerHTML = tabla
    }
    //Se cargan las estadisticas de los censistas
    cargarEstadisticasAdmin() {

        //total de personas censadas hasta el momento
        let total = `<strong>El total de personas censadas hasta el momento es:</strong> ${this.contarTotalPersonasCensadas()}`
        document.querySelector("#divVerEstadisticas--pEstadisticas1").innerHTML = total;

        //total de personas censadas por depto (tabla)
        let tabla = `<table border="1"><thead><tr><th>Departamento</th><th>Censados</th></tr></thead>`
        let departamentos = this.arrayDepartamentos();
        for (let dpto of departamentos) {
            tabla += `<tr><td>${dpto}</td><td>${this.contarCensadosXDpto(dpto)}</td></tr>`
        }
        tabla += `</table>`
        document.querySelector("#divVerEstadisticas--pEstadisticas2").innerHTML = tabla

        //porcentaje que faltan validar respecto al total
        let porcentajePorValidar = this.calcularPorcentajePorValidar()
        document.querySelector("#divVerEstadisticas--pEstadisticas3").innerHTML = porcentajePorValidar

        //mayores vs menores por dpto (combo precargable)
        let sel = `<label>Franja etaria 18 años: </label><select id="divVerEstadisticas--slcDepartamentos"><option value=-1>Seleccione un departamento...</option>`
        for (let dpto of departamentos) {
            sel += `<option value="${dpto}">${dpto}</option>`
        }
        sel += `</select><br><br>`
        document.querySelector("#divVerEstadisticas--pEstadisticas4").innerHTML = sel
    }
    //** FIN PRECARGA DE TABLAS ESTADÍSTICAS **//



    //** INICIO CALCULOS ESTADÍSTICAS **//
    //estadísticas Invitado
    //Cuenta los que estudian
    contarEstudian(pDpto) {
        let estudian = 0
        for (let censo of this.arrayCensos) {
            if (censo.departamento == pDpto && censo.censado == true) {
                if (censo.ocupacion == "estudiante") {
                    estudian++
                }
            }
        }
        return estudian
    }
    //Cuenta los que no trabajan
    contarNoTrabajan(pDpto) {
        let noTrabajan = 0
        for (let censo of this.arrayCensos) {
            if (censo.departamento == pDpto && censo.censado == true) {
                if (censo.ocupacion == "No trabaja") {
                    noTrabajan++
                }
            }
        }
        return noTrabajan
    }
    //Cuenta los que trabajan
    contarTrabajan(pDpto) {
        let trabajan = 0
        for (let censo of this.arrayCensos) {
            if (censo.departamento == pDpto && censo.censado == true) {
                if (censo.ocupacion == "independiente" || censo.ocupacion == "dependiente") {
                    trabajan++
                }
            }
        }
        return trabajan
    }
    //Cuenta el porcentaje de personas por departamento
    contarPorcentaje(pDpto) {
        let total = this.arrayCensos.length
        let contador = 0
        for (let censo of this.arrayCensos) {
            if (censo.departamento == pDpto) {
                contador++
            }
        }
        let porcentajeNum = ((contador * 100) / total)
        let porcentaje = `${porcentajeNum.toFixed(1)}%`
        return porcentaje
    }
    //Fin estadísticas Invitado

    //Estadísticas Censista
    //Cuenta los que estan censados
    contarTotalPersonasCensadas() {
        let cantidad = 0
        for (let censo of this.arrayCensos) {
            if (censo.censado == true) {
                cantidad++
            }
        }
        return cantidad
    }
    //Cuenta los que estan censados por dpto
    contarCensadosXDpto(pDpto) {
        let contador = 0
        for (let censo of this.arrayCensos) {
            if (censo.departamento == pDpto && censo.censado == true) {
                contador++
            }
        }
        return contador
    }
    //Calcula el porcentaje de personas por validar
    calcularPorcentajePorValidar() {
        let total = this.arrayCensos.length
        let sinValidar = 0
        for (let censo of this.arrayCensos) {
            if (censo.censado == false) {
                sinValidar++
            }
        }
        let porcentajeNum = ((sinValidar * 100) / total)
        let porcentaje = `<strong>El porcentaje de personas pendientes de validar es:</strong> ${porcentajeNum.toFixed(1)}% respecto a un total de ${total} censos`
        return porcentaje
    }
    //Calcula el porcentaje de la franja etaria
    porcentajeFranjaEtaria() {
        let array = this.arrayCensos
        let dpto = document.querySelector("#divVerEstadisticas--slcDepartamentos").value
        let cantidadMay = 0
        let cantidadMen = 0
        let mensaje = ""
        let total = 0
        let i = 0
        if (dpto != -1) {
            while (i < array.length) {
                let censo = this.arrayCensos[i]
                if (censo.censado == true && censo.departamento == dpto) {
                    if (censo.edad >= 18) {
                        cantidadMay++
                    } else {
                        cantidadMen++
                    }
                    total++
                }
                i++
            }
            let porcentajeMay = ((cantidadMay * 100) / total)
            let porcentajeMen = ((cantidadMen * 100) / total)
            if (!isNaN(porcentajeMay) && !isNaN(porcentajeMen)) {
                mensaje = `<strong>Departamento:</strong> ${dpto} <br><strong>Porcentaje Mayores:</strong> ${porcentajeMay.toFixed(2)}% <br><strong>Porcentaje Menores:</strong> ${porcentajeMen.toFixed(2)}%`
            } else {
                mensaje = "No hay censados suficientes"
            }
        } else {
            mensaje = "Seleccione un departamento"
        }
        document.querySelector("#divVerEstadisticas--pFranjaEtaria").innerHTML = mensaje
    }
    //Fin estadísticas Censista
    //** FIN PRECARGA CALCULOS ESTADÍSTICAS **//



    //**PRECARGA USUARIOS CENSISTAS**//
    //Precarga los censistas pusheandolos si son validos
    preCargarCensistas() {
        let censista1 = this.crearCensista("gasvar", "Gaston", "Admin1")
        if (censista1 !== null) {
            this.arrayAdmins.push(censista1)
        } else {
            console.log("ERROR AL CARGAR CENSISTA 1")
        }

        let censista2 = this.crearCensista("rome115", "Matias", "Admin2")
        if (censista2 !== null) {
            this.arrayAdmins.push(censista2)
        } else {
            console.log("ERROR AL CARGAR CENSISTA 2")
        }

        let censista3 = this.crearCensista("cati", "Ferre", "Admin3")
        if (censista3 !== null) {
            this.arrayAdmins.push(censista3)
        } else {
            console.log("ERROR AL CARGAR CENSISTA 3")
        }
    }
    //**FIN PRECARGA USUARIOS CENSISTAS**//



    //**INICIO FUNCION CREADORA DE CENSISTAS**//
    //Crea un objeto censista 
    crearCensista(pUsu, pNom, pContra) {
        let censistaOk = null;
        pUsu = pUsu.toLowerCase()
        if (this.validarMayus(pNom) && this.validarHaySoloLetras(pNom) && !this.noNros(pNom) && this.validarContra(pContra) && this.validarHayAlgo(pNom) && this.validarHayAlgo(pUsu) && this.validarHayAlgo(pContra) && !this.buscarUsuario(pUsu)) {
            censistaOk = new PerfilCensista();
            censistaOk.nombre = pNom
            censistaOk.usuario = pUsu
            censistaOk.contraseña = pContra
        } else {
            console.log(`Error al cargar a USUARIO: ${pUsu} NOMBRE: ${pNom} `)
        }
        return censistaOk
    }
    //** FIN FUNCION CREADORA DE CENSISTAS **//



    //** INICIO PRECARGA CENSOS **//
    //Precarga todos los censos validados
    preCargarCensosValidados() {
        this.crearCensoValidadoPrecarga("1.206.609-6", "Jacinto", "Caronti", "43", "Artigas", "dependiente", 0)
        this.crearCensoValidadoPrecarga("1.641.846-1", "Jorge", "Catriel", "54", "Canelones", "independiente", 1)
        this.crearCensoValidadoPrecarga("3.865.604-7", "Gerardo", "Maracuya", "34", "Montevideo", "dependiente", 2)
        this.crearCensoValidadoPrecarga("6.138.260-9", "Martina", "Pomodoro", "23", "Maldonado", "estudiante", 0)
        this.crearCensoValidadoPrecarga("7.141.280-2", "Igor", "Pensilvania", "76", "Durazno", "No trabaja", 1)
        this.crearCensoValidadoPrecarga("1.258.342-6", "Ernesto", "Picolor", "48", "Cerro Largo", "No trabaja", 2)
        this.crearCensoValidadoPrecarga("4.604.265-0", "Anacleta", "Caruzzo", "36", "Artigas", "No trabaja", 0)
        this.crearCensoValidadoPrecarga("7.065.527-9", "Josefina", "Velez", "12", "Montevideo", "No trabaja", 1)
        this.crearCensoValidadoPrecarga("9.946.094-4", "Pedro", "Candolier", "52", "Artigas", "estudiante", 2)
        this.crearCensoValidadoPrecarga("6.654.635-1", "Marcelo", "Corneta", "21", "Salto", "estudiante", 0)
        this.crearCensoValidadoPrecarga("9.487.988-3", "Cameron", "Ramirez", "18", "Paysandú", "estudiante", 1)
        this.crearCensoValidadoPrecarga("4.048.119-9", "Abdul", "Ahbal", "84", "Soriano", "estudiante", 2)
        this.crearCensoValidadoPrecarga("7.924.000-1", "Jaimito", "Cartero", "25", "Treinta y Tres", "dependiente", 0)
        this.crearCensoValidadoPrecarga("9.536.029-9", "Ramon", "Valdez", "33", "Montevideo", "dependiente", 1)
        this.crearCensoValidadoPrecarga("8.555.962-4", "Pablo", "Esquivel", "48", "Canelones", "dependiente", 2)

        this.crearCensoValidadoPrecarga("6.942.280-5", "Abrojo", "Abrojin", "12", "Colonia", "No trabaja", 0)
        this.crearCensoValidadoPrecarga("0.366.530-4", "Lucas", "Vinoles", "14", "Durazno", "independiente", 1)
        this.crearCensoValidadoPrecarga("8.905.276-1", "Manuel", "Romero", "17", "Colonia", "estudiante", 2)
        this.crearCensoValidadoPrecarga("9.714.373-2", "Franco", "Vinoles", "129", "Flores", "estudiante", 0)
        this.crearCensoValidadoPrecarga("0.193.596-7", "Lugares", "Encontrados", "49", "Florida", "estudiante", 1)
        this.crearCensoValidadoPrecarga("5.173.730-3", "Armando", "Paredes", "68", "Montevideo", "estudiante", 2)
        this.crearCensoValidadoPrecarga("0.913.113-9", "Bondad", "Maldades", "57", "Río Negro", "independiente", 0)
        this.crearCensoValidadoPrecarga("2.457.646-9", "Mateo", "Profe", "10", "Rocha", "independiente", 1)
        this.crearCensoValidadoPrecarga("6.683.430-6", "Gonzalo", "Profe", "10", "Salto", "independiente", 2)
        this.crearCensoValidadoPrecarga("5.854.802-4", "Cuidado", "Esquivel", "98", "Soriano", "independiente", 0)
        this.crearCensoValidadoPrecarga("9.075.242-9", "Annie", "Morales", "64", "Tacuarembó", "dependiente", 1)
        this.crearCensoValidadoPrecarga("9.529.170-1", "Teemo", "Pequeno", "10", "Rivera", "No trabaja", 2)
        this.crearCensoValidadoPrecarga("1.800.096-5", "Gran", "Alma", "9", "Montevideo", "dependiente", 0)
        this.crearCensoValidadoPrecarga("7.388.284-7", "Issei", "Esteban", "12", "Montevideo", "dependiente", 1)
        this.crearCensoValidadoPrecarga("3.816.676-1", "Goku", "Morales", "37", "Rocha", "dependiente", 2)
    }

    //Precarga los censos sin validar
    preCargarCensosNoValidados() {
        this.crearCensoNoValidadoPrecarga("8.742.346-1", "Pilaro", "Pillares", "12", "Montevideo", "dependiente", 0)
        this.crearCensoNoValidadoPrecarga("5.137.474-7", "Lady", "Gaga", "8", "Canelones", "independiente", 1)
        this.crearCensoNoValidadoPrecarga("4.251.506-3", "Mariano", "Gago", "13", "Maldonado", "estudiante", 2)
        this.crearCensoNoValidadoPrecarga("0.789.249-0", "Gollum", "Smeargle", "15", "Cerro Largo", "No trabaja", 0)
        this.crearCensoNoValidadoPrecarga("3.678.547-8", "Harry", "Potter", "67", "Durazno", "dependiente", 1)
        this.crearCensoNoValidadoPrecarga("7.237.827-3", "Hernesto", "Perez", "54", "Artigas", "No trabaja", 2)
        this.crearCensoNoValidadoPrecarga("6.773.359-9", "Juanchi", "Pereira", "32", "Tacuarembó", "dependiente", 0)
        this.crearCensoNoValidadoPrecarga("3.724.749-7", "Nola", "Bura", "69", "San José", "No trabaja", 1)
        this.crearCensoNoValidadoPrecarga("2.508.594-2", "Dalai", "Lama", "82", "Canelones", "dependiente", 2)
        this.crearCensoNoValidadoPrecarga("6.402.719-7", "Genio", "Mago", "98", "Montevideo", "independiente", 0)
        this.crearCensoNoValidadoPrecarga("4.949.004-0", "Esteban", "Quito", "23", "Montevideo", "No trabaja", 1)
        this.crearCensoNoValidadoPrecarga("8.568.182-7", "Juan", "Nauj", "42", "Cerro Largo", "estudiante", 2)
        this.crearCensoNoValidadoPrecarga("1.294.499-3", "Yoni", "Estudio", "23", "Montevideo", "estudiante", 0)
        this.crearCensoNoValidadoPrecarga("5.414.601-8", "Lavira", "Lacasa", "55", "Montevideo", "estudiante", 1)
        this.crearCensoNoValidadoPrecarga("7.309.209-8", "Pepe", "DelCampo", "67", "Artigas", "dependiente", 2)

        this.crearCensoNoValidadoPrecarga("2.026.545-6", "Freezer", "Manuel", "23", "Cerro Largo", "estudiante", 0)
        this.crearCensoNoValidadoPrecarga("4.386.890-4", "Vegeta", "Estable", "24", "San José", "No trabaja", 1)
        this.crearCensoNoValidadoPrecarga("3.586.374-8", "Broly", "Padrelines", "25", "Maldonado", "estudiante", 2)
        this.crearCensoNoValidadoPrecarga("9.280.532-3", "Juanchon", "Grandon", "27", "Montevideo", "dependiente", 0)
        this.crearCensoNoValidadoPrecarga("1.486.129-8", "Steven", "Morales", "45", "Artigas", "dependiente", 1)
        this.crearCensoNoValidadoPrecarga("2.650.756-1", "Universe", "Lies", "33", "Tacuarembó", "estudiante", 2)
        this.crearCensoNoValidadoPrecarga("6.056.544-8", "Steve", "Maincrafghossen", "32", "Artigas", "estudiante", 0)
        this.crearCensoNoValidadoPrecarga("3.709.477-5", "Alex", "Maincrafghossen", "31", "Montevideo", "No trabaja", 1)
        this.crearCensoNoValidadoPrecarga("0.189.666-6", "Carl", "Johnson", "29", "Canelones", "estudiante", 2)
        this.crearCensoNoValidadoPrecarga("6.459.806-7", "Marques", "Pomodoro", "25", "Rocha", "dependiente", 0)
        this.crearCensoNoValidadoPrecarga("6.888.944-4", "Brian", "Hernandez", "3", "Salto", "dependiente", 1)
        this.crearCensoNoValidadoPrecarga("7.404.114-7", "Manute", "Mordekaiser", "4", "Lavalleja", "independiente", 2)
        this.crearCensoNoValidadoPrecarga("2.779.921-0", "Irelia", "Ionia", "5", "Colonia", "independiente", 0)
        this.crearCensoNoValidadoPrecarga("9.820.074-7", "Qiyana", "Sanchez", "9", "Paysandú", "independiente", 1)
        this.crearCensoNoValidadoPrecarga("0.507.593-7", "Viego", "Sombras", "12", "Florida", "dependiente", 2)

    }

    //Valida que el censo de la precarga recibido tenga datos correctos, lo "censa" y lo pushea si es asi
    crearCensoValidadoPrecarga(pCedula, pNom, pApellido, pEdad, pDepartamento, pOcupacion, pCensistaID) {
        let invitadoOk = null
        if (pCedula.length == 9) {
            pCedula = "0." + pCedula
        }
        if (this.validarHayAlgo(pCedula) && this.validarFormatoCedula(pCedula) && this.validarCedula(pCedula) && this.validarHayAlgo(pNom) && this.validarHaySoloLetras(pNom) && this.validarHayAlgo(pApellido) && this.validarHaySoloLetras(pApellido) && this.validarHayAlgo(pEdad) && this.validarEdad(pEdad) && this.validarHayAlgo(pDepartamento) && this.validarHayAlgo(pOcupacion) && this.validarIDcensista(pCensistaID) && !this.buscarCedula(pCedula)) {
            invitadoOk = new Censo()
            invitadoOk.cedula = pCedula
            invitadoOk.nombre = pNom
            invitadoOk.apellido = pApellido
            invitadoOk.edad = pEdad
            invitadoOk.departamento = pDepartamento
            invitadoOk.ocupacion = pOcupacion
            invitadoOk.censado = true
            invitadoOk.Censista = this.arrayAdmins[pCensistaID]
            this.arrayCensos.push(invitadoOk)
        } else {
            console.log(`Error al cargar ${pNom}`)
        }
    }
    //Valida que el censo de la precarga recibido tenga datos correctos, no lo "censa" y lo pushea si es asi
    crearCensoNoValidadoPrecarga(pCedula, pNom, pApellido, pEdad, pDepartamento, pOcupacion, pCensistaID) {
        let invitadoOk = null
        if (pCedula.length == 9) {
            pCedula = "0." + pCedula
        }
        if (this.validarHayAlgo(pCedula) && this.validarFormatoCedula(pCedula) && this.validarCedula(pCedula) && this.validarHayAlgo(pNom) && this.validarHaySoloLetras(pNom) && this.validarHayAlgo(pApellido) && this.validarHaySoloLetras(pApellido) && this.validarHayAlgo(pEdad) && this.validarEdad(pEdad) && this.validarHayAlgo(pDepartamento) && this.validarHayAlgo(pOcupacion) && this.validarIDcensista(pCensistaID) && !this.buscarCedula(pCedula)) {
            invitadoOk = new Censo()
            invitadoOk.cedula = pCedula
            invitadoOk.nombre = pNom
            invitadoOk.apellido = pApellido
            invitadoOk.edad = pEdad
            invitadoOk.departamento = pDepartamento
            invitadoOk.ocupacion = pOcupacion
            invitadoOk.censado = false
            invitadoOk.Censista = this.arrayAdmins[pCensistaID]
            this.arrayCensos.push(invitadoOk)
        }
    }
    //** FIN PRECARGA CENSOS **//



    //**INICIO FUNCION CREADORA DE CENSOS INVITADO**//
    //Crea el censo recibiendo parametros ingresados por un invitado
    crearCenso(pCedula, pNom, pApellido, pEdad, pDepartamento, pOcupacion) {
        let invitadoOk = null
        if (pCedula.length == 9) {
            pCedula = "0." + pCedula
        }
        if (this.validarHayAlgo(pCedula) && this.validarFormatoCedula(pCedula) && this.validarCedula(pCedula) && this.validarHayAlgo(pNom) && this.validarHaySoloLetras(pNom) && this.validarHayAlgo(pApellido) && this.validarHaySoloLetras(pApellido) && this.validarHayAlgo(pEdad) && this.validarEdad(pEdad) && this.validarHayAlgo(pDepartamento) && this.validarHayAlgo(pOcupacion)) {
            invitadoOk = new Censo()
            invitadoOk.cedula = pCedula
            invitadoOk.nombre = pNom
            invitadoOk.apellido = pApellido
            invitadoOk.edad = pEdad
            invitadoOk.departamento = pDepartamento
            invitadoOk.ocupacion = pOcupacion
            invitadoOk.censado = false
            invitadoOk.Censista = this.asignarCensista()
            this.arrayCensos.push(invitadoOk)
        }
    }
    //Se le asigna un censista random al censo ingresado en la funcion anterior
    asignarCensista() {
        let max = this.arrayAdmins.length;
        let numAleatorio = Math.floor(Math.random() * max);
        let cencistaAleatorio = this.arrayAdmins[numAleatorio]
        return cencistaAleatorio;
    }
    //**FIN FUNCION CREADORA DE CENSOS INVITADO**//


    //** INICIO EDITAR CENSO INVITADO **//
    //Si el usuario precargo datos se le muestran y al clickear guardar se editan con los parametros recibidos
    editarCenso(pCedula, pNombre, pApellido, pEdad, pDepartamento, pOcupacion) {
        if (pCedula.length == 9) {
            pCedula = "0." + pCedula
        }
        let encontrado = false
        let i = 0
        while (i < this.arrayCensos.length && !encontrado) {
            let censo = this.arrayCensos[i]
            if (censo.cedula == pCedula) {
                censo.cedula = pCedula
                censo.nombre = pNombre
                censo.apellido = pApellido
                censo.edad = pEdad
                censo.departamento = pDepartamento
                censo.ocupacion = pOcupacion
                encontrado = true
            }
            i++
        }
    }
    //Retorna el nombre del censista asignado a un censo (que es buscado por su cedula)
    obtenerNombreCensistaAsignado(pCedula) {
        let censistaEncontrado = null
        let i = 0
        while (i < this.arrayCensos.length && censistaEncontrado == null) {
            let censoX = this.arrayCensos[i]
            if (pCedula == censoX.cedula) {
                censistaEncontrado = censoX.Censista.nombre
            }
            i++
        }
        return censistaEncontrado
    }
    //** FIN EDITAR CENSO INVITADO **//



    //**INICIO BORRAR CENSO INVITADO**//
    //Se borra el censo del array
    borrarCensoInvitado(pCedula) {
        let i = 0
        let encontrado = false
        let censoBorrado = null
        while (i < this.arrayCensos.length && !encontrado) {
            let censo = this.arrayCensos[i]
            if (pCedula == censo.cedula) {
                encontrado = true
                censoBorrado = this.arrayCensos.splice(i, 1)
            }
            i++
        }
        return censoBorrado
    }
    //**FIN BORRAR CENSO INVITADO**//



    //**INICIO FUNCION CREADORA DE CENSOS ADMIN**//
    //Crea el censo que se valida automaticamente por el censista
    crearCensoAdmin(pCedula, pNom, pApellido, pEdad, pDepartamento, pOcupacion) {
        let invitadoOk = null
        if (pCedula.length == 9) {
            pCedula = "0." + pCedula
        }
        if (this.validarHayAlgo(pCedula) && this.validarFormatoCedula(pCedula) && this.validarCedula(pCedula)) {
            invitadoOk = new Censo()
            invitadoOk.cedula = pCedula
            invitadoOk.nombre = pNom
            invitadoOk.apellido = pApellido
            invitadoOk.edad = pEdad
            invitadoOk.departamento = pDepartamento
            invitadoOk.ocupacion = pOcupacion
            invitadoOk.censado = true
            invitadoOk.Censista = this.usuarioLogueado
            this.arrayCensos.push(invitadoOk)
        }
    }
    //Actualiza datos de un censo buscado por cedula
    actualizarCensoAdmin(pCedula, pNombre, pApellido, pEdad, pDepartamento, pOcupacion) {
        if (pCedula.length == 9) {
            pCedula = "0." + pCedula
        }
        let encontrado = false
        let i = 0
        while (i < this.arrayCensos.length && !encontrado) {
            let censo = this.arrayCensos[i]
            if (censo.cedula == pCedula) {
                censo.cedula = pCedula
                censo.nombre = pNombre
                censo.apellido = pApellido
                censo.edad = pEdad
                censo.departamento = pDepartamento
                censo.ocupacion = pOcupacion
                censo.censado = true
                censo.Censista = this.usuarioLogueado
                encontrado = true
            }
            i++
        }
    }
    //**FIN FUNCION CREADORA DE CENSOS ADMIN**//



    //** INICIO VISUALIZAR CENSOS ASIGNADOS **//
    //Esta funcion carga los datos del censo seleccionado
    previsualizarCensoValidar(pCensoID) {
        let mensaje = "";
        let encontrado = false
        let i = 0
        while (i < this.arrayCensos.length && !encontrado) {
            let censo = this.arrayCensos[i]
            if (censo.cedula == pCensoID) {
                console.log(censo)
                mensaje = `<b>NOMBRE:</b> ${censo.nombre}<br><b>APELLIDO:</b> ${censo.apellido}<br><b>EDAD:</b> ${censo.edad}<br><b>CEDULA:</b> ${censo.cedula}<br><b>DEPARTAMENTO:</b> ${censo.departamento}<br><b>OCUPACIÓN:</b> ${censo.ocupacion}`
            }
            i++
        }
        return mensaje;
    }
    //** FIN VISUALIZAR CENSOS ASIGNADOS **//

    //** INICIO REASIGNAR CENSO **//
    //Reasigna a otro censista el censo seleccionado
    reasignarCensoSeleccionado(pCedula, pId) {
        let censoEncontrado = this.buscarDatos(pCedula)
        let adminEncontrado = null
        console.log(censoEncontrado)
        let encontrado = false
        let i = 0
        while (i < this.arrayAdmins.length && !encontrado) {
            let admin = this.arrayAdmins[i]
            if (admin.id == pId) {
                adminEncontrado = admin
                encontrado = true
            }
            i++
        }
        console.log(adminEncontrado)

        censoEncontrado.Censista = adminEncontrado

        console.log(censoEncontrado)
    }
    //** FIN REASIGNAR CENSO **//




}