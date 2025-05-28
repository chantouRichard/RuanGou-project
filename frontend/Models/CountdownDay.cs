using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace frontend.Models
{
    using System;

    public class CountdownDay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TargetDate { get; set; }
        public int DaysDifference { get; set; }

        public string DisplayText
        {
            get
            {
                if (DaysDifference == 0)
                    return $"{Name}: 就是今天！";
                else if (DaysDifference > 0)
                    return $"{Name}: {DaysDifference}天后是{Name}";
                else
                    return $"{Name}: 已经过去{-DaysDifference}天";
            }
        }
    }

}
