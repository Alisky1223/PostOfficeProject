using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;
using PostOfficeFrontendProject__all_interactive.Components;
using PostOfficeFrontendProject__all_interactive.Extention;
using PostOfficeFrontendProject__all_interactive.Provider;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// 🌍 تنظیم فرهنگ پیش‌فرض
var culture = new CultureInfo("fa-IR");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

// 🎨 سرویس‌های MudBlazor
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopLeft;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

// 🧩 سرویس‌های اختصاصی پروژه
builder.Services.AddRepositoryServices(builder.Configuration);
builder.Services.AddPolicies();

// ✅ ثبت CustomAuthStateProvider
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());


// ⚠️ این خط رو تغییر بده:
builder.Services.AddAuthorization(); // ← به جای AddAuthorizationCore()

// 🧱 Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// ⚙️ خط لوله‌ی درخواست‌ها
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ⚠️ باید بعد از Routing و قبل از MapRazorComponents باشد
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
