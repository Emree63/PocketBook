using System;
namespace Model
{
	public class Loan : IEquatable<Loan> //prêt
	{
		public string Id { get; set; }
		public Book Book { get; set; }
		public Contact Loaner  { get; set; }
		public DateTime LoanedAt { get; set; }
		public DateTime? ReturnedAt { get; set; }

		public bool Equals(Loan? other)
			=> Id == other.Id;

		public override bool Equals(object? obj)
		{
			if(ReferenceEquals(obj, null)) return false;
			if(ReferenceEquals(this, obj)) return true;
			if(GetType() != obj.GetType()) return false;
			return Equals(obj as Loan);
		}

		public override int GetHashCode()
			=> Id.GetHashCode();
	}
}

