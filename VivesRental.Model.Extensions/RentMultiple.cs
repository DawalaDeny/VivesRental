namespace VivesRental.UI.Models
{
    public class RentMultiple
    {
        public Guid orderId { get; set; }
       public  IList<Guid> articleIds { get; set; }

    }
}
