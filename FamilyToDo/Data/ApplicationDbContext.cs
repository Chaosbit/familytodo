using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FamilyToDo.Models;

namespace FamilyToDo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FamilyToDo.Models.ToDoModel> ToDoModel { get; set; }
        public DbSet<FamilyToDo.Models.ToDoList> ToDoList { get; set; }
        public DbSet<FamilyToDo.Models.RepeatingTodo> RepeatingTodos { get; set; }
    }
}
