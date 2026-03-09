using Microsoft.AspNetCore.Mvc;
using SqlAgent.Services;
using SqlAgent.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            // collect model validation errors
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => new ApiError(e.ErrorMessage))
                .ToList();

            var apiResponse = new ApiResponse<object>
            {
                Result = null,
                Status = false,
                Message = errors.FirstOrDefault()?.Message ?? "Validation failed",
                Errors = errors
            };

            return new BadRequestObjectResult(apiResponse);
        };
    });
builder.Services.AddHttpClient<AgentService>();
builder.Services.AddScoped<SqlService>();
builder.Services.AddScoped<SchemaService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
