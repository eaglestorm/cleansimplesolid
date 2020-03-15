namespace CleanConnect.Common.Model.Errors
{
    public sealed class ErrorGroup
    {        
        public static readonly ErrorGroup General = new ErrorGroup("General","G");
        public static readonly ErrorGroup Configuration = new ErrorGroup("Configuration","C");
        
        /// <summary>
        /// Validation errors.  Should not be thrown as exceptions but returned as a list.
        /// </summary>
        public static readonly ErrorGroup Validation = new ErrorGroup("Validation","V");

        public ErrorGroup(
            string name,
            string code)
        {
            Name = name;
            Code = code;
        }
        
        /// <summary>
        /// Name of the error group.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Code for the error group.
        /// </summary>
        public string Code { get; }
    }
}