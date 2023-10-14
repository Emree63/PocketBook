using System;
namespace Model
{
	public class Author : IEquatable<Author>
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ImageSmall => $"https://covers.openlibrary.org/a/olid/{Id.Substring(Id.LastIndexOf("/"))}-S.jpg";
		public string ImageMedium => $"https://covers.openlibrary.org/a/olid/{Id.Substring(Id.LastIndexOf("/"))}-M.jpg";
		public string ImageLarge => $"https://covers.openlibrary.org/a/olid/{Id.Substring(Id.LastIndexOf("/"))}-L.jpg";
		public string Bio { get; set; }
		public List<string> AlternateNames { get; set; } = new List<string>();
		public List<Link> Links { get; set; }
		public DateTime? BirthDate { get; set; }
		public DateTime? DeathDate { get; set; }

		public bool Equals(Author? other)
			=> Id == other.Id;

		public override bool Equals(object? obj)
		{
			if(ReferenceEquals(obj, null)) return false;
			if(ReferenceEquals(this, obj)) return true;
			if(GetType() != obj.GetType()) return false;
			return Equals(obj as Author);
		}

		public override int GetHashCode()
			=> Id.GetHashCode();
	}
}

