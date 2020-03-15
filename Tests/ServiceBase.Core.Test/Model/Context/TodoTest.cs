using System;
using CleanDdd.Common.Model.Identity;
using  CleanSimpleSolid.Core.Model.Tasks;
using Xunit;

namespace ServiceBase.Core.Test.Model.Context
{
    /// <summary>
    /// Unit tests primarily focus on the domain model as it contains the business logic.
    /// </summary>
    public class ToDoTest
    {
        public const string TaskName = "Task1";

        /// <summary>
        /// Test we can create a task and it's state is valid.
        /// </summary>
        [Fact]
        public void CreateTest()
        {
            var obj = new Todo(TaskName );
            
            Assert.Equal(TaskName,obj.Name);
            Assert.True(obj.IsValid());
        }

        /// <summary>
        /// Test we can create a task and set the due date.
        /// </summary>
        [Fact]
        public void CreateSetDueDateTest()
        {
            var dueDate = DateTimeOffset.Now.AddDays(2);
            var obj = new Todo(TaskName);
            obj.SetDueDate(dueDate);
            
            Assert.Equal(dueDate,obj.DueDate);
            Assert.Equal(dueDate, obj.ScheduleDate);  //scheduled date is set as it's null.
            Assert.True(obj.IsValid());
        }
    }
}