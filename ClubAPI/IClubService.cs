using System;
using System.Collections.Generic;
using ClubAPI.Models;

namespace ClubAPI.Services
{
    public interface IClubService
    {
        Club AddClub(string name);
        void AddMember(Guid clubId, int playerId);
        IEnumerable<Club> GetAllClubs();
        Club GetClubById(Guid clubId);
        Club GetClubByName(string name);
        IEnumerable<int> GetMembersByClub(Guid clubId);
    }
}