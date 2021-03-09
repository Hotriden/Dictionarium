using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionarium.Models
{
    public class WordUserModel
    {
        public int UserId { get; set; }
        public UserModel UserModel { get; set; }

        public int WordId { get; set; }

        public Word Word { get; set; }
    }
}
