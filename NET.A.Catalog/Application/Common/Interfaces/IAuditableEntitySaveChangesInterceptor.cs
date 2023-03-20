using Domain.Common;

namespace Application.Common.Interfaces
{
    public interface IAuditableEntitySaveChangesInterceptor
    {
        void InitialDate(BaseAuditableEntity entry);

        void UpdateDate(BaseAuditableEntity entry);
    }
}
