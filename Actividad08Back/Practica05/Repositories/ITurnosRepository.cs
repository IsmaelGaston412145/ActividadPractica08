using Practica05.Models;

namespace Practica05.Repositories
{
    public interface ITurnosRepository
    {
        bool Save(Turno turno);

        bool Delete(int id);

        bool Update(Turno turno);

        List<Turno> GetAll();

        List<Turno> GetAllByFilters(string cliente, string? fecha=null, string? hora=null);
    }
}