using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Data.Entities;
using Microsoft.Extensions.Options;

namespace Data
{
    public class LinkGetter : ILinkGetter
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly DynamoDBOperationConfig _config;

        public LinkGetter(IDynamoDBContext dynamoDbContext, IOptions<DbConfig> options)
        {
            _dynamoDbContext = dynamoDbContext;
            _config = new DynamoDBOperationConfig
            {
                OverrideTableName = options.Value.TableName
            };
        }

        public Task<Link> GetAsync(string shortLinkId)
        {
            return _dynamoDbContext.LoadAsync<Link>(shortLinkId, _config);
        }
    }
}
