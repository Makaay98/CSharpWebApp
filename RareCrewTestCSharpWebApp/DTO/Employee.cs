namespace RareCrewTestCSharpWebApp.DTO
{
    public class Employee
    {
        public Guid? Id { get; set; }

        public string? EmployeeName { get; set; }

        public DateTime? StarTimeUtc { get; set; }

        public DateTime? EndTimeUtc { get; set; }
    }
}
