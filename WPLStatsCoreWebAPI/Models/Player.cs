using System;
using System.Collections.Generic;

namespace WPLStatsCoreWebAPI.Models;

public partial class Player
{
    public int EntryId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int GamesWon { get; set; }

    public int GamesLost { get; set; }

    public int WeekNumber { get; set; }
}
