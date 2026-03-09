using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlAgent.Services;
using SqlAgent.Utils;

namespace SqlAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly AgentService _agent;
        public AgentController(AgentService agent)
        {
            _agent = agent;
        }

        [HttpPost]
        //[Authorize(Roles = "USER, ADMIN")]
        public async Task<ApiResponse<string>> AskAgent([FromBody] PromptRequest req)
        {
            string query = req.Prompt;
            var res = new ApiResponse<string>();
            try
            {
                //var ans = await _agent.QueryAgent(query);
                //if (User.IsInRole("SUPERADMIN"))
                //{
                    res.Result = await _agent.QueryAgent(query); ;
                    res.Message = "Query successfully processed";
                    res.Status = true;
                //}
                //res.Status = true;
                //res.Message = "FAQ statistics retrieved successfully";
                //res.Result = stats;
            }
            catch (Exception ex)
            {
                res.Status = false;
                res.Message = ex.Message;
            }
            return res;
        }
    }

    public record PromptRequest(string Prompt);
}
