using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionarium.Models
{
    public class Word
    {
        public int WordId { get; set; }

        public int Name { get; set; }

        public int Translation { get; set; }

        public ICollection<WordUserModel> WordUserModels { get; set; }
    }
}
