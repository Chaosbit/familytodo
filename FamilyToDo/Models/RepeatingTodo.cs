using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyToDo.Models
{
    public class RepeatingTodo
    {
        public Guid ID { get; set; }

        public ICollection<ToDoModel> ToDoModel { get; set; }

        public DateTime Start { get; set; }

        public RepeatingMode Modus { get; set; }

        public int RepeatingValue { get; set; }
    }

    public enum RepeatingMode
    {
        DayOfTheWeek,
        DayOfTheMonth,
        EveryXDays,
        EveryXWeeks,
        EveryXMonth,

    }
}
