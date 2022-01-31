using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClubAPI.Models
{
    public class Club
    {
        public Club()
        {
        }

        public Club(string name)
        {
            this.Name = name;
        }

        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid ClubId { get; set; }
        [BsonElement]
        public string Name { get; set; }
    }
}