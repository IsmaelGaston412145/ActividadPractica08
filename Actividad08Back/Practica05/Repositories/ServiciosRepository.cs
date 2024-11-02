using Practica05.Models;

namespace Practica05.Repositories
{
    public class ServiciosRepository : IServiciosRepository
    {
        private turnos_db_2_7DbContext _DbContext;

        public ServiciosRepository(turnos_db_2_7DbContext context)
        {
            _DbContext = context;
        }

        public bool Save(Servicio servicio)
        {
            bool aux = false;
            if (servicio != null)
            {
                _DbContext.TServicios.Add(servicio);
                if (_DbContext.SaveChanges() == 1) { aux = true; }
            }
            return aux;
        }

        public List<Servicio> GetAll()
        {
            return _DbContext.TServicios.ToList();
        }
    }
}
