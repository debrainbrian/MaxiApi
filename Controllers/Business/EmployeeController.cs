namespace MaxiApi.Controllers.Business
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Reflection;
    using MaxiApi.Error;
    using MaxiApi.DataAccess;
    using MaxiApi.Models.Business;

    /// <summary>
    /// The employee controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// Get the employee data.
        /// </summary>
        /// <returns>The employee data.</returns>
        [HttpPost]
        [Route("Post")]
        [Authorize]
        public IActionResult Post(EmployeeData data)
        {
            try
            {
                var business = new BusinessDao();
                var result = business.GetEmployee(data);

                if (result == null)
                {
                    result = new List<EmployeeData>();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new Log();
                var id = error.LogError(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                return BadRequest(id);
            }
        }

        /// <summary>
        /// Set the employee data.
        /// </summary>
        /// <param name="data">The employe data.</param>
        /// <returns>If the employee data was saved.</returns>
        [HttpPost]
        [Route("Set")]
        [Authorize]
        public IActionResult Set(EmployeeData data)
        {
            try
            {
                var business = new BusinessDao();
                var result = business.SetEmployee(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new Log();
                var id = error.LogError(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                return BadRequest(id);
            }
        }

        /// <summary>
        /// Delete the employee.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <returns>If the employe was deleted.</returns>
        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public IActionResult Delete(int employeeId)
        {
            try
            {
                var business = new BusinessDao();
                var result = business.DelEmployee(employeeId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new Log();
                var id = error.LogError(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                return BadRequest(id);
            }
        }
    }
}