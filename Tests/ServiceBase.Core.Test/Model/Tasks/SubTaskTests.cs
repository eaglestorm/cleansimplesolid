using System;
using System.Collections.Generic;
using CleanDdd.Common.Model.Identity;
using CleanSimpleSolid.Core.Model.Tasks;
using Xunit;

namespace ServiceBase.Core.Test.Model.Context
{
    public class SubTaskTests
    {
        private const string TaskName = "Task1";
        private const string TaskDescription = "This is a task description to descrive the task.";
        
        [Theory]
        [InlineData("<script>")]
        [InlineData("--script")]
        public void InvalidNameShouldFail(string name)
        {
            var obj = new SubTask(name);
            
            Assert.False(obj.IsValid());
        }
        
        [Fact]
        public void CanCreateNew()
        {
            var obj = new SubTask(TaskName );
            
            Assert.Equal(TaskName,obj.Name);
            Assert.True(obj.IsValid());
        }

        [Fact]
        public void CanCreateFull()
        {
            var dueDate = DateTimeOffset.Now.AddDays(1);
            var scheduledDate = DateTimeOffset.Now;
            var obj = new SubTask(new LongIdentity(1), TaskName, TaskDescription,dueDate,
                scheduledDate, DateTimeOffset.Now,DateTimeOffset.Now);
            
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
            var obj = new SubTask(TaskName);
            obj.SetDueDate(dueDate);
            
            Assert.Equal(dueDate,obj.DueDate);
            Assert.Equal(dueDate, obj.ScheduleDate);  //scheduled date is set as it's null.
            Assert.True(obj.IsValid());
        }
        
        [Fact]
        public void CanSetDueDateNull()
        {
            var obj = new SubTask(TaskName);
            obj.SetDueDate(null);
            
            Assert.False(obj.DueDate.HasValue);
            Assert.False(obj.ScheduleDate.HasValue);  //scheduled date is set as it's null.
            Assert.True(obj.IsValid());
        }
        

        [Fact]
        public void CanSetScheduleDate()
        {
            var dueDate = DateTimeOffset.Now.AddDays(2);
            var scheduledDate = DateTimeOffset.Now.AddDays(1);
            var obj = new SubTask(TaskName);
            obj.SetDueDate(dueDate);
            obj.SetScheduledDate(scheduledDate);
            
            Assert.Equal(dueDate,obj.DueDate);
            Assert.Equal(scheduledDate, obj.ScheduleDate);  //scheduled date is set as it's null.
            Assert.True(obj.IsValid());
        }
        
        [Fact]
        public void CanSetScheduleDateNull()
        {
            var dueDate = DateTimeOffset.Now.AddDays(2);
            
            var obj = new SubTask(TaskName);
            obj.SetDueDate(dueDate);
            obj.SetScheduledDate(null);
            
            Assert.Equal(dueDate,obj.DueDate);
            Assert.False(obj.ScheduleDate.HasValue);  //scheduled date is set as it's null.
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
            var obj = new SubTask(TaskName);
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
            var obj = new SubTask(TaskName);
            obj.SetDueDate(dueDate);
            obj.SetScheduledDate(scheduledDate);
            
            //scheduled date is set as it's null.
            Assert.False(obj.IsValid());
        }
        
        

        [Fact]
        public void CanSetDescription()
        {
            var obj = new SubTask(TaskName);
            obj.SetDescription(TaskDescription);
            
            Assert.True(obj.IsValid());
            Assert.Equal(TaskDescription,obj.Description);
        }
        
        [Theory]
        [InlineData("<script>")]
        [InlineData("--script")]
        public void InvalidDescription(string description)
        {
            var obj = new SubTask(TaskName);
            obj.SetDescription(description);
            
            Assert.False(obj.IsValid());
        }
        
        [Theory]
        [InlineData("description")]
        [InlineData("This is my description !@#$%^&*()")]
        [InlineData(null)]
        public void ValidDescription(string description)
        {
            var obj = new SubTask(TaskName);
            obj.SetDescription(description);
            
            Assert.True(obj.IsValid());
        }
    }
}