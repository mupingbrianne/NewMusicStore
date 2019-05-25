using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NewMusicStore.Models
{
    public class SportsMusic
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("Title")]
        [MaxLength(50, ErrorMessage = "Max Title lenght is 50 Charaters")]
        public string Title { get; set; }
        [BsonElement("SingerName")]
        [MaxLength(40, ErrorMessage = "Max Title lenght is 40 Charaters")]
        public string SingerName { get; set; }
        [BsonElement("PublishDate")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LoginDate { get; set; }
    }
}
