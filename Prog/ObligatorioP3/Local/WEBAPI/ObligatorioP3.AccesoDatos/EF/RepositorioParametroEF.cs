using Libreria.LogicaNegocio.Entidades.ParametrosConfigurables;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.AccesoDatos.EF;

namespace Libreria.AccesoDatos.EF
{
    public class RepositorioParametroEF : IRepositorioParametro
    {
        private ObligatorioP3Context _db { get; set; }
        public RepositorioParametroEF(ObligatorioP3Context db)
        {
            _db = db;
        }

        /// <summary>
        /// Recupera el parámetro solicitado por nombre
        /// </summary>
        /// <param name="nombre">Nombre del parámetro</param>
        /// <returns>Entidad Parámetro</returns>
        public Parametro? GetParametro(string nombre)
        {
            try
            {
                Parametro? parametro = _db.Parametros
                .SingleOrDefault(parametro => parametro.Nombre.Equals(nombre));
                return parametro;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Parametro GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Add(Parametro obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Parametro obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Parametro obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parametro> GetAll()
        {
            throw new NotImplementedException();
        }
    }

    }
 