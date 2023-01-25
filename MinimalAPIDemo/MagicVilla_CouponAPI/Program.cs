using MagicVilla_CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

app.MapGet("/api/coupon", () => {
    return Results.Ok();
}).WithName("GetCoupons");

app.MapGet("/api/coupon/{id:int}", (int id) => {
    return Results.Ok(FirstOrDefault(u => u.Id == id);
}).WithName("GetCoupon");

app.MapPost("/api/coupon", ([FromBody] Coupon coupon) => {
    if(coupon.Id != 0 || string.IsNullOrEmpty(coupon.Name))
    {
        return Results.BadRequest("Invalis Id or Coupon name");
    }
    if(CouponStore.)
});

app.UseHttpsRedirection();

app.Run();
