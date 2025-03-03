using PhoneBook.Repositories;
using PhoneBook.src.Services;
using PhoneBook.src.Functions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var contactRepo = services.GetRequiredService<IContactRepository>();
    var categoryRepo = services.GetRequiredService<ICategoryRepository>();

    await DbSeeder.SeedAsync(contactRepo, categoryRepo);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
    options
    .WithOrigins(new[] {"http://localhost:4200"}) 
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.UseAuthorization();
app.MapControllers();
app.Run();
