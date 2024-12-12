

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities;

namespace CEA.Application.DTOs
{
    public class DeptoUeDto : IMapFrom<DeptoUe>
    {
        public int Id { get; set; }
        public int IdCea { get; set; }
        public int IdShpoa { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Nivel { get; set; }
        public int Oficial { get; set; }
        public int IdReporta { get; set; }
        public int AgrupaPoa { get; set; }
        public int Meta { get; set; }
        public int Accion { get; set; }
        public string Prog { get; set; } = string.Empty;
        public int EmpRespon { get; set; }
        public int AgrupaDir { get; set; }
    }
}
