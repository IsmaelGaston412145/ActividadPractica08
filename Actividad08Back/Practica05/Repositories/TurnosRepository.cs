using Microsoft.EntityFrameworkCore;
using Practica05.Models;

namespace Practica05.Repositories
{
    public class TurnosRepository : ITurnosRepository
    {
        private turnos_db_2_7DbContext _DbContext;

        public TurnosRepository(turnos_db_2_7DbContext context)
        {
            _DbContext = context;
        }

        public bool Delete(int id)
        {
            var turnoDel = _DbContext.TTurnos.Find(id);
            if (turnoDel != null)
            {
                foreach (var t in _DbContext.TDetallesTurno.Where(x => x.IdTurno == id).ToList())
                {
                    _DbContext.TDetallesTurno.Remove(t);
                }

                _DbContext.TTurnos.Remove(turnoDel);
                return _DbContext.SaveChanges()>0;
            }
            else
            {
                return false;
            }
        }

        public List<Turno> GetAll()
        {
            return _DbContext.TTurnos.ToList();
        }

        public List<Turno> GetAllByFilters(string cliente, string? fecha = null, string? hora = null)
        {
            List<Turno> lst = new List<Turno>();

            if(!string.IsNullOrWhiteSpace(cliente))
            {
                if (!string.IsNullOrWhiteSpace(fecha) && !string.IsNullOrWhiteSpace(hora))
                {
                    lst = _DbContext.TTurnos.Where(x => x.Cliente == cliente && x.Fecha == fecha && x.Hora == hora).ToList();
                }
                if (!string.IsNullOrWhiteSpace(fecha) && string.IsNullOrWhiteSpace(hora))
                {
                    lst = _DbContext.TTurnos.Where(x => x.Cliente == cliente && x.Fecha == fecha).ToList();
                }
                if (string.IsNullOrWhiteSpace(fecha) && !string.IsNullOrWhiteSpace(hora))
                {
                    lst = _DbContext.TTurnos.Where(x => x.Cliente == cliente && x.Hora == hora).ToList();
                }
                if (string.IsNullOrWhiteSpace(fecha) && string.IsNullOrWhiteSpace(hora))
                {
                    lst = _DbContext.TTurnos.Where(x => x.Cliente == cliente).ToList();
                }
            }
            return lst;
        }

        public bool Save(Turno turno)
        {
            if (turno != null)
            {
                _DbContext.TTurnos.Add(turno);
                return _DbContext.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Turno turno)
        {
            var turnoUpdate = _DbContext.TTurnos.Find(turno.IdTurno);
            if (turno!=null && turnoUpdate!=null)
            {
                turnoUpdate.Cliente = turno.Cliente;
                turnoUpdate.Hora = turno.Hora;
                turnoUpdate.Fecha = turno.Fecha;
                _DbContext.TTurnos.Update(turnoUpdate);
                return _DbContext.SaveChanges() == 1;
            }
            else
            {
                return false;
            }
        }
    }
}
