namespace MvvmPresentation.Data.Data.Model
{
    internal class OrderDTO
    {
        public int Id { get; init; }

        public DateTime Created { get; init; }

        public decimal Sum { get; init; }

        public int CustomerId { get; init; }
    }
}
