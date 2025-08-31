using Confluent.Kafka;
using Main.Extensions;
using Main.Setting;
using Share.Extentsions;
using Share.KafkaManager.ConsumerManager;
using Share.KafkaManager.ProducerManager;

var builder = WebApplication.CreateBuilder(args);

var appSetting = AppSetting.MapValue(builder.Configuration);
builder.Services.AddSingleton(typeof(AppSetting));

// Add services to the container.
builder.Services.AddKafkaManager();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();





var app = builder.Build();

app.InitKafkaConsumer(appSetting);
app.InitKafkaProducer(appSetting);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
