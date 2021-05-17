using System.ComponentModel.DataAnnotations;

namespace SunLex.WebAPI.Endpoints.Dictionary
{
    public class CreateWordDictionaryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
