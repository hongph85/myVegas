using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubAPI.DataAccess;
using ClubAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ClubAPI.Services
{
    public class ClubService : IClubService
    {
        private readonly IMongoCollection<Club> _clubs;
        private readonly IMongoCollection<Member> _members;
        public ClubService(IClubDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _clubs = database.GetCollection<Club>(settings.ClubsCollectionName);
            _members = database.GetCollection<Member>(settings.MembersCollectionName);
        }

        public Club GetClubByName(string name)
        {
            return _clubs.AsQueryable<Club>().Where(x => x.Name == name).FirstOrDefault();
        }

        public IEnumerable<Club> GetAllClubs()
        {
            var documents = _clubs.Find(new BsonDocument()).ToList();
            return documents;
        }

        public Club AddClub(string name)
        {
            var club = new Club(name);
            _clubs.InsertOne(club);
            return club;
        }

        public Club GetClubById(Guid clubId)
        {
            return _clubs.AsQueryable<Club>().Where(x => x.ClubId == clubId).FirstOrDefault();
        }

        public IEnumerable<int> GetMembersByClub(Guid clubId)
        {
            return _members.AsQueryable<Member>().Where(x => x.ClubId.CompareTo(clubId) == 0).ToList().Select(x => x.PlayerId);
        }

        public void AddMember(Guid clubId, int playerId)
        {
            var entity = new Member()
            {
                ClubId = clubId,
                PlayerId = playerId
            };

            _members.InsertOne(entity);
        }
    }
}