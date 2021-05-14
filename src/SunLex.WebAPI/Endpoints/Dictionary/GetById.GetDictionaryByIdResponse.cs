using SunLex.ApplicationCore.Entities.WordDictionaryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunLex.WebAPI.Endpoints.Dictionary
{
    public class GetDictionaryByIdResponse
    {
        public IReadOnlyCollection<WordTranslation> WordsTranslations { get; set; }
        public string Name { get; internal set; }
        public int Id { get; internal set; }
    }
}
