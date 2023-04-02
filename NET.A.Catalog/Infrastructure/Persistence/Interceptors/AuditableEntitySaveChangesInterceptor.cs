using Application.Common.Interfaces;
using Domain.Common;

namespace Infrastructure.Persistence.Interceptors;
public class AuditableEntitySaveChangesInterceptor : IAuditableEntitySaveChangesInterceptor
{
    private readonly IDateTime _dateTime;

    public AuditableEntitySaveChangesInterceptor(IDateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public void InitialDate(BaseAuditableEntity entry)
    {
        entry.Created = _dateTime.Now;
    }

    public void UpdateDate(BaseAuditableEntity entry)
    {
        entry.LastModified = _dateTime.Now;
    }
}