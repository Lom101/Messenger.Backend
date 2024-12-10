using Microsoft.EntityFrameworkCore;
using Messenger.Backend.Data;
using Messenger.Backend.Repository.Common;
using Messenger.Backend.Repository.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MessengerDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();