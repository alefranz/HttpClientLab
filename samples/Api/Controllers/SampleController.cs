using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly GitHubClient _gitHubClient;

        public SampleController(GitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return await _gitHubClient.GetSomething();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post()
        {
            var result = await _gitHubClient.PostSomething();
            return new StatusCodeResult((int)result);
        }
    }
}
