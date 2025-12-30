using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);
// add projects and cloud-native backing services here

//Backing services
var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);
var catlogDb = postgres.AddDatabase("catalogdb");

var cache = builder.AddRedis("cache").WithRedisInsight().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);
var rabbitMq = builder.AddRabbitMQ("rabbitmq").WithManagementPlugin().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);

var keycock = builder.AddKeycloak("keycloak", 8070)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var ollama = builder.AddOllama("ollama", 11434)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent).WithOpenWebUI();

var llama = ollama.AddModel("llama3.2");


//Projects
var catalog = builder.AddProject<Projects.Catalog>("catalog")
    .WithReference(catlogDb)
    .WithReference(rabbitMq)
    .WithReference(llama)
    .WaitFor(catlogDb)
    .WaitFor(rabbitMq)  
    .WaitFor(llama);


var basket = builder.AddProject<Projects.Basket>("basket")
    .WithReference(cache)
    .WithReference(catalog)
    .WithReference(rabbitMq)
    .WithReference(keycock)
    .WaitFor(cache)
    .WaitFor(rabbitMq)
    .WaitFor(keycock);



var webapp = builder.AddProject<Projects.WebApp>("webapp")
    .WithExternalHttpEndpoints()
     .WithReference(cache)
    .WithReference(catalog)
    .WithReference(basket)
    .WaitFor(catlogDb)
    .WaitFor(basket);



builder.Build().Run();
