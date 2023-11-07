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
    public class BeneficiaryController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public IActionResult Get(int employeeId)
        {
            try
            {
                var business = new BusinessDao();
                var result = business.GetBeneficiary(employeeId);

                if (result == null)
                {
                    result = new List<BeneficiaryData>();
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
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Set")]
        [Authorize]
        public IActionResult Set(BeneficiaryData data)
        {
            try
            {
                var business = new BusinessDao();
                var result = business.SetBeneficiary(data);

                if (result == null)
                {
                    result = new List<BeneficiaryData>();
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

        [HttpDelete]
        [Route("Del")]
        [Authorize]
        public IActionResult Del(int beneficiaryId)
        {
            try
            {
                var business = new BusinessDao();
                business.DelBeneficiary(beneficiaryId);

                return Ok(true);
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