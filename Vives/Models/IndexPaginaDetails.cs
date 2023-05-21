using Vives.SDK;
using VivesRental.Services.Model.Results;
using VivesRental.UI.SDK;

namespace VivesRental.UI.Models
{
    public class IndexPaginaDetails
    {
        public IList<CustomerResult> Customers { get; set; }
        public IList<ProductResult> Products { get; set; }
        public IList<ArticleResult> Articles { get; set; }
        public IList<OrderResult> Orders { get; set; }
        public IList<ArticleReservationResult> Reservations { get; set; }
        public IList<OrderLineResult> Orderlines { get; set; }

    }
}
