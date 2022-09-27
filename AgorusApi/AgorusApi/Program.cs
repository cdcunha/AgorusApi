using AgorusApi.Context;
using AgorusApi.Context.Helper;
using AgorusApi.Dto;
using AgorusApi.Profiles;
using AgorusService.Repositories;
using AgorusService.Repositories.Interfaces;
using AgorusService.Services;
using AgorusService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(FileProfile));
builder.Services.AddControllersWithViews();

builder.Services.Configure<DbConfigOptions>(builder.Configuration.GetSection(DbConfigOptions.DbConfig));
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IFileHistoryRepository, FileHistoryRepository>();

builder.Services.AddControllers();
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

app.MapControllers();
//Creating endpoints

// GET: api/File
app.MapGet("api/File", async (IFileService fileService) =>
    await fileService.ReadAllWithDetails()
    is IEnumerable<FileDto> files ? Results.Ok(files) : Results.NotFound());

// GET: api/File/5
app.MapGet("api/File/{id}", async (int id, IFileService fileService) =>
    await fileService.ReadByIdWithDetail(id)
    is FileDto file ? Results.Ok(file) : Results.NotFound($"Id {id} not found"));

// DELETE: api/File/5
app.MapDelete("api/File/{id}", async (int id, IFileService fileService) => 
{
    var (IsSuccess, Message) = await fileService.Delete(id);
    if (IsSuccess)
        return Results.NoContent();
    else
        return Results.NotFound(Message);
});

// DELETE: api/File/5/History/1
app.MapDelete("api/File/{id}/History/{historyId}", async (int id, int historyId, IFileService fileService) => 
{
    var (IsSuccess, Message) = await fileService.DeleteHistory(id, historyId);
    if (IsSuccess)
    {
        return Results.NoContent();
    }
    else
        return Results.NotFound(Message);
});

app.Run();