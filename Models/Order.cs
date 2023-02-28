using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Shipped { get; set; } = false;
        [Range(1.00, double.MaxValue)]
        [Column(TypeName ="decimal(8,2)")]
        public decimal TotalPrice { get; set; }
        public int ItemCount { get; set; }

        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(8,2)")]
        public decimal TaxAmount { get; set; }

        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPayment { get; set; }

        [Range(0.00, double.MaxValue)]
        [Column(TypeName = "decimal(8,2)")]
        public decimal TipAmount { get; set; }

        public IEnumerable<CartLine> Lines { get; set; }
    }

    public class CartLine
    {
        public int CartLineId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
