using System;
using System.Collections.Generic;

namespace SystemIO.Models
{
    internal class GameSettings
    {
        public string PlayerName { get; set; }
        public int Volume { get; set; }
        public bool IsFullscreen { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public List<string> FavoriteHeroes { get; set; }
    }
}
