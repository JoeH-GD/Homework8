using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueFalseGame
{
   public class Questions
    {

        public string Text { get; set; }
        public bool TrueFalse { get; set; }

        public Questions(string text, bool trueFalse)
        {
            this.Text = text;
            this.TrueFalse = trueFalse;
        }

        public Questions() { }
    }
}
