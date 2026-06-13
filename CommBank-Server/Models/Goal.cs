using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models
{
    public class Goal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal TargetAmount { get; set; }

        /// <summary>
        /// An optional public Icon field of string type
        /// </summary>
        [BsonElement("Icon")]
        public string? Icon { get; set; } 
    }
}
