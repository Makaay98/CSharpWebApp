namespace RareCrewTestCSharpWebApp.Services
{
    public class EmployeeService(HttpClient httpClient) : IEmployeeService
    {
        private readonly HttpClient httpClient = httpClient;

        public async Task<IEnumerable<Models.Employee>> GetEmployees()
        {
            string url = "api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            try
            {
                var employees = await this.httpClient.GetFromJsonAsync<IEnumerable<DTO.Employee>>(url);
                var calculatedAndMappedEmployees = this.GetEmployeesWithCalculatedWorkTimes(employees ?? []);
                return calculatedAndMappedEmployees;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Enumerable.Empty<Models.Employee>();
            }
        }


        private IEnumerable<Models.Employee> GetEmployeesWithCalculatedWorkTimes(IEnumerable<DTO.Employee> employees)
        {
            List<Models.Employee> appModelEmployees = new List<Models.Employee>();

            Dictionary<string, Models.Employee> employeesMap = new Dictionary<string, Models.Employee>();

            foreach (var employee in employees)
            {
                TimeSpan? difference = employee.EndTimeUtc - employee.StarTimeUtc;

                if (!difference.HasValue) { continue; }

                int hoursWorked = difference!.Value.Hours;

                if (employeesMap.ContainsKey(employee.EmployeeName ?? "N/A"))
                {
                    employeesMap[employee.EmployeeName ?? "N/A"].HoursWorked += hoursWorked;
                } else
                {
                    var employeeAppModel = new Models.Employee { 
                        Id = employee.Id, 
                        EmployeeName = employee.EmployeeName, 
                        HoursWorked = hoursWorked
                    };

                    employeesMap.Add(employee.EmployeeName ?? "N/A", employeeAppModel);
                }
            }

            double totalWorkedHours = employeesMap.Sum(x => x.Value.HoursWorked ?? 0);

            foreach (var item in employeesMap)
            {
                double procentage = 0;
                if(item.Value.HoursWorked != null)
                {
                    procentage = Math.Round(((double)item.Value.HoursWorked / totalWorkedHours) * 100);
                }
                 
                item.Value.Procentage = procentage;
                appModelEmployees.Add(item.Value);
            }


            return appModelEmployees;
        }
    }
}
