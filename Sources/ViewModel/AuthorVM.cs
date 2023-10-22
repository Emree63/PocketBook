using Model;

namespace ViewModel
{
	public class AuthorVM : BaseViewModel<Author>
	{
		public AuthorVM(Author author) : base(author)
        {
		}

        public string Id
        {
            get => Model.Id;
            set
            {
                Model.Id = value;
            }
        }

        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
            }
        }
    }
}

