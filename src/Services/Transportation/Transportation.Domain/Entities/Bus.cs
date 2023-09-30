using Transportation.Domain.Enums;

namespace Transportation.Domain.Entities;

public class Bus : FullAuditedEntityBase<Guid>, IEntity<Guid>
{
    public Bus()
    {
        this.CreatedOn = DateTime.Now;
        this.CreatedBy = "Yasser";
        this.Id = Guid.NewGuid();
    }
    public string Name { get; private set; }
    public string NameAr { get; private set; }
    public int? ReservationStatusId { get; private set; }
    public ReservationStatus ReservationStatus { get; private set; }

    public void Reserve()
    {
        this.ReservationStatusId = ReservationStatuses.Reserved.ToInt();
    }

    public void Release()
    {
        this.ReservationStatusId =  ReservationStatuses.NotReserved.ToInt();
    }

    public void Update(string name, string nameAr)
    {
        this.Name = name;
        this.NameAr = nameAr;
    }
}
