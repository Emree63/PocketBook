using System;
namespace Model
{
	public class Work : IEquatable<Work>
	{
		public string Id { get; set; }
		public string Description { get; set; }
		public string Title { get; set; }
		public List<string> Subjects { get; set; } = new List<string>();
		public List<Author> Authors { get; set; } = new List<Author>();
		public Ratings Ratings { get; set; }

		public bool Equals(Work? other)
			=> Id == other.Id;

		public override bool Equals(object? obj)
		{
			if(ReferenceEquals(obj, null)) return false;
			if(ReferenceEquals(this, obj)) return true;
			if(GetType() != obj.GetType()) return false;
			return Equals(obj as Work);
		}

		public override int GetHashCode()
			=> Id.GetHashCode();
	}
}

