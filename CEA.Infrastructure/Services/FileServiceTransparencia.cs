using CEA.Application.DTOs;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Infrastructure.Services
{
    public class FileServiceTransparencia : IFileServiceTransparencia
    {
        protected readonly string rutaPredeterminadaOficios = "C:\\ceatransparencia\\";


        public async Task PostMultiFileAsync(List<FileUploadDto> fileData)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), rutaPredeterminadaOficios, fileData.FirstOrDefault().FolderName, fileData.FirstOrDefault().FileName);
            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                foreach (var file in fileData)
                {
                    await file.File.CopyToAsync(fileStream);
                }
            }
        }
    }
}
