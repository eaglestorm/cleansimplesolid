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
        // Max lengths based on database.
        public const int NameMaxLength = 255;
        public const int SubjectMaxLength = 255;
        public const int EmailMaxLength = 100;
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
        public string Subject { get; private set; }

        /// <summary>
        /// We require all users to have an email.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// The id token contains a new user.  should be used for new users only
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        public CssUser(string name, string subject, string email)
        {
            Id = new LongIdentity();
            Errors = new Validations();
            SetName(name);
            SetSubject(subject);
            SetEmail(email);
        }

        /// <summary>
        /// User created from database record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="createdDate"></param>
        /// <param name="modifiedDate"></param>
        public CssUser(LongIdentity id,string name, string subject, string email, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
        :base(id,createdDate,modifiedDate)
        {
            Errors = new Validations();
            SetName(name);
            SetSubject(subject);
            SetEmail(email);
        }

        public bool IsValid()
        {
            return Errors.Count == 0;
        }

        public Validations Errors { get; }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Errors.AddError(ErrorCode.ValueRequired, "The name is required");
                return;
            }
            if (RegexConstants.NameRegex.IsMatch(name) && name.Length <= NameMaxLength)
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
            if (string.IsNullOrEmpty(email))
            {
                Errors.AddError(ErrorCode.ValueRequired, "The name is required");
                return;
            }
            if (RegexConstants.EmailRegex.IsMatch(email) && email.Length <= EmailMaxLength)
            {
                Email = email;
            }
            else
            {
                Errors.AddError(ErrorCode.InvalidEmail, "The supplied email is not valid.");
            }
        }

        private void SetSubject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
            {
                Errors.AddError(ErrorCode.ValueRequired, "The subject is required.");
            }
            else
            {
                Subject = subject;
            }
        }
    }
}