using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using ApplicationLog.Models;
using ApplicationLog.Data;

namespace ApplicationLog.Controllers
{
    [Route("api/[controller]"), Produces("application/json"), EnableCors("CorePolicy")]
    [ApiController]
    public class logController : ControllerBase
    {
        #region Database Operation
        // POST: api/log/setLogData
        [HttpPost("[action]")]
        public async Task<object> setLogData([FromBody] tbllog data)
        {
            object resdata = null;
            try
            {
                resdata = DataAccess.saveData(data);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }

        // GET: api/log/getLogData
        [HttpGet("[action]")]
        public async Task<object> getLogData()
        {
            object resdata = null;
            try
            {
                resdata = DataAccess.getLogData();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }

        #endregion
    }
}