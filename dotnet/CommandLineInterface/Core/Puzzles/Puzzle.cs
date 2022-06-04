using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.CommandLineInterface.Core
{
    public abstract class Puzzle
    {
        public class Answer
        {
            public string Description { get; }
            public long Value { get; }

            public Answer(long value, string description)
            {
                Description = description;
                Value = value;
            }
        }

        public abstract IEnumerable<Answer> Run(string[] lines);
    }
}
