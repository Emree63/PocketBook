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

        public string Title
        {
            get => Model.Title;
            set
            {
                Model.Title = value;
            }
        }

    }
}

