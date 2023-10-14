using System;
using System.Collections.Generic;
using Model;
using MyMVVM_ToolKit;

namespace ViewModel
{
	public class BookVM : BaseViewModel<Book>
	{
		public BookVM(Book book) : base(book)
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
            get => Model.Title;
            set
            {
                Model.Title = value;
            }
        }

        public string Image
        {
            get => Model.ImageMedium;
        }

        public string Authors
        {
            get
            {
                string authors = string.Join(", ", Model.Authors.Select(a => a.Name));
                string worksAuthors = string.Join(", ", Model.Works.SelectMany(w => w.Authors.Select(a => a.Name)));

                var result = authors != "" ? authors + ", " + worksAuthors : worksAuthors;
                return result;
            }
        }

        public string FirstAuthor
        {
            get
            {
                var allAuthors = Model.Authors.Union(
                    Model.Works.SelectMany(work => work.Authors)
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

        public string Publishers
        {
            get => string.Join(", ", Model.Publishers);
        }

        public string Resume
        {
            get => Model.Works.FirstOrDefault().Description ?? "";
        }

        public string Status
        {
            get => Model.Status.ToString();
        }

        public int NbPages
        {
            get => Model.NbPages;
            set
            {
                Model.NbPages = value;
            }
        }

        public string Language
        {
            get => Model.Language.ToString();
        }

        public string ISBN13
        {
            get => Model.ISBN13;
            set
            {
                Model.ISBN13 = value;
            }
        }

        public float? UserRating
        {
            get => Model.UserRating;
            set
            {
                Model.UserRating = value;
            }
        }

        public string PublishDate
        {
            get => Model.PublishDate.ToString("dd MMMM yyyy");
        }
    }
}

