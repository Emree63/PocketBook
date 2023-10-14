using static Model.Book;

namespace Model;

public class Book : IEquatable<Book>
{
	public string Id { get; set; }
	public string Title { get; set; }
	public List<string> Publishers { get; set; } = new List<string>();
	public DateTime PublishDate { get; set; }
	public string ISBN13 { get; set; }
	public List<string> Series { get; set; } = new List<string>();
	public int NbPages { get; set; }
	public string Format { get; set; }
	public Languages Language { get; set; }
	public List<Contributor> Contributors { get; set; }
	public string ImageSmall => $"https://covers.openlibrary.org/b/isbn/{ISBN13}-S.jpg";
	public string ImageMedium => $"https://covers.openlibrary.org/b/isbn/{ISBN13}-M.jpg";
	public string ImageLarge  => $"https://covers.openlibrary.org/b/isbn/{ISBN13}-L.jpg";
    public List<Work> Works { get; set; } = new List<Work>();
	public List<Author> Authors { get; set; } = new List<Author>();
	public Status Status { get; set; }
	public List<string> UserTags { get; set; } = new List<string>();
	public float? UserRating { get; set; }
	public string UserNote { get; set; }

    public bool Equals(Book? other)
		=> Id == other.Id;

    public override bool Equals(object? obj)
    {
		if(ReferenceEquals(obj, null)) return false;
		if(ReferenceEquals(this, obj)) return true;
		if(GetType() != obj.GetType()) return false;
        return Equals(obj as Book);
    }

    public override int GetHashCode()
		=> Id.GetHashCode();
}

