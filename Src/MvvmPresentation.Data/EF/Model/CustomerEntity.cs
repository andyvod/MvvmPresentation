using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvvmPresentation.Data.EF.Model
{
    [Table("Customers")]
    internal class CustomerEntity
    {
        public CustomerEntity(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        [Key]
        public int Id { get; }

        [Column("Name")]
        public string FullName { get; }

        public virtual IList<OrderEntity> Orders { get; private set; } = [];
    }
}
