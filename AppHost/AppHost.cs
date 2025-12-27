var builder = DistributedApplication.CreateBuilder(args);
// add projects and cloud-native backing services here

//Backing services
var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);
var catlogDb = postgres.AddDatabase("catalogdb");

var cache = builder.AddRedis("cache").WithRedisInsight().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);

//Projects
builder.AddProject<Projects.Catalog>("catalog").WithReference(catlogDb).WaitFor(catlogDb);


builder.AddProject<Projects.Basket>("basket").WithReference(cache).WaitFor(cache); ;


builder.Build().Run();
