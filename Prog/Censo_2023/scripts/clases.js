//Clase del censista
class PerfilCensista{

    static idCensista=0
    
    constructor(){
    this.id=PerfilCensista.idCensista++ //El identificador 1 del censista (autogestionado por el objeto)
    this.usuario="" //El identificador 2 del censista
    this.nombre="" //Nombre que debe empezar en mayusucla y no contener numeros
    this.contraseña="" //Contraseña para el inicio de sesion
    }
}
//Clase del invitado
class Censo{

    constructor(){
        this.cedula=Number //Identificador de los invitados
        this.nombre="" //Dato del censo
        this.apellido="" //Dato del censo
        this.edad=Number //Dato del censo
        this.departamento="" //Dato del censo
        this.ocupacion="" //Dato del censo
        this.censado=Boolean //Dato del censo
        this.Censista=null //Dato del censo
    }
}

