using CSharpApp.Application.Services;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDefaultConfiguration();

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

//app.UseHttpsRedirection();

app.MapGet("/todos", async (ITodoService todoService) =>
    {
        var todos = await todoService.GetAllTodos();
        return todos;
    })
    .WithName("GetTodos")
    .WithOpenApi();

app.MapGet("/todos/{id}", async ([FromRoute] int id, ITodoService todoService) =>
    {
        var todos = await todoService.GetTodoById(id);
        return todos;
    })
    .WithName("GetTodosById")
.WithOpenApi();


app.MapGet("/posts", async (ITodoPostService todopostService) =>
{
    var todos = await todopostService.GetAllTodos();
    return todos;
})
    .WithName("GetPostTodos")
    .WithOpenApi();

app.MapGet("/posts/{id}", async ([FromRoute] int id, ITodoPostService todopostService) =>
{
    var todos = await todopostService.GetTodoById(id);
    return todos;
})
    .WithName("GetPostTodosById")
.WithOpenApi();

app.MapPost("/posts/{TodoRecord}", async (TodoRecord todoitem, ITodoPostService todopostService) =>
{
    TodoRecord todos = new(todoitem.UserId,todoitem.Id,todoitem.Title,true);
    
    
    //TodoRecord vals = (TodoRecord)todoitem;
    var todoos = await todopostService.postTodo(todoitem);
    //return todos;
    //var todoos = await todopostService.GetTodoById(todoitem.Id);
    return todoos;
})
    .WithName("postTodo")
    .WithOpenApi();

app.MapDelete("/posts/{id}", async ([FromRoute] int id, ITodoPostService todopostService) =>
{
    var todos = await todopostService.deleteTodoById(id);
    return todos;
})
    .WithName("deleteTodoById")
    .WithOpenApi();


//var todoItems = app.MapGroup("/todoitems");

//todoItems.MapGet("/", GetTodos);
////todoItems.MapGet("/complete", GetCompleteTodos);
////todoItems.MapGet("/{id}", GetTodosById);
//todoItems.MapPost("/", postTodoById);
////todoItems.MapPut("/{id}", UpdateTodo);
//todoItems.MapDelete("/{id}", deleteTodoById);


app.Run();