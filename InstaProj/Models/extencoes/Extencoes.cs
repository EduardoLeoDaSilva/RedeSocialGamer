using InstaProj.Models.Entidades;
using InstaProj.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Models.extencoes
{
    public static class Extencoes
        
    {
        public static byte[] LerStreamFoto(IFormFile formFile)
        {
            using (var stream = formFile.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
   
                return memoryStream.ToArray();
            }
        }

        public static List<byte[]> toListaBytes(this IFormFileCollection formFiles)
        {
            var list = new List<byte[]>();
            foreach (var formFile in formFiles)
            {
                using (var streamFile = formFile.OpenReadStream())
                using (var memoryStream = new MemoryStream())
                {
                    streamFile.CopyTo(memoryStream);
                    var bytesFile = memoryStream.ToArray();
                    list.Add(bytesFile);
                }
            }
            return list;
        }

        public static byte[] ObterFotoBytes(this UsuarioViewModel userView)
        {
            var fileUploaded = userView.Foto;
            var bytesFoto = LerStreamFoto(fileUploaded);
            return bytesFoto;
        }

        public static PostagemViewModel toViewModel(this Postagem postagem)
        {
            if (postagem != null)
            {
                var listaImageLink = new List<string>();
                foreach(var imagemLink in postagem.Imagens)
                {
                    listaImageLink.Add($"CarregarImagemPostagem/{ imagemLink.ImagemId}");
                }

                var viewModel = new PostagemViewModel(postagem.PostagemId, listaImageLink, postagem.Texto, postagem.Comentarios, postagem.Likes);
                return viewModel;
            }
            throw new NullReferenceException("Objeto null, o objeto postagem não pode ser nulo");
        }

    }
}
