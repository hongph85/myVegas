namespace ClubAPI.DataAccess
{
    public interface IClubDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ClubsCollectionName { get; set; }
        string MembersCollectionName { get; set; }
    }

    public class ClubDBSettings : IClubDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ClubsCollectionName { get; set; } = null!;
        public string MembersCollectionName { get; set; } = null;
    }
}