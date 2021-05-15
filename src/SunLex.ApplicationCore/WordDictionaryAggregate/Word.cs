using Ardalis.GuardClauses;
using SunLex.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunLex.ApplicationCore.WordDictionaryAggregate
{
    public class Word: BaseEntity
    {
        private Word()
        {

        }

        public Word(string spelling)
        {
            Spelling = Guard.Against.NullOrEmpty(spelling, nameof(spelling));
        }

        public string Spelling { get; private set; }
    }
}
