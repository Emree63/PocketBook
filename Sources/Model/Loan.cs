using System;
namespace Model
{
	public class Loan //prêt
	{
		public string Id { get; set; }
		public Person Loaner  { get; set; }
		public DateTime LoanedAt { get; set; }
		public DateTime? ReturnedAt { get; set; }
	}
}

