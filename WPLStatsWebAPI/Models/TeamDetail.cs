using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XWPLStatsWebAPI.Models
{
    public class TeamDetails
    {
        public int Id { get; set; }
        public int TeamNumber { get; set; }
        public string TeamName { get; set; }
        public string Captain { get; set; }
    }
}