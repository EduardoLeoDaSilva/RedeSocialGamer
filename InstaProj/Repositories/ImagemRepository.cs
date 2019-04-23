using InstaProj.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Repositories
{
    public class ImagemRepository : IImagemRepository
    {
        private readonly ApplicationContext _context;

        public ImagemRepository(ApplicationContext context)
        {
            _context = context;
        }


        public byte[] GetImagensPostagem(int idImagem)
        {
            var imagem = _context.Set<Imagem>().Where(i => i.ImagemId == idImagem).SingleOrDefault();
            if(imagem != null)
            {
                return imagem.foto;
            }

            throw new ArgumentException("Não foi achado nenhuma postagem com esse id", nameof(idImagem));
        }


    }
}
