using System;
using Model;

namespace ViewModel
{
	public class BookVM
	{
		public BookVM(Book book)
		{
			model = book;
		}

		public Book Model
		{
			get => model;
			set
			{
				if(model != value)
				{
                    model = value;
				}
			}
		}
		private Book model;
	}
}

