using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClasesObligatorioP2GVDS.Models
{
    public class Mensaje
    {
        private static int _ultimoId = 0;
        public int Id { get; set; }
        public int IdEnvia { get; set; }
        public int IdRecibe { get; set; }
        public string Contenido { get; set; }
        public DateTime Hora { get; set; }

        public Mensaje()
        {
            Id = _ultimoId++;
            Hora = DateTime.Now;
        }

        public Mensaje(int idEnvia, int idRecibe, string contenido)
        {
            Id = _ultimoId++;
            IdEnvia = idEnvia;
            IdRecibe = idRecibe;
            Contenido = contenido;
            Hora = DateTime.Now;
        }

        public void EsValido()
        {
            if (String.IsNullOrEmpty(Contenido))
            {
                throw new Exception("No puedes enviar mensajes vacios");
            }
            if(IdEnvia == null || IdRecibe == null)
            {
                throw new Exception("Error al enviar mensaje");
            }
        }

        public static void ReducirId()
        {
            _ultimoId--;
        }
    }
}
