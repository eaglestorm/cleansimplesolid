using System;
using System.Collections.Generic;
using CleanConnect.Common.Model.Errors;
using CleanDdd.Common.Model.Identity;

namespace CleanSimpleSolid.Core.Model.Tasks
{
    public class SubTask: TaskBase
    {

        public SubTask(string name)
        :base(name)
        {
            
        }
        
        public SubTask(LongIdentity identity, string name, string description, DateTimeOffset dueDate,
            DateTimeOffset scheduledDate, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
            :base(identity,name,description,dueDate,scheduledDate,createdDate,modifiedDate)
        {

        }
    }
}