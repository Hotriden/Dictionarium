using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionarium.Models
{
    public class Word
    {
        public int WordId { get; set; }

        public string Name { get; set; }

        public string Translation { get; set; }

        public ICollection<WordUserModel> WordUserModels { get; set; }
    }
}
