using ExampleApp.Shared;
using ExampleApp.Shared.Models;

namespace ExampleApp.Client.Data
{
    public class AnnouncementService : BaseConnectorService<Announcement>, IAnnouncementService
    {
        public AnnouncementService(IHttpService httpClient) : base(httpClient)
        {

        }

        public override string BASE_URL => "api/announcement";
    }
}