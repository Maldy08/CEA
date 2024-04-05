

namespace CEA.Domain.Common.Interfaces
{
    public interface IAuditableEntity : IEntity
    {
         int? CreatedBy { get; set; }
         DateTime? CreatedDate { get; set; }
         int? UpdateBy { get; set; }
         DateTime? UpdateDate { get; set; }
    }
}
