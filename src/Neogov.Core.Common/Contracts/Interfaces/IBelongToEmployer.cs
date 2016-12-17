namespace Neogov.Core.Common.Interfaces
{
    public interface IBelongToEmployer
    {
        int EmployerId { get; }
        void SetEmployerID(int employerId);
    }
}