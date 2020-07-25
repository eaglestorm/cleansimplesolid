using System;
using System.Collections.Generic;
using CleanDdd.Common.Model.Identity;
using  CleanSimpleSolid.Core.Model.Tasks;
using Xunit;

namespace ServiceBase.Core.Test.Model.Context
{
    /// <summary>
    /// Unit tests primarily focus on the domain model as it contains the business logic.
    /// </summary>
    public class TaskTest
    {
        private const string TaskName = "Task1";
        private const string TaskDescription = "This is a task description to descrive the task.";

        /// <summary>
        /// Test we can create a task and it's state is valid.
        /// </summary>
        [Theory]
        [InlineData("<script>")]
        [InlineData("--script")]
        public void InvalidNameShouldFail(string name)
        {
            var obj = new CssTask(name);
            
            Assert.False(obj.IsValid());
        }
        
        public void CanCreateNew()
        {
            var obj = new CssTask(TaskName );
            
            Assert.Equal(TaskName,obj.Name);
            Assert.True(obj.IsValid());
        }

        [Fact]
        public void CanCreateFull()
        {
            var dueDate = DateTimeOffset.Now.AddDays(1);
            var scheduledDate = DateTimeOffset.Now;
            var obj = new CssTask(new LongIdentity(1), TaskName, TaskDescription,dueDate,
                scheduledDate, DateTimeOffset.Now,DateTimeOffset.Now,null);
            
            Assert.True(obj.IsValid());
            Assert.Equal(TaskName,obj.Name);
            Assert.Equal(TaskDescription, obj.Description);
            Assert.Equal(dueDate,obj.DueDate);
            Assert.Equal(scheduledDate, obj.ScheduleDate);
        }
        
        /// <summary>
        /// Test we can create a task and set the due date.
        /// </summary>
        [Fact]
        public void CanSetDueDate()
        {
            var dueDate = DateTimeOffset.Now.AddDays(2);
            var obj = new CssTask(TaskName);
            obj.SetDueDate(dueDate);
            
            Assert.Equal(dueDate,obj.DueDate);
            Assert.Equal(dueDate, obj.ScheduleDate);  //scheduled date is set as it's null.
            Assert.True(obj.IsValid());
        }
        
        /// <summary>
        /// Test valid due dates
        /// </summary>
        /// <param name="yearDiff">Difference between now and the due date.</param>
        /// <param name="monthDiff"></param>
        /// <param name="dayDiff"></param>
        /// <param name="hourDiff"></param>
        /// <param name="minuteDiff"></param>
        /// <param name="secondDiff"></param>
        [Theory]
        [InlineData(0,1,0,0,0,0)]
        [InlineData(1,0,0,0,0,0)]
        [InlineData(0,0,1,0,1,0)]
        [InlineData(0,0,1,0,0,1)]
        [InlineData(0,0,1,1,0,0)]
        [InlineData(0,12,0,0,0,0)]
        [InlineData(0,0,1,5,0,0)]
        public void DueDateShoudBeValid(int yearDiff, int monthDiff, int dayDiff, int hourDiff, int minuteDiff, int secondDiff)
        {
            var dueDateToSet = DateTimeOffset.Now.AddYears(yearDiff).AddMonths(monthDiff).AddDays(dayDiff)
                .AddHours(hourDiff).AddMinutes(minuteDiff).AddSeconds(secondDiff);
            var dueDateStart = DateTimeOffset.Now.AddDays(2);
            var childDueDate = DateTimeOffset.Now.AddDays(1);
            var scheduledDate = DateTimeOffset.Now.AddDays(1);

            var obj = new CssTask(new LongIdentity(1), TaskName, TaskDescription,dueDateStart,scheduledDate
                , DateTimeOffset.Now,DateTimeOffset.Now,new List<SubTask>()
                {
                    new SubTask(new LongIdentity(2),TaskName,TaskDescription,childDueDate,childDueDate,DateTimeOffset.Now,DateTimeOffset.Now)
                });
            obj.SetDueDate(dueDateToSet);

            Assert.Equal(dueDateToSet,obj.DueDate);
            Assert.Equal(scheduledDate, obj.ScheduleDate);  //scheduled date is set as it's null.
            Assert.True(obj.IsValid());
        }
        
