using OwnedRepro;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddOwnedReproHandlers();
builder.Services.AddSingleton(typeof(Owned<>));

var host = builder.Build();
host.Run();
