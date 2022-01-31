using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClubAPI.Models
{
    public class Member
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid ClubId { get; set; }
        [BsonId]
        public int PlayerId { get; internal set; }
    }
}