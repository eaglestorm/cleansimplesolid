using System;
using CleanConnect.Common.Model;
using CleanConnect.Common.Model.Errors;
using CleanDdd.Common.Model.Identity;

namespace CleanSimpleSolid.Core.Model.User
{
    /// <summary>
    /// An authenticated user.
    /// </summary>
    public class CssUser: Base<LongIdentity, long>, IValidator
    {
        /// <summary>
        /// Database identity.
        /// </summary>
        public LongIdentity Identity { get; }
        
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// External identifier for the user.  Should only change once.
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// We require all users to have an email.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// The id token contains a new user.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        public CssUser(string name, string subject, string email)
        {
            Name = name;
            Subject = subject;
            Email = email;
            Errors = new Validations();
        }

        /// <summary>
        /// User created from database record.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="createdDate"></param>
        /// <param name="modifiedDate"></param>
        public CssUser(LongIdentity identity,string name, string subject, string email, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
        :base(identity,createdDate,modifiedDate)
        {
            Name = name;
            Subject = subject;
            Email = email;
            Errors = new Validations();
        }

        public bool IsValid()
        {
            return Errors.Count == 0;
        }

        public Validations Errors { get; }

        public void SetName(string name)
        {
            if (RegexConstants.NameRegex.IsMatch(name))
            {
                Name = name;
            }
            else
            {
                Errors.AddError(ErrorCode.InvalidName, "The supplied name is not valid.");
            }
        }

        public void SetEmail(string email)
        {
            if (RegexConstants.EmailRegex.IsMatch(email))
            {
                Email = email;
            }
            else
            {
                Errors.AddError(ErrorCode.InvalidEmail, "The supplied email is not valid.");
            }
        }
    }
}