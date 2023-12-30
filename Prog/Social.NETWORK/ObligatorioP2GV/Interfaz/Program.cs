using ClasesObligatorioP2GVDS;

namespace Interfaz
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Obtenemos la instancia mediante singleton
            Sistema miSistema = Sistema.GetInstancia();
            //Llamamos la precarga, no la realizamos en el constructor para asi poder recibir mensajes de error mediante un string
            string precargaCorrecta = miSistema.LlamarPrecarga();
            //Menu de carga
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("----------------BIENVENIDO A SOCIAL.NETWORK---------------");
            Console.WriteLine("----------------------------------------------------------");

            Console.ResetColor();
            Console.WriteLine("          .\r\n        ('\r\n        '|\r\n        |'\r\n       [::]\r\n       [::]   _......_\r\n       [::].-'      _.-`.\r\n       [:.'    .-. '-._.-`.\r\n       [/ /\\   |  \\        `-..\r\n       / / |   `-.'      .-.   `-.\r\n      /  `-'            (   `.    `.\r\n     |           /\\      `-._/      \\\r\n     '    .'\\   /  `.           _.-'|\r\n    /    /  /   \\_.-'        _.':;:/\r\n  .'     \\_/             _.-':;_.-'\r\n /   .-.             _.-' \\;.-'\r\n/   (   \\       _..-'     |\r\n\\    `._/  _..-'    .--.  |\r\n `-.....-'/  _ _  .'    '.|\r\n          | |_|_| |      | \\  (o)\r\n     (o)  | |_|_| |      | | (\\'/)\r\n    (\\'/)/  ''''' |     o|  \\;:;\r\n     :;  |        |      |  |/)\r\n LGB  ;: `-.._    /__..--'\\.' ;:\r\n          :;  `--' :;   :;");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("(° ʖ °) Espere mientras cargamos su experiencia...");
            Console.ResetColor();
            Thread.Sleep(2000);
            int ingreso = -1;
            //Interfaz de usuario para seleccionar que hacer
            while (ingreso != 0)
            {
                //Muestra el menu de opciones
                MostrarMenu();

                try
                {
                    ingreso = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ingreso = -1;
                }

                switch (ingreso)
                {
                    case 1:
                        //Funcionalidad 1: Alta de miembro
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("----------------------------------------------------------");
                        Console.WriteLine("-----------------------ALTA USUARIO-----------------------");
                        Console.WriteLine("----------------------------------------------------------");
                        Console.ResetColor();
                        try
                        {
                            CreacionDeUsuario();
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("----------------------------------------------------------");
                            Console.WriteLine("----------------Usuario creado exitosamente---------------");
                            Console.WriteLine("----------------------------------------------------------");
                            Console.ResetColor();

                        }
                        catch (Exception e)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("Ha habido un error al tomar los datos, no se ha creado el usuario, intentelo de nuevo siguiendo las indicaciones.");
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("Mensaje de error: ");
                            Console.WriteLine(e.Message);
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                            Console.ResetColor();
                        }
                        break;
                    case 2:
                        //Funcionalidad 2: Dado un email de miembro listar todas las publicaciones que ha realizado diferenciando posts de comentarios
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("----------------------------------------------------------");
                        Console.WriteLine("---------PUBLICACIONES REALIZADAS POR X USUARIO-----------");
                        Console.WriteLine("----------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Ingrese el email del miembro al que desea buscar publicaciones: ");

                        try
                        {
                            string email2 = Console.ReadLine();
                            Miembro miembro2 = miSistema.GetMiembroXEmail(email2);
                            List<Publicacion> listaPublicacionesXUsuario = miSistema.BuscarPublicacionesXUsuario(miembro2);
                            MostrarListaPublicacion(listaPublicacionesXUsuario);
                        }
                        catch (Exception e)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("-------------------------------------------------------------------------------");
                            Console.WriteLine("No se han encontrado publicaciones asociadas a este mail, compruebe el ingreso.");
                            Console.WriteLine("-------------------------------------------------------------------------------");
                            Console.WriteLine("Mensaje de error: ");
                            Console.WriteLine(e.Message);
                            Console.WriteLine("-------------------------------------------------------------------------------");
                            Console.ResetColor();
                        }


                        break;
                    case 3:
                        //Funcionalidad 3: Dado un email de un miembro listar todos los posts en los que haya comentado
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("--------------------------------------------------------------------------------");
                        Console.WriteLine("----------------------LISTAR POST COMENTADOS SEGÚN EMAIL------------------------");
                        Console.WriteLine("--------------------------------------------------------------------------------");
                        Console.WriteLine();
                        Console.ResetColor();
                        Console.WriteLine("Ingrese un email: ");
                        try
                        {
                            string email3 = Console.ReadLine();
                            Miembro miembro3 = miSistema.GetMiembroXEmail(email3);
                            List<Publicacion> lista = miSistema.ListarPostComentadosXMiembro(miembro3);
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"------------------------Estos son los post comentados por {email3}-----------------------------");
                            Console.WriteLine("------------------------------------------------------------------------------------------------");
                            Console.ResetColor();
                            MostrarListaPublicacion(lista);
                        }
                        catch (Exception e)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("----------------------------------------------------------------------------------------------------");
                            Console.WriteLine("----------------------No se encontraron post comentados por el usuario ingresado.-------------------");
                            Console.WriteLine("----------------------------------------------------------------------------------------------------");
                            Console.WriteLine("Mensaje de error: ");
                            Console.WriteLine(e.Message);
                            Console.WriteLine("----------------------------------------------------------------------------------------------------");
                            Console.ResetColor();
                        }
                        break;
                    case 4:
                        //Funcionalidad 4: Dadas dos fechas listar los posts realizados entre esas fechas inclusive
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("----------------------------------------------------------");
                        Console.WriteLine("---------------BUSCAR POST ENTRE DOS FECHAS---------------");
                        Console.WriteLine("----------------------------------------------------------");
                        Console.ResetColor();
                        try
                        {
                            BuscarPostEntreDosFechas();
                        }
                        catch (Exception e)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("---------------------------------------------------------------");
                            Console.WriteLine("No se han encontrado publicaciones entre las fechas ingresadas.");
                            Console.WriteLine("---------------------------------------------------------------");
                            Console.WriteLine("Mensaje de error:");
                            Console.WriteLine(e.Message);
                            Console.WriteLine("---------------------------------------------------------------");
                            Console.ResetColor();
                        }
                        break;
                    case 5:
                        //Funcionalidad 5: Obtener los miembros que hayan realizado mas publicaciones de cualquier tipo
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine("---------------OBTENER EL/LOS MIEMBROS CON MAS PUBLICACIONES---------------");
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Thread.Sleep(1000);
                        Console.ResetColor();
                        try
                        {
                            MostrarMiembros(miSistema.MiembrosConMasPublicaciones());
                        }
                        catch (Exception e)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("---------------------------------------------------------------");
                            Console.WriteLine($"Mensaje de error:");
                            Console.WriteLine(e.Message);
                            Console.WriteLine("---------------------------------------------------------------");
                            Console.ResetColor();
                        }
                        break;
                    case 6:
                        //Opcion para comprobar la precarga (conveniencia para corregir)
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine("-----------------------------COMPROBAR PRECARGA----------------------------");
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine("Numeros minimos esperados: ");
                        Console.WriteLine("Invitaciones: 34");
                        Console.WriteLine("Publicaciones: 20");
                        Console.WriteLine("Usuarios: 12");
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine("Cantidad de invitaciones: " + miSistema.GetInvitaciones().Count);
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine("Cantidad de publicaciones (comentarios y post): " + miSistema.GetPublicaciones().Count);
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine("Cantidad de usuarios: " + miSistema.GetUsuarios().Count);
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine(miSistema.ComprobarPrecarga());
                        Console.WriteLine(precargaCorrecta);
                        break;
                    case 0:
                        //Sale del sistema
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        Console.WriteLine("----------------║█║▌║█║▌│║║▌█║║█║▌│║▌SALIR DEL SISTEMA║█║▌║█║▌│║║▌█║║█║▌│║▌---------------");
                        Console.WriteLine("----------------║█║▌║█║▌│║║▌█║║█║▌│║▌GRACIAS POR USAR║█║▌║█║▌│║║▌█║║█║▌│║▌----------------");
                        Console.WriteLine("║█║▌║█║▌│║║▌█║║█║▌│║▌Presione cualquier tecla para salir del sistema.║█║▌║█║▌│║║▌█║║█║▌│║▌");
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        Console.WriteLine("Ninguno de sus datos bancarios han sido recompilados mediante la ejecucion de esta aplicacion.");
                        Console.ResetColor();
                        break;
                    default:
                        Console.WriteLine("Ingreso no válido");
                        break;
                }



                Console.ReadKey();
            }

            Console.ReadKey();

            void CreacionDeUsuario()
            {
                //Pide los datos para el alta del usuario y los envia al metodo del sistema
                Console.WriteLine("Ingrese su email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Ingrese su contraseña (minimo una mayuscula y un numero): ");
                string contrasenia = Console.ReadLine();
                Console.WriteLine("Ingrese su nombre (inicia con mayuscula): ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese su apellido (inicia con mayuscula): ");
                string apellido = Console.ReadLine();
                Console.WriteLine("Ingrese su fecha de nacimiento en el formato DD/MM/AAAA");
                DateTime fechaNacimiento = DateTime.Parse(Console.ReadLine());
                Miembro m = new Miembro(nombre, apellido, fechaNacimiento, email, contrasenia);
                miSistema.AltaUsuario(m);
            }


            void BuscarPostEntreDosFechas()
            {
                //Pide las fechas y las envia al metodo de sistema para realizar la busqueda, llama al metodo de program para mostrar la lista
                Console.WriteLine("Ingrese la primer fecha en formato DD/MM/AAAA");
                DateTime fecha1 = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese la segunda fecha en formato DD/MM/AAAA");
                DateTime fecha2 = DateTime.Parse(Console.ReadLine());
                List<Post> listaPostEntre2Fechas = miSistema.ObtenerPostEntre2Fechas(fecha1, fecha2);
                //Transformamos la lista recibida a una lista de publicaciones para poder utilizar el metodo declarado en program, muestra los posts mediante polimorfismo
                List<Publicacion> listaPostTransformada = miSistema.TransformarListaPostAPublicacion(listaPostEntre2Fechas);
                MostrarListaPublicacion(listaPostTransformada);
            }


            void MostrarListaPublicacion(List<Publicacion> lista)
            {
                //Muestra las publicaciones de una lista de publicacion recibida por parametro diferenciandolas mediante polimorfismo
                foreach (Publicacion p in lista)
                {
                    Console.WriteLine(p);
                }
            }

            void MostrarMiembros(DTOMaximoCantidadPublicaciones DTO)
            {
                //Muestra los miembros de una lista de miembros recibida por parametro
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("-------------------------------------------------------------------------------------");
                Console.WriteLine("Se encontraron los siguientes miembros con el DTO:");
                Console.WriteLine("-------------------------------------------------------------------------------------");
                foreach (Miembro m in DTO.ListaMiembros)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------------------------------------------");
                    Console.WriteLine("MIEMBRO");
                    Console.WriteLine("-------------------------------------------------------------------------------------");
                    Console.ResetColor();
                    Console.WriteLine(m);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("-------------------------------------------------------------------------------------");

                    Console.ResetColor();
                }
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine();
                Console.WriteLine("-------------------------------------------------------------------------------------");
                Console.WriteLine($"Cantidad de publicaciones maxima: {DTO.Cantidad}");
                Console.WriteLine("-------------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.ResetColor();
            }


        }

        private static void MostrarMenu()
        {
            //Muestra las opciones del menu
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("║█║▌║█║▌│║║▌█║║█║▌│║▌ MENU PRINCIPAL ║█║▌║█║▌▌║█║▌║█║│║▌█║");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Seleccione: ");
            Console.WriteLine();
            Console.WriteLine("1-Dar de alta a un miembro");
            Console.WriteLine();
            Console.WriteLine("2-Listar publicaciones segun EMAIL");
            Console.WriteLine();
            Console.WriteLine("3-Listar post comentados segun EMAIL");
            Console.WriteLine();
            Console.WriteLine("4-Listar post publicados entre dos fechas");
            Console.WriteLine();
            Console.WriteLine("5-Obtener el/los miembros con mas publicaciones");
            Console.WriteLine();
            Console.WriteLine("6-Comprobar precarga.");
            Console.WriteLine();
            Console.WriteLine("0-Salir del sistema.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
        }

    }
}