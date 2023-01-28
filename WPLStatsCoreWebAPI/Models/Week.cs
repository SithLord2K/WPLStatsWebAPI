using System;
using System.Collections.Generic;

namespace WPLStatsCoreWebAPI.Models;

public partial class Week
{
    public int Id { get; set; }

    public int WeekNumber { get; set; }

    public bool? WeekWon { get; set; }

    public DateTime? DatePlayed { get; set; }

    public int? TeamPlayed { get; set; }

    public bool? Home { get; set; }
}
