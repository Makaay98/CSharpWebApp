namespace RareCrewTestCSharpWebApp.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<RareCrewTestCSharpWebApp.Models.Employee>> GetEmployees();
    }
}
