using System;
using Model;
using MyMVVM_ToolKit;

namespace ViewModel
{
	public class BorrowingVM : BaseViewModel<Borrowing>
	{
        public BorrowingVM(Borrowing borrowing) : base(borrowing)
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

        public string Title
        {
            get => Model.Book.Title;
            set
            {
                Model.Book.Title = value;
            }
        }

        public string Image
        {
            get => Model.Book.ImageMedium;
        }

        public string Authors
        {
            get
            {
                string authors = string.Join(", ", Model.Book.Authors.Select(a => a.Name));
                string worksAuthors = string.Join(", ", Model.Book.Works.SelectMany(w => w.Authors.Select(a => a.Name)));

                var result = authors != "" ? authors + ", " + worksAuthors : worksAuthors;
                return result;
            }
        }

        public string FirstAuthor
        {
            get
            {
                var allAuthors = Model.Book.Authors.Union(
                    Model.Book.Works.SelectMany(work => work.Authors)
                );

                var firstAuthor = allAuthors.FirstOrDefault();

                if (firstAuthor != null)
                {
                    return firstAuthor.Name;
                }
                else
                {
                    return "no author";
                }
            }
        }

        public string Status
        {
            get => Model.Book.Status.ToString();
        }

        public float? UserRating
        {
            get => Model.Book.UserRating;
            set
            {
                Model.Book.UserRating = value;
            }
        }
    }
}

