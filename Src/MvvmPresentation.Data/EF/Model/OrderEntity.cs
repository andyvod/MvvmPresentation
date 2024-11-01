using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvvmPresentation.Data.EF.Model
{
    [Table("Orders")]
    internal class OrderEntity
    {
        public OrderEntity(int id, DateTime created, decimal sum, CustomerEntity customer)
        {
            Id = id;
            Created = created;
            Sum = sum;
            Customer = customer;
        }
        [Key]
        public int Id { get; }

        [Column("Created")]
        public DateTime Created { get; }

        [Column("Sum")]
        public decimal Sum { get; }

        [Column("CustomerId")]
        public CustomerEntity Customer { get; }
    }
}
