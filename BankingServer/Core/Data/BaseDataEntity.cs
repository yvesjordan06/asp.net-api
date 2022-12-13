namespace BankingServer.Core.Data;

public class BaseDataEntity
{
     public DateTime CreatedDate { get; set; }
     public DateTime ModifiedDate { get; set; }
     public DateTime? DeletedDate { get; set; }
     
     //Public getter isDeleted
     public bool IsDeleted => DeletedDate.HasValue;
}