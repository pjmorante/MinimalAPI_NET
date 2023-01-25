using MagicVilla_CouponAPI.Models;
using MagicVilla_CouponAPI.Models.DTO;
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

app.MapGet("/api/coupon", (ILogger<Program> _logger) => {
    _logger.Log(LogLevel.Information, "Getting all coupons");
    return Results.Ok();
}).WithName("GetCoupons").Produces<IEnumerable<Coupon>>(200);

//app.MapGet("/api/coupon/{id:int}", (int id) => {
//    return Results.Ok(FirstOrDefault(u => u.Id == id);
//}).WithName("GetCoupon").Produces<Coupon>(200);

app.MapPost("/api/coupon", ([FromBody] CouponCreateDTO coupon_C_DTO) =>
{
    if (coupon_C_DTO.Id != 0 || string.IsNullOrEmpty(coupon_C_DTO.Name))
    {
        return Results.BadRequest("Invalis Id or Coupon name");
    }
    if (CouponStore.couponList.FirstOrDefault(u => u.Name.ToLower() == coupon_C_DTO.Name.ToLower()))
    {
        return Results.BadRequest("Coupon Name already exists!");
    }

    Coupon coupon = new()
    {
        IsActive = coupon_C_DTO.IsActive,
        Name = coupon_C_DTO.Name,
        Percent = coupon_C_DTO.Percent
    };

    coupon.Id = CouponStore.couponList.OrderByDescending(u => u.Id).FirstOrDefault().Id;
    couponStore.couponList.Add(coupon);
    CouponDTO couponDTO = new()
    {
        Id = coupon.Id,
        IsActive = coupon.IsActive,
        Name = coupon.Name,
        Percent = coupon.Percent,
        Created = coupon.Created
    };
    return Results.CreatedAtRoute("GetCoupon", new { id = coupon.Id }, coupon);
}).WithName("CreateCoupon").Accepts<CouponCreateDTO>("application/json").Produces<Coupon>(201).Produces<Coupon>(400);

app.UseHttpsRedirection();

app.Run();
