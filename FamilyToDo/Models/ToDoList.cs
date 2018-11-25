using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyToDo.Models
{
    public class ToDoList
    {
        public Guid ID { get; set; } = new Guid();

        public string Name { get; set; }

        public uint Order { get; set; }

        public ICollection<ToDoModel> ToDos { get; set; }
        

    }
}
