using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioFunctions : IOficioFunctions
    {
        private readonly ApplicationDbContext _context;

        public OficioFunctions(ApplicationDbContext context)
        {
            _context = context;
        }

        public string ConvertOficioBitacoraDtoToXml(List<OficioBitacoraDto> datos)
        {
            System.Xml.Linq.XElement xml = new System.Xml.Linq.XElement("Root",
                datos.Select(x => new System.Xml.Linq.XElement("Row",
                    new System.Xml.Linq.XElement("EJERCICIO", x.Ejercicio),
                    new System.Xml.Linq.XElement("FOLIO", x.Folio),
                    new System.Xml.Linq.XElement("EOR", x.Eor),
                    new System.Xml.Linq.XElement("FECHA_CAPTURA", x.FechaCaptura),
                    new System.Xml.Linq.XElement("ID_EMPLEADO", x.IdEmpleado),
                    new System.Xml.Linq.XElement("ESTATUS", x.Estatus),
                    new System.Xml.Linq.XElement("COMENTARIOS", x.Comentarios)

                ))
                );
            ;
            return xml.ToString();
        }

        public string ConvertOficioDtoToXml(OficioDto oficioDto)
        {
            System.Xml.Linq.XElement xml = new System.Xml.Linq.XElement("Registros",
                new System.Xml.Linq.XElement("Registro",
                    new System.Xml.Linq.XElement("EJERCICIO", oficioDto.Ejercicio),
                    new System.Xml.Linq.XElement("FOLIO", oficioDto.Folio),
                    new System.Xml.Linq.XElement("EOR", oficioDto.Eor),
                    new System.Xml.Linq.XElement("TIPO", oficioDto.Tipo),
                    new System.Xml.Linq.XElement("NO_OFICIO", oficioDto.NoOficio),
                    new System.Xml.Linq.XElement("PDFPATH", oficioDto.Pdfpath),
                    new System.Xml.Linq.XElement("FECHA", oficioDto.Fecha),
                    new System.Xml.Linq.XElement("FECHA_CAPTURA", oficioDto.FechaCaptura),
                    new System.Xml.Linq.XElement("FECHA_ACUSE", oficioDto.FechaAcuse),
                    new System.Xml.Linq.XElement("FECHA_LIMITE", oficioDto.FechaLimite),
                    new System.Xml.Linq.XElement("REM_DEPEN", oficioDto.RemDepen),
                    new System.Xml.Linq.XElement("REM_SIGLAS", oficioDto.RemSiglas),
                    new System.Xml.Linq.XElement("REM_NOMBRE", oficioDto.RemNombre),
                    new System.Xml.Linq.XElement("REM_CARGO", oficioDto.RemCargo),
                    new System.Xml.Linq.XElement("DEST_DEPEN", oficioDto.DestDepen),
                    new System.Xml.Linq.XElement("DEST_SIGLAS", oficioDto.DestSiglas),
                    new System.Xml.Linq.XElement("DEST_NOMBRE", oficioDto.DestNombre),
                    new System.Xml.Linq.XElement("DEST_CARGO", oficioDto.DestCargo),
                    new System.Xml.Linq.XElement("TEMA", oficioDto.Tema),
                    new System.Xml.Linq.XElement("ESTATUS", oficioDto.Estatus),
                    new System.Xml.Linq.XElement("EMPQENTREGA", oficioDto.Empqentrega),
                    new System.Xml.Linq.XElement("RELACIONOFICIO", oficioDto.Relacionoficio),
                    new System.Xml.Linq.XElement("DEPTO", oficioDto.Depto),
                    new System.Xml.Linq.XElement("DEPTORESPON", oficioDto.DeptoRespon),
                     //recorrer oficioDto.OficioBitacora y oficioDto.OficiosResponsables
                     new System.Xml.Linq.XElement("BITACORA_DATA",
                       oficioDto.OficioBitacora.Select(x => new System.Xml.Linq.XElement("BITACORA",
                        new System.Xml.Linq.XElement("FECHA_CAPTURA", x.FechaCaptura),
                        new System.Xml.Linq.XElement("ID_EMPLEADO", x.IdEmpleado),
                        new System.Xml.Linq.XElement("ESTATUS", x.Estatus),
                        new System.Xml.Linq.XElement("COMENTARIOS", x.Comentarios)

                    )
                )),
                new System.Xml.Linq.XElement("RESPOSABLE_DATA",
                    oficioDto.OficiosResponsables.Select(x => new System.Xml.Linq.XElement("RESPONSABLE",
                        new System.Xml.Linq.XElement("ID_EMPLEADO", x.IdEmpleado),
                        new System.Xml.Linq.XElement("ROL", x.Rol),
                        new System.Xml.Linq.XElement("IOX", 0)
                    )
                ))
            )
          );

            return xml.ToString();
        }

        public async Task<List<OficioContadoresDashboardDto>> GetContadoresDashboard(int ejercicio, int idEmpleado)
        {
            return await _context.oficioContadoresDashboardDtos.FromSqlInterpolated($"SELECT * FROM TABLE (F_CONTADORES_DASHBOARD1({ejercicio},{idEmpleado}))").ToListAsync();
        }

        public async Task<List<OficioListaDashboardDto>> GetListaDashboard(int ejercicio, int idEmpleado)
        {
            var entities = await _context.oficioListaDashboardDtos.FromSqlInterpolated($"SELECT * FROM TABLE (F_LISTA_DASHBOARD1({ejercicio},{idEmpleado}))").ToListAsync();

            return await _context.oficioListaDashboardDtos.FromSqlInterpolated($"SELECT * FROM TABLE (F_LISTA_DASHBOARD1({ejercicio},{idEmpleado}))").ToListAsync();
        }

        public async Task<List<OficioDtoFunction>> GetListadoOficioFunction(int ejercicio, int eor, int idEmpleado)
        {

            return await _context.OficioDtoFunction.FromSqlInterpolated($"SELECT * FROM TABLE (F_LISTADOOFICIOS({ejercicio},{eor},{idEmpleado}))").ToListAsync();
        }

        public Task SaveBitacoraOficio(List<OficioBitacoraDto> bitacoraOficio)
        {
            var xmlData = ConvertOficioBitacoraDtoToXml(bitacoraOficio);
            var param = new OracleParameter("p_data", OracleDbType.Clob)
            {
                Value = xmlData
            };

            _context.Database.ExecuteSqlRaw("BEGIN INSERT_OFICIOS_BITACORATEST(:p_data); END;", param);
            Console.WriteLine("Bitacora guardada");
            return Task.CompletedTask;
        }

        public Task SaveOficioSP(OficioDto oficioDto)
        {
            throw new NotImplementedException();
        }
    }
}
