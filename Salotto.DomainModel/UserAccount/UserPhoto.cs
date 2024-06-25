using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salotto.DomainModel.UserAccount
{
    public class UserPhoto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public long UserId { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }

    }
}
