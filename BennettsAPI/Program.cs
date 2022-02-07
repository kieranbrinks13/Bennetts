using BennettsAPI;
using BennettsData;
using BennettsDataModels;
using System.Data.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/users", async () =>
{
    using (var context = new BennettsDbContext())
    {
        return Results.Ok(await context.Users.ToListAsync());
    }
})
.WithName("GetUsers");


app.MapPost("/newuser", [ValidateModel] async (UserDM model) =>
{
    using (var context = new BennettsDbContext())
    {
        context.Users.Add(model);
        await context.SaveChangesAsync();

        return Results.Ok(model);
    }
})
.WithName("AddUser");

app.MapDelete("/deleteuser/{id}", async (Guid id) =>
{
    using (var context = new BennettsDbContext())
    {
        var user = new UserDM { UserId = id };
        context.Users.Attach(user);
        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return Results.Ok();
    }
})
.WithName("DeleteUser");

app.Run();