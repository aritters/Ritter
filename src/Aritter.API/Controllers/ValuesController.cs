﻿using Aritter.API.Core.Attributes;
using Aritter.Application.DTO.Security;
using Aritter.Infra.CrossCutting.Security;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aritter.API.Controllers
{
    public class ValuesController : DefaultApiController
    {
        // GET api/values
        [Authorization("Feat1", Rule.Get)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new UserDTO { UserName = "Teste" });
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
