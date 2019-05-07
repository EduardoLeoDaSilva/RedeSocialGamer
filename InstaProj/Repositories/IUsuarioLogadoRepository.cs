using System.Collections.Generic;

namespace InstaProj.Repositories
{
    public interface IUsuarioLogadoRepository
    {
        void AddLoggedUser(int userId);
        List<int> GetLoggedUsersid();
        void RemoveLoggedUser(int userId);
    }
}