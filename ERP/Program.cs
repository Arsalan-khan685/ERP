using ERP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ISectorService, SectorService>();
builder.Services.AddSingleton<IBlockService, BlockService>();
builder.Services.AddSingleton<IStreetService, StreetService>();
builder.Services.AddSingleton<IPlotTypeService, PlotTypeService>();
builder.Services.AddSingleton<IPlotSizeService, PlotSizeService>();
builder.Services.AddSingleton<IPlotService, PlotService>();
builder.Services.AddSingleton<IBankService, BankService>();
builder.Services.AddSingleton<IRelationService, RelationService>();
builder.Services.AddSingleton<IInstallmentTypeService,InstallmentTypeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
