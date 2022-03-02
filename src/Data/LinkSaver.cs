using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Data.Entities;
using Microsoft.Extensions.Options;

namespace Data
{
    public class LinkSaver : ILinkSaver
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly DynamoDBOperationConfig _config;

        public LinkSaver(IDynamoDBContext dynamoDbContext, IOptions<DbConfig> options)
        {
            _dynamoDbContext = dynamoDbContext;
            _config = new DynamoDBOperationConfig
            {
                OverrideTableName = options.Value.TableName
            };
        }

        public Task SaveAsync(Link link)
        {
            return _dynamoDbContext.SaveAsync(link, _config);
        }
    }
}
