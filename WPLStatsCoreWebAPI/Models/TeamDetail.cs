using System;
using System.Collections.Generic;

namespace WPLStatsCoreWebAPI.Models;

public partial class TeamDetail
{
    public int Id { get; set; }

    public int TeamNumber { get; set; }

    public string TeamName { get; set; } = null!;

    public string Captain { get; set; } = null!;
}
