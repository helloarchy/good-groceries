using System;

namespace GoodGroceries.Models
{
    public class ConsoleOption
    {
        public string Name { get; }
        public Action Selected { get; }

        public ConsoleOption(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}