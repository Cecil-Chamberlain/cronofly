using Microsoft.Extensions.Options;

namespace Data
{
    public class DbConfig : IOptions<DbConfig>
    {
        public string TableName { get; set; }
        public DbConfig Value => this;
    }
}
