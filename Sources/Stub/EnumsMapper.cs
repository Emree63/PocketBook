using System;
using Utils;

namespace Stub
{
	static class EnumsMapper
    {
        public static EnumsMapper<Model.Languages, LibraryDTO.Languages> BiddingsMapper { get; }
            = new EnumsMapper<Model.Languages, LibraryDTO.Languages>(
                Tuple.Create(Model.Languages.Unknown, LibraryDTO.Languages.Unknown),
                Tuple.Create(Model.Languages.French, LibraryDTO.Languages.French),
                Tuple.Create(Model.Languages.English, LibraryDTO.Languages.English)
                );

        public static TModel ToModel<TModel, TDTO>(this TDTO dto) where TModel : Enum
                                                                           where TDTO : Enum
        {
            foreach(var prop in typeof(EnumsMapper).GetProperties())
            {
                if(prop.PropertyType.Equals(typeof(EnumsMapper<TModel, TDTO>)))
                {
                    return (prop.GetValue(null) as EnumsMapper<TModel, TDTO>).GetModel(dto);
                }
            }
            return default(TModel);
        }

        public static Model.Languages ToModel(this LibraryDTO.Languages dto)
            => ToModel<Model.Languages, LibraryDTO.Languages>(dto);

        public static TDTO ToDTO<TModel, TDTO>(this TModel model) where TModel : Enum
                                                                           where TDTO : Enum
        {
            foreach(var prop in typeof(EnumsMapper).GetProperties())
            {
                if(prop.PropertyType.Equals(typeof(EnumsMapper<TModel, TDTO>)))
                {
                    return (prop.GetValue(null) as EnumsMapper<TModel, TDTO>).GetEntity(model);
                }
            }
            return default(TDTO);
        }

        public static LibraryDTO.Languages ToDTO(this Model.Languages model)
            => ToDTO<Model.Languages, LibraryDTO.Languages>(model);
    }

}

