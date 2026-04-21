using CEA.Application.DTOs.Checador;
using CEA.Application.Services;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Checador.Queries
{
    public record GetAcsEventsFromDeviceQuery(string DeviceKey, DateTime StartTime, DateTime EndTime)
        : IRequest<Result<List<RegistroAsistenciaDto>>>;


    internal class GetAcsEventsFromDeviceQueryHandler
        : IRequestHandler<GetAcsEventsFromDeviceQuery, Result<List<RegistroAsistenciaDto>>>
    {
        private readonly IChecadorService _checadorService;

        public GetAcsEventsFromDeviceQueryHandler(IChecadorService checadorService)
        {
            _checadorService = checadorService;
        }

        public async Task<Result<List<RegistroAsistenciaDto>>> Handle(GetAcsEventsFromDeviceQuery request, CancellationToken cancellationToken)
        {
            const string timeZoneOffset = "-08:00";
            var requestBody = new AcsEventRequestDto
            {
                AcsEventCond = new AcsEventCond
                {
                    StartTime = request.StartTime.ToString("yyyy-MM-ddTHH:mm:ss") + timeZoneOffset,
                    EndTime = request.EndTime.ToString("yyyy-MM-ddTHH:mm:ss") + timeZoneOffset
                }
            };

            var eventData = await _checadorService.GetEventosAsync(request.DeviceKey, requestBody);

            if (eventData?.AcsEvent?.InfoList == null)
            {
                return await Result<List<RegistroAsistenciaDto>>.FailureAsync($"No se recibieron datos del dispositivo '{request.DeviceKey}'. Revise la conexión y las credenciales.");
            }

            var datosIntermedios = eventData.AcsEvent.InfoList.Select(info =>
            {

                int idEmpleado = 0;
                if (int.TryParse(info.EmployeeNoString, out int parsedId))
                {
                    idEmpleado = parsedId;
                    if (info.EmployeeNoString.EndsWith("0") && info.EmployeeNoString.Length > 4)
                    {
                        int.TryParse(info.EmployeeNoString.Substring(0, info.EmployeeNoString.Length - 1), out idEmpleado);
                    }
                }

                return new
                {
                    IdEmpleado = idEmpleado.ToString(),
                    Nombre = info.Name,
                    FechaHora = DateTime.Parse(info.Time)
                };
            });


            var cleanedList = datosIntermedios
                .GroupBy(e => new
                {
                    e.IdEmpleado,
                    e.FechaHora.Date
                })
                .Select(g => new RegistroAsistenciaDto
                {
                    IdEmpleado = g.Key.IdEmpleado,
                    Nombre = g.First().Nombre,
                    FechaPrimera = g.Min(e => e.FechaHora),

                    FechaUltima = (g.Count() > 1)
                                ? g.Max(e => e.FechaHora)
                                : (DateTime?)null
                })
                .OrderBy(r => r.IdEmpleado)
                .ThenBy(r => r.FechaPrimera)
                .ToList();


            return await Result<List<RegistroAsistenciaDto>>.SuccessAsync(cleanedList, $"Datos de '{request.DeviceKey}' extraídos y procesados.");
        }
    }
}