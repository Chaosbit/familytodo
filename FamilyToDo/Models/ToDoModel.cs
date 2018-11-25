
namespace FamilyToDo.Models
{
    using System;
    using System.ComponentModel;

    public class ToDoModel
    {
        public Guid ID { get; set; } = new Guid();

        public string Title { get; set; }

        public ToDoStatus Status { get; set; } = ToDoStatus.Open;

        public Guid ToDoListID { get; set; }

        public ToDoList ToDoList { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Completed { get; set; }

        public Guid RepeatingTodoID { get; set; }

        public RepeatingTodo RepeatingTodo { get; set; } 
    }

    public enum ToDoStatus
    {
        [DisplayName("Open")]
        Open,
        [DisplayName("Closed")]
        Closed
    }
}
