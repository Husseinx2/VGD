using System;

namespace Capstone.Models
{
    public class GameListEntry
    {
        public int GameId { get; set; }

        public int UserId { get; set; }

        public string GameListType { get; set; }

        public DateTime GameListDatetime { get; set; }
    }
}
