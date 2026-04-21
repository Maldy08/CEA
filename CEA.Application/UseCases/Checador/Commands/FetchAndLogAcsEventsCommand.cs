using CEA.Application.DTOs.Checador;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Services;
using CEA.Domain.Entities.Checador;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Checador.Commands
{

    public record FetchAndLogAcsEventsCommand : IRequest<Result<int>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }


    internal class FetchAndLogAcsEventsCommandHandler : IRequestHandler<FetchAndLogAcsEventsCommand, Result<int>>
    {

        private readonly IChecadorService _checadorService;

        public FetchAndLogAcsEventsCommandHandler(
            IChecadorService checadorService
            )
        {
            _checadorService = checadorService;
        }
        public async Task<Result<int>> Handle(FetchAndLogAcsEventsCommand request, CancellationToken cancellationToken)
        {
            // 1. CONSTRUIR EL REQUEST DTO
            // La API del dispositivo espera un formato ISO 8601 con offset.
            // Asumiré el offset de Mexicali (-8:00 o -7:00). Usaré -08:00 como en tu ejemplo.
            const string timeZoneOffset = "-08:00";

            var requestBody = new AcsEventRequestDto
            {
                AcsEventCond = new AcsEventCond
                {
                    // Los demás valores (searchID, maxResults, major, minor) usarán los defaults del DTO
                    StartTime = request.StartDate.ToString("yyyy-MM-ddTHH:mm:ss") + timeZoneOffset,
                    EndTime = request.EndDate.ToString("yyyy-MM-ddTHH:mm:ss") + timeZoneOffset
                }
            };

            // 2. LLAMAR AL SERVICIO con el body
            var eventData = await _checadorService.GetEventosAsync("",requestBody);

            if (eventData?.AcsEvent?.InfoList == null)
            {
                return await Result<int>.FailureAsync("No se recibieron eventos del dispositivo o hubo un error de conexión.");
            }

            // 3. PROCESAR Y GUARDAR (La lógica de guardado no cambia)
            var infoList = eventData.AcsEvent.InfoList;
            int registrosGuardados = 0;

            foreach (var info in infoList)
            {
                // (Tu lógica de limpieza de ID de empleado, ej. "71480" -> 7148)
                int idEmpleado = 0;
                if (int.TryParse(info.EmployeeNoString, out int parsedId))
                {
                    idEmpleado = parsedId;
                    if (info.EmployeeNoString.EndsWith("0") && info.EmployeeNoString.Length > 4)
                    {
                        int.TryParse(info.EmployeeNoString.Substring(0, info.EmployeeNoString.Length - 1), out idEmpleado);
                    }
                }

                var registro = new RegistroChecador
                {
                    IdEmpleado = idEmpleado,
                    FechaHora = DateTime.Parse(info.Time), // Esto parseará el string con offset
                    NombreEmpleado = info.Name,
                    TipoEventoMayor = info.Major,
                    TipoEventoMenor = info.Minor,
                    DoorNo = info.DoorNo,
                    PictureURL = info.PictureURL,
                    SerialNo = info.SerialNo
                };

                //await _unitOfWork.Repository<RegistroChecador>().AddAsync(registro);
                registrosGuardados++;
            }

           // await _unitOfWork.Save(cancellationToken);

            return await Result<int>.SuccessAsync(registrosGuardados, $"Se sincronizaron {registrosGuardados} eventos.");
        }
    }
}
