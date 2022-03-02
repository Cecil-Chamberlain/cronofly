using System.Collections.Generic;
using System.Net.Http;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Cronofly.AcceptanceTests
{
    public class Fixture : WebApplicationFactory<Startup>
    {
        public AmazonDynamoDBClient DynamoDB;

        private HttpClient _httpClient;
        public HttpClient HttpClient => _httpClient ??= CreateClient();

        public Fixture()
        {
            var credentials = new BasicAWSCredentials("localstack", "localstack");
            var dbConfig = new AmazonDynamoDBConfig
            {
                AuthenticationRegion = "eu-west-1",
                ServiceURL = "http://localhost:4566"
            };

            DynamoDB = new AmazonDynamoDBClient(credentials, dbConfig);
        }
    }
}
