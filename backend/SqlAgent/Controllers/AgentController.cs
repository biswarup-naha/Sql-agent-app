using System.Security.Claims;
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
        private readonly SqlService _sql;
        private readonly SchemaService _schemaService;

        public AgentController(AgentService agent, SqlService sql, SchemaService schemaService)
        {
            _agent = agent;
            _sql = sql;
            _schemaService = schemaService;
        }

        [HttpPost("ask")]
        [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
        public async Task<ApiResponse<string>> AskSqlAgent([FromBody] PromptRequest request)
        {
            var schema = await _schemaService.GetDatabaseSchema();

            var question = request.Prompt;

            var prompt = $@"
                            You are a SQL Server expert.

                            Database Schema:
                            {schema}

                            Rules:
                            - Only generate SELECT queries
                            - Do not explain anything
                            - Do not use markdown
                            - If there is any question where data type need conversion from NVARCHAR to INT etc then convert that
                            - Only return SQL

                            User Question:
                            {request.Prompt}
                            ";

            var res = new ApiResponse<string>();
            try
            {
                var aiResponse = await _agent.QueryAgent(prompt);


                var sqlQuery = SqlCleaner.ExtractSql(aiResponse);

                //Security : before executing, validate:
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                SqlValidator.Validate(sqlQuery, role);


                var result = await _sql.ExecuteQuery(sqlQuery);

                var finalPrompt = $@"
                                    You are explaining database query results.

                                    User Question:
                                    {question}

                                    SQL Query Executed:
                                    {sqlQuery}

                                    Database Response (JSON):
                                    {result}

                                    If the command created/updated/deleted data, explain that the operation succeeded.
                                    If rows are returned, summarize the data in simple English.";

                res.Result = await _agent.QueryAgent(finalPrompt);
                res.Message = "Query successfully processed";
                res.Status = true;
            }
            catch (Exception ex)
            {
                res.Status = false;
                res.Message = ex.Message;
            }
            return res;
        }

        //[HttpPost]
        ////[Authorize(Roles = "USER, ADMIN")]
        //public async Task<ApiResponse<string>> AskAgent([FromBody] PromptRequest req)
        //{
        //    string query = req.Prompt;
        //    var res = new ApiResponse<string>();
        //    try
        //    {
        //        //var ans = await _agent.QueryAgent(query);
        //        //if (User.IsInRole("SUPERADMIN"))
        //        //{
        //        res.Result = await _agent.QueryAgent(query); ;
        //        res.Message = "Query successfully processed";
        //        res.Status = true;
        //        //}
        //        //res.Status = true;
        //        //res.Message = "FAQ statistics retrieved successfully";
        //        //res.Result = stats;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Status = false;
        //        res.Message = ex.Message;
        //    }
        //    return res;
        //}
    }

    public record PromptRequest(string Prompt);
}
