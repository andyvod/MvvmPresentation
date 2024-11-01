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

        //For EF
        private OrderEntity()
        {
                
        }

        [Key]
        public int Id { get; private set;  }

        [Column("Created")]
        public DateTime Created { get; private set; }

        [Column("Sum")]
        public decimal Sum { get; private set; }

        [Column("CustomerId")]
        public CustomerEntity Customer { get; private set; } = new(0, string.Empty);
    }
}
