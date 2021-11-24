using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher.Infrastructure.CacheKeys
{
    public static class PlayerCacheKeys
    {
        public static string ListKey => "PlayerList";

        public static string SelectListKey => "PlayerSelectList";

        public static string GetKey(int playerId) => $"Player-{playerId}";

        public static string GetDetailsKey(int playerId) => $"PlayerDetails-{playerId}";
    }
}
