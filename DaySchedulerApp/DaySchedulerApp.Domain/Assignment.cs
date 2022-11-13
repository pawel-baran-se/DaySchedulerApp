using DaySchedulerApp.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Domain
{
    public class Assignment : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int FrequencyInDays { get; set; }
        public bool SendNotification { get; set; }
        public DateTime? LatestCompletion { get; set; }
    }
}
