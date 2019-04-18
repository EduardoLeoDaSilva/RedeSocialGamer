using System.Threading.Tasks;
using InstaProj.Models.ViewModels;

namespace InstaProj.Repositories
{
    public interface IUsuarioRepository
    {
         Task CadastrarUsuario(UsuarioViewModel usuarioView);
    }
}