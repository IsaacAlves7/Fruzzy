using Fruzzy.Data;
using Fruzzy.Services.Abstractions;
using Fruzzy.Services.Ocr;
using Fruzzy.Services.Pdf;
using Fruzzy.Services.Export;
using Fruzzy.Services.Processing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Cors.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


// Serilog
builder.Host.UseSerilog((ctx, lc) => lc
.ReadFrom.Configuration(ctx.Configuration));


// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();


// Razor Pages
builder.Services.AddRazorPages();


// DI: Services
builder.Services.AddScoped<IOcrService, IronOcrService>();
builder.Services.AddScoped<AsposeOcrFallbackService>();


builder.Services.AddScoped<IPdfService, PdfPigService>();
builder.Services.AddScoped<IExcelExporter, EpplusExcelExporter>();
builder.Services.AddScoped<IDocumentProcessor, DocumentProcessor>();


builder.Services.AddScoped<IOperationGate, OperationGate>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();