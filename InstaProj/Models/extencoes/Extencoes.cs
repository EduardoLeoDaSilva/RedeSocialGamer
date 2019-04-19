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

        public static byte[] ObterFotoBytes(this UsuarioViewModel userView)
        {
            var fileUploaded = userView.Foto;
            var bytesFoto = LerStreamFoto(fileUploaded);
            return bytesFoto;
        }

    }
}
