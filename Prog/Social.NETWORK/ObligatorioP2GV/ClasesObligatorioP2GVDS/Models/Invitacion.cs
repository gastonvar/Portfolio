using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClasesObligatorioP2GVDS
{
    public class Invitacion : IValidacion
    {
        private static int _ultimoId { get; set; }
        public int Id { get; set; }
        public Miembro Solicitante { get; set; }
        public Miembro Solicitado { get; set; }
        public Estado Estado { get; set; }
        public DateTime FechaEnvio { get; set; }

        public Invitacion()
        {
            Id = _ultimoId++;
            FechaEnvio = DateTime.Now;
            Estado = Estado.PENDIENTE_APROBACION;
        }

        public Invitacion(Miembro solicitante, Miembro solicitado)
        {
            Id = _ultimoId++;
            Solicitante = solicitante;
            Solicitado = solicitado;
            Estado = Estado.PENDIENTE_APROBACION;
            FechaEnvio = DateTime.Now;
        }

        //Validaciones
        public void EsValido()
        {
            if (this == null)
            {
                throw new Exception($"Invitación ID {Id}: Invitación nula");
            }
            try
            {
                Solicitante.EsValido();
                Solicitado.EsValido();
            }
            catch (Exception e)
            {

                throw e;
            }
           
            if (Solicitante == null || Solicitado == null)
            {
                throw new Exception($"Solicitante o solicitado no validos");
            }
            if (Estado != Estado.PENDIENTE_APROBACION && Estado != Estado.APROBADA && Estado != Estado.RECHAZADA)
            {
                throw new Exception($"Estado invalido");
            }
            if (Solicitante.Equals(Solicitado))
            {
                throw new Exception($"Solicitante no puede ser igual a solicitado");
            }
        }
        //Es llamada en caso de error de validacion
        public static void ReducirId()
        {
            _ultimoId--;
        }

        public override string ToString()
        {
            return $"Invitacion: [Id: {Id}][Solicitante: {Solicitante.ToString()}][Estado: {Estado}][Fecha de envio: {FechaEnvio}]";
        }

        //Método que chequea que los miembros no se tengan en sus listas de amigos y que la invitacion este pendiente de aprobación.
        //De ser así se agregan a sus listas de amigos y se establece como aprobada.
        public void AceptarInvitacion()
        {
            EsValido();
            if (!Solicitado.GetListaAmigos().Contains(Solicitante)&&!Solicitante.GetListaAmigos().Contains(Solicitado)&&Estado==Estado.PENDIENTE_APROBACION) {
                Estado = Estado.APROBADA;
                Solicitado.AgregarAmigo(Solicitante);
                Solicitante.AgregarAmigo(Solicitado);
            }
            else
            {
                throw new Exception("Hubo un error inesperado");
            }

        }

        //Método para rechazar una invitación, cambia el estado a rechazada.
        public void RechazarInvitacion()
        {
            EsValido();
            Estado = Estado.RECHAZADA;
            
        }

        public override bool Equals(object? obj)
        {
            return obj is Invitacion invitacion &&
                   Solicitante.Equals(invitacion.Solicitante) &&
                   Solicitado.Equals(invitacion.Solicitado) || obj is Invitacion invitacionb && Solicitante.Equals(invitacionb.Solicitado) &&
                   Solicitado.Equals(invitacionb.Solicitante);
        }
    }
}
