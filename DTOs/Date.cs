using System;

namespace DTOs
{
    public class Date
    {

        public int Week { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return Year + "- " + Week;
        }
    }
}
