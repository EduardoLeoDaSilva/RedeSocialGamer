using System.Threading.Tasks;
using InstaProj.Models.Entidades;
using InstaProj.Models.ViewModels;

namespace InstaProj.Repositories
{
    public interface IUsuarioRepository
    {
        Task CadastrarUsuario(UsuarioViewModel usuarioView);
        Usuario GetUsuarioPorEmail(string email);
    }
}