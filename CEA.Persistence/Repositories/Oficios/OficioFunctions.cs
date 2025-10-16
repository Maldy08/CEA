using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data;

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
                    new System.Xml.Linq.XElement("FECHA_CAPTURA", x.FechaCaptura.Date.Add(DateTime.Now.TimeOfDay),
                    new System.Xml.Linq.XElement("ID_EMPLEADO", x.IdEmpleado),
                    new System.Xml.Linq.XElement("ESTATUS", x.Estatus),
                    new System.Xml.Linq.XElement("COMENTARIOS", x.Comentarios)

                ))
                ));
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
                    new System.Xml.Linq.XElement("FECHA_CAPTURA", oficioDto.FechaCaptura.Date.Add(DateTime.Now.TimeOfDay)),
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
                    new System.Xml.Linq.XElement("DEPTO_RESPON", oficioDto.DeptoRespon),
                    new System.Xml.Linq.XElement("ID_CLASIFICACION", oficioDto.idClasificacion),
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
                        new System.Xml.Linq.XElement("IOX", 1)
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

        public Task<OficioSpFoliarResult> OficioSPFoliar(int ejercicio, int folio, int eor, int idEmpleado)
        {
            var ejercicioParam = new OracleParameter("P_EJERCICIO", OracleDbType.Int32)
            {
                Value = ejercicio
            };

            var folioParam = new OracleParameter("P_FOLIO", OracleDbType.Int32)
            {
                Value = folio
            };

            var eorParam = new OracleParameter("P_EOR", OracleDbType.Int32)
            {
                Value = eor
            };

            var idEmpleadoParam = new OracleParameter("P_EMPLEADO", OracleDbType.Int32)
            {
                Value = idEmpleado
            };

            var mensajeParam = new OracleParameter("P_MENSAJE", OracleDbType.Varchar2, 100)
            {
                Direction = ParameterDirection.Output
            };

            var folioNuevo = new OracleParameter("P_FOLIO_NUEVO", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };

            _context.Database.ExecuteSqlRaw("BEGIN SP_ACTUALIZAR_OFICIO(:P_EJERCICIO, :P_EOR, :P_FOLIO, :P_EMPLEADO, :P_FOLIO_NUEVO,:P_MENSAJE ); END;", ejercicioParam, eorParam,folioParam, idEmpleadoParam, folioNuevo,mensajeParam);

            var mensaje = mensajeParam.Value.ToString();
            var folioNuevoInt = folioNuevo.Value.ToString();
            return Task.FromResult(new OficioSpFoliarResult { MENSAJE = mensaje!, FOLIO_NUEVO = Convert.ToInt32(folioNuevoInt) });
        }

        public Task<OficioSpInsertarResult> SaveOficioSP(OficioDto oficioDto)
        {
            var xmlData = ConvertOficioDtoToXml(oficioDto);
            var param = new OracleParameter("P_DATOS", OracleDbType.Clob)
            {
                Value = xmlData
            };

            var resultadoParam = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, 100)
            {
                Direction = ParameterDirection.Output
            };

            var folioParam = new OracleParameter("P_FOLIO", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };

            var noOficioParam = new OracleParameter("P_NO_OFICIO", OracleDbType.Varchar2, 50)
            {
                Direction = ParameterDirection.Output
            };

            _context.Database.ExecuteSqlRaw("BEGIN INSERT_OFICIOS_COMPLETO(:P_DATOS, :P_RESULTADO, :P_FOLIO, :P_NO_OFICIO); END;", param, resultadoParam, folioParam, noOficioParam);

            var resultado = resultadoParam.Value.ToString();
            var folio = folioParam.Value.ToString();
            var noOficio = noOficioParam.Value.ToString();

            return Task.FromResult(new OficioSpInsertarResult { Resultado = resultado!, Folio = Convert.ToInt32(folio), NoOficio = noOficio! });

        }

        public async Task<List<OficioCppDto>> OficioCpp(int ejercicio, int folio)
        {
            return await _context.oficioCppDtos
                .FromSqlInterpolated($"SELECT * FROM TABLE (F_LISTADOCCP({ejercicio},{folio}))")
                .ToListAsync();
        }
    }
}
