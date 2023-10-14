using System;
namespace Model
{
	public class Contact : IEquatable<Contact>
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public bool Equals(Contact? other)
			=> Id == other.Id;

		public override bool Equals(object? obj)
		{
			if(ReferenceEquals(obj, null)) return false;
			if(ReferenceEquals(this, obj)) return true;
			if(GetType() != obj.GetType()) return false;
			return Equals(obj as Contact);
		}

		public override int GetHashCode()
			=> Id.GetHashCode();
	}
}

