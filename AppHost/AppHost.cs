var builder = DistributedApplication.CreateBuilder(args);
// add projects and cloud-native backing services here

//Backing services
var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);
var catlogDb = postgres.AddDatabase("catalogdb");

//Projects
builder.AddProject<Projects.Catalog>("catalog").WithReference(catlogDb).WaitFor(catlogDb);


builder.AddProject<Projects.Basket>("basket");


builder.Build().Run();
