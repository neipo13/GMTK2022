using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.dialogue
{
    [System.Serializable]
    public  class OptionLine
    {
        public Action OnChoice;
        public string Text;
    }
}
