using Practica05.Models;

namespace Practica05.Repositories
{
    public interface IServiciosRepository
    {
        List<Servicio> GetAll();

        bool Save(Servicio servicio);

    }
}