        [Theory]
        [InlineData(0,0,6,0,0,0)]
        [InlineData(0,0,-1,0,0,0)]
        [InlineData(0,0,6,23,59,59)]
        public void DueDateShoudBeInValid(int yearDiff, int monthDiff, int dayDiff, int hourDiff, int minuteDiff, int secondDiff)
        {
            var dueDateToSet = DateTimeOffset.Now.AddYears(yearDiff).AddMonths(monthDiff).AddDays(dayDiff)
                .AddHours(hourDiff).AddMinutes(minuteDiff).AddSeconds(secondDiff);
            var dueDateStart = DateTimeOffset.Now.AddDays(14);
            var childDueDate = DateTimeOffset.Now.AddDays(7);
            var scheduledDate = DateTimeOffset.Now.AddDays(6);

            var obj = new CssTask(new LongIdentity(1), TaskName, TaskDescription,dueDateStart,scheduledDate
                , DateTimeOffset.Now,DateTimeOffset.Now,new List<SubTask>()
                {
                    new SubTask(new LongIdentity(2),TaskName,TaskDescription,childDueDate,childDueDate,DateTimeOffset.Now,DateTimeOffset.Now)
                });
            obj.SetDueDate(dueDateToSet);
            
            Assert.False(obj.IsValid());
        }

        [Fact]
        public void CanSetScheduleDate()
        {
            var dueDate = DateTimeOffset.Now.AddDays(2);
            var scheduledDate = DateTimeOffset.Now.AddDays(1);
            var obj = new CssTask(TaskName);
            obj.SetDueDate(dueDate);
            obj.SetScheduledDate(scheduledDate);
            
            Assert.Equal(dueDate,obj.DueDate);
            Assert.Equal(scheduledDate, obj.ScheduleDate);  //scheduled date is set as it's null.
            Assert.True(obj.IsValid());
        }
        
        [Theory]
        [InlineData("01/12/2020","01/11/2020")]
        [InlineData("01/12/2020","01/12/2020")]
        [InlineData("02/12/2020","01/12/2020")]
        public void ScheduleDateShouldBeValid(string dDate, string sDate)
        {
            var dueDate = DateTimeOffset.Parse(dDate);
            var scheduledDate = DateTimeOffset.Parse(sDate);
            var obj = new CssTask(TaskName);
            obj.SetDueDate(dueDate);
            obj.SetScheduledDate(scheduledDate);
            
            Assert.Equal(dueDate,obj.DueDate);
            Assert.Equal(scheduledDate, obj.ScheduleDate);  //scheduled date is set as it's null.
            Assert.True(obj.IsValid());
        }
        
        [Theory]
        [InlineData("01/12/2020","02/12/2020")]
        [InlineData("01/11/2020","01/12/2020")]
        public void ScheduleDateShouldBeInValid(string dDate, string sDate)
        {
            var dueDate = DateTimeOffset.Parse(dDate);
            var scheduledDate = DateTimeOffset.Parse(sDate);
            var obj = new CssTask(TaskName);
            obj.SetDueDate(dueDate);
            obj.SetScheduledDate(scheduledDate);
            
            //scheduled date is set as it's null.
            Assert.False(obj.IsValid());
        }
        
        

        [Fact]
        public void CanSetDescription()
        {
            var obj = new CssTask(TaskName);
            obj.SetDescription(TaskDescription);
            
            Assert.True(obj.IsValid());
            Assert.Equal(TaskDescription,obj.Description);
        }
        
        [Theory]
        [InlineData("<script>")]
        [InlineData("--script")]
        public void InvalidDescription(string description)
        {
            var obj = new CssTask(TaskName);
            obj.SetDescription(description);
            
            Assert.False(obj.IsValid());
        }
        
        
    }
}