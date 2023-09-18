using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Model
{
	public class Borrowing //emprunt
	{
		public string Id { get; set; }
		public Person Owner  { get; set; }
		public DateTime BorrowedAt { get; set; }
		public DateTime? ReturnedAt { get; set; }
	}
}

