namespace CleanConnect.Common.Model
{
    public interface ISecurable
    {
        bool CanRead();
        bool CanUpdate();
        bool CanCreate();
        bool CanDelete();
    }
}