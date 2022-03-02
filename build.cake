#addin "Cake.Docker&version=1.1.1"
#addin "Cake.FileHelpers&version=5.0.0"
#addin nuget:?package=AWSSDK.DynamoDBv2&version=3.3.106.47&loaddependencies=true
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using System.Threading;

var target = Argument("target", "init");

var awsCredentials = new BasicAWSCredentials("localstack", "localstack");

Task("init")
    .Description("Brings up docker infrastructure")
    .Does(async () =>
    {
        DockerComposeUp(new DockerComposeUpSettings
        {
            DetachedMode = true,
            Files = new[] { "docker-compose.yml" },
            ProjectName = "cronofly",
            RemoveOrphans = true
        });

        Information("Creating table");

        await EnsureTable(new CreateTableRequest
        {
            TableName = "cronofly-table",
            AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition("ShortLinkId", ScalarAttributeType.S)
            },
            KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement("ShortLinkId", Amazon.DynamoDBv2.KeyType.HASH)
            },
            ProvisionedThroughput = new ProvisionedThroughput(5, 5)
        });

        Information("Done");
    });

Task("teardown")
    .Description("Tears down docker infrastructure")
    .Does(() =>
    {
        DockerComposeDown(new DockerComposeDownSettings
        {
          Files = new[] { "docker-compose.yml" },
          RemoveOrphans = true
        });
    });

async Task EnsureTable(CreateTableRequest request, TimeToLiveSpecification ttlSpec = null)
{
    using (var dynamo = new AmazonDynamoDBClient(awsCredentials, new AmazonDynamoDBConfig
    {
        ServiceURL = "http://localhost:4566",
        AuthenticationRegion = "eu-west-1"
    }))
    {
        try
        {
            await dynamo.DeleteTableAsync(request.TableName);
        }
        catch (Amazon.DynamoDBv2.Model.ResourceNotFoundException)
        {
        }
        await dynamo.CreateTableAsync(request);
    }
}

RunTarget(target);
