using System.Text.RegularExpressions;

namespace CleanConnect.Common.Model
{
    /// <summary>
    /// Validation regexs.
    /// </summary>
    /// <remarks>
    /// TODO: enable this to be configured.
    /// TODO: typesafe enum?
    /// </remarks>
    public static class RegexConstants
    {
        private const string EmailPattern = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
        public static readonly Regex EmailRegex = new Regex(EmailPattern);

        public const           string NamePattern = @"^[\w\s]*$";
        public static readonly Regex  NameRegex   = new Regex(NamePattern);

        private const           string UsernamePattern = @"^[\w\!@#$%^&\.\-\+]{8,25}$";
        public static readonly Regex  UsernameRegex   = new Regex(UsernamePattern);

        private const           string DescriptionPattern = @"^[\w\s!@#$%&*^():;""'{}?\/\\]*$";
        public static readonly Regex  DescriptionRegex   = new Regex(DescriptionPattern);

        private const           string PasswordPattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\-!$%^&*()_+|~=`{}\[\]:"";'<>?,.\/]).{6,24}$";
        public static readonly Regex  PasswordRegex   = new Regex(PasswordPattern);

        private const string PhonePattern = @"^[0-9\+]{8,14}$";
        public static readonly  Regex PhoneRegex = new Regex(PhonePattern);

        public const string UriPattern = @"^[\w\s:\/\.]*$";
        public static readonly  Regex UriRegex = new Regex(UriPattern);

        public const string UuidPattern = @"^[\w\s-]*$";
        public static readonly Regex UuidRegex = new Regex(UuidPattern);
        
        

    }
}