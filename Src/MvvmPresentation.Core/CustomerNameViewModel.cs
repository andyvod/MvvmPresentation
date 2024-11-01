namespace MvvmPresentation.Core
{
    public class CustomerNameViewModel
    {
        public CustomerNameViewModel(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public int Id { get; }
        public string FullName { get; }
    }
}
