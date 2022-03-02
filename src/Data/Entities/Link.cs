using System;
using Amazon.DynamoDBv2.DataModel;

namespace Data.Entities
{
    public class Link
    {
        [DynamoDBHashKey]
        public string ShortLinkId { get; set; }
        public string RedirectUrl { get; set; }

        public static Link Create(string redirectUrl)
        {
            return new Link(redirectUrl);
        }

        private Link(string redirectUrl)
        {
            ShortLinkId = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            RedirectUrl = redirectUrl;
        }

        public Link(){} // For benefit of DynamoDB
    }
}
