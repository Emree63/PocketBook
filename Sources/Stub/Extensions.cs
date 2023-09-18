using System;
using LibraryDTO;
using Model;
using static Stub.Mapper;

namespace Stub
{
	public static class Extensions
	{
		public static Ratings ToPoco(this RatingsDTO dto)
			=> new Ratings() { Average = dto.Average, Count = dto.Count };
		public static IEnumerable<Ratings> ToPocos(this IEnumerable<RatingsDTO> dtos)
			=> dtos.Select(dto => dto.ToPoco());

		public static Contributor ToPoco(this ContributorDTO dto)
			=> new Contributor { Name = dto.Name, Role = dto.Role };
		public static IEnumerable<Contributor> ToPocos(this IEnumerable<ContributorDTO> dtos)
			=> dtos.Select(dto => dto.ToPoco());

		public static Link ToPoco(this LinkDTO dto)
			=> new Link { Title = dto.Title, Url = dto.Url };
		public static IEnumerable<Link> ToPocos(this IEnumerable<LinkDTO> dtos)
			=> dtos.Select(dto => dto.ToPoco());

		public static Author ToPoco(this AuthorDTO dto)
		{
			var result = AuthorsMapper.GetT(dto);
			if(result == null)
			{
				result = new Author
				{
					AlternateNames = dto.AlternateNames,
					Bio = dto.Bio,
					BirthDate = dto.BirthDate,
					DeathDate = dto.DeathDate,
					Id = dto.Id,
					Links = dto.Links.ToPocos().ToList(),
					Name = dto.Name
				};
			}
			return result; 
		}
		public static IEnumerable<Author> ToPocos(this IEnumerable<AuthorDTO> dtos)
			=> dtos.Select(dto => dto.ToPoco());

		public static Work ToPoco(this WorkDTO dto)
		{
			var result = WorksMapper.GetT(dto);
			if(result == null)
			{
				result = new Work
				{
					Authors = dto.Authors.ToPocos().ToList(),
					Description = dto.Description,
					Id = dto.Id,
					Ratings = dto.Ratings.ToPoco(),
					Subjects = dto.Subjects,
					Title = dto.Title
				};
			}
			return result;
		}
		public static IEnumerable<Work> ToPocos(this IEnumerable<WorkDTO> dtos)
			=> dtos.Select(dto => dto.ToPoco());

		public static Book ToPoco(this BookDTO dto)
		{
			var result = BooksMapper.GetT(dto);
			if(result == null)
			{
				result = new Book
				{
					Authors = dto.Authors.ToPocos().ToList(),
					Contributors = dto.Contributors.ToPocos().ToList(),
					Format = dto.Format,
					Id = dto.Id,
					ISBN13 = dto.ISBN13,
					Language = dto.Language.ToModel(),
					NbPages = dto.NbPages,
					PublishDate = dto.PublishDate,
					Publishers = dto.Publishers,
					Series = dto.Series,
					Title = dto.Title,
					Works = dto.Works.ToPocos().ToList()
				};
			}
			return result;
		}
		public static IEnumerable<Book> ToPocos(this IEnumerable<BookDTO> dtos)
			=> dtos.Select(dto => dto.ToPoco());
	}
}

