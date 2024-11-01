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

        //For EF
        private CustomerEntity()
        {
                
        }

        [Key]
        public int Id { get; private set; }

        [Column("Name")]
        public string FullName { get; private set; } = string.Empty;

        public virtual IList<OrderEntity> Orders { get; private set; } = [];
    }
}
