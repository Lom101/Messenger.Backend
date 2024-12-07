using Microsoft.EntityFrameworkCore;

using Messenger.Backend.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MessengerDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Автоматическое применение миграций
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MessengerDbContext>();
    //dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.Run();