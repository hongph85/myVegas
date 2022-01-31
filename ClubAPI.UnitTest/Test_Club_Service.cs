using System;
using System.Collections.Generic;
using ClubAPI.DataAccess;
using ClubAPI.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace ClubAPI.UnitTest
{
    public class Test_Club_Service
    {
        private ClubDBSettings settings;
        private ClubService service;
        private readonly Guid clubId = new Guid("a10d0b11-545d-4180-a786-4508a88a9387");

        public Test_Club_Service()
        {
            settings = new ClubDBSettings()
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "ClubDB",
                ClubsCollectionName = "Clubs",
                MembersCollectionName = "Members"
            };

            //Act 
            service = new ClubService(settings);
        }

        [Fact]
        public void Test_Constructor()
        {
            //Assert 
            Assert.NotNull(service);
        }

        [Fact]
        public void Add_Club_Succeed()
        {
            var club = service.AddClub("Foo");
            Assert.Equal<string>(club.Name, "Foo");
        }

        [Fact]
        public void Get_Club_By_ClubId()
        {
            var club = service.GetClubById(clubId);
            Assert.Equal<string>(club.Name, "Foo");
        }

        [Fact]
        public void Get_Members_By_ClubId()
        {
            var ids = service.GetMembersByClub(clubId);
            Assert.Collection(ids,
                x => Assert.Equal(2, x));
        }

        [Fact]
        void Get_All_Clubs()
        {
            var clubs = service.GetAllClubs();
            foreach (var doc in clubs)
            {
                Console.WriteLine($"{doc.ClubId}: {doc.Name}");
            }

            Assert.NotEmpty(clubs);
        }

        [Fact]
        public void Add_Member_Succeed()
        {
            service.AddMember(clubId, 2);
        }
    }
}
