using RedisSample.Infra;
using RedisSample.Subscribers;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration = builder.Configuration;
var redisConnection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
builder.Services.AddSingleton<ICache, RedisCacheRepository>();
builder.Services.AddSingleton<IRedisSubscriber, RedisSubscriber>();




//var subscriber = redisConnection.GetSubscriber();

//subscriber.Subscribe("Product-Channel", (channel, message) =>
//{
//  Console.WriteLine($"data : {message}");
//});


builder.Services.AddHostedService<ProductSubscriber>();


// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
