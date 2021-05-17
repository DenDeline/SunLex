using System;
using System.ComponentModel.DataAnnotations;

namespace SunLex.WebAPI.Endpoints.Dictionary
{
    public class GetDictionaryByIdRequest
    {
        [Required]
        public Guid DictionaryId { get; set; }
    }
}
