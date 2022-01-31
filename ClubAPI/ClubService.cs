using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubAPI.DataAccess;
using ClubAPI.Models;
using MongoDB.Driver;

namespace ClubAPI.Services
{
    public class ClubService
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
            return _clubs.Find<Club>(x => x.Name == name).FirstOrDefault();
        }

        public Club AddClub(string name)
        {
            var club = new Club(name);
            _clubs.InsertOne(club);
            return club;
        }

        internal Club GetClubById(Guid clubId)
        {
            return _clubs.Find<Club>(x => x.ClubId == clubId).FirstOrDefault();
        }

        internal IEnumerable<int> GetMembersByClub(Guid clubId)
        {
            return _members.Find<Member>(x => x.ClubId == clubId).ToList().Select(x => x.PlayerId);
        }

        internal void AddMember(Guid clubId, int playerId)
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