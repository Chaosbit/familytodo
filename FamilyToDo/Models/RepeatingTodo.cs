using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyToDo.Models
{
    public class RepeatingTodo
    {
        public Guid ID { get; set; }

        public ICollection<ToDoModel> ToDoModel { get; set; }

        public DateTime Start { get; set; } = DateTime.Now;

        public RepeatingMode Modus { get; set; }

        public int RepeatingValue { get; set; }

        public Guid MasterTodoID { get; set; }
    }

    public enum RepeatingMode
    {
        [Display(Name = "Day of the week")]
        DayOfTheWeek,
        [Display(Name = "Day of the month")]
        DayOfTheMonth,
        [Display(Name = "Every x days")]
        EveryXDays,
        [Display(Name = "Every x weeks")]
        EveryXWeeks,
        [Display(Name = "Every x month")]
        EveryXMonth,

    }
}
