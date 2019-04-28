using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Location
    {
        public String buildingId { set; get; }
        public String buildingName { set; get; }
        public String roomId { set; get; }
        public String roomName { set; get; }

        public Location(string buildingId, string buildingName, string roomId, string roomName)
        {
            this.buildingId = buildingId;
            this.buildingName = buildingName;
            this.roomId = roomId;
            this.roomName = roomName;
        }

        public Location(string buildingId, string buildingName)
        {
            this.buildingId = buildingId;
            this.buildingName = buildingName;
        }
    }
}