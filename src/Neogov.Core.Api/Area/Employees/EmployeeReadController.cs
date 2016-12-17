using System.Web.Http;
using Neogov.Core.Api.Area.Employees.Views;

namespace Neogov.Core.Api.Area.Employees
{
    [RoutePrefix("api/employee")]
    public class EmployeeReadController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public EmployeeView Get()
        {
            return new EmployeeView
            {
                Id = 1,
                FirstName = "Ashish",
                LastName = "Srivastava"
            };
        }
    }
}