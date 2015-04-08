using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Angular4DotNetMvc.Controllers
{
    public class AccountController : ApiController
    {
        public HttpResponseMessage Post(HttpRequestMessage request, StudentVm student)
        {
            if (ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);    
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
