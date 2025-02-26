using CEA.Application.DTOs.Oficios;
using Google.Apis.Auth.OAuth2;

namespace CEA.Application.Services
{
    public interface IGoogleCloudService
    {
        MemoryStream DriveExportWord(OficioDto _oficioDto);
        void DocumentUpdate(OficioDto _oficioDto);
        Google.Apis.Drive.v3.Data.File FileMetadata(OficioDto _oficioDto);

        GoogleCredential GetGoogleCredential();

    }
}
