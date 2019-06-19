using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Happiness;
using Pipliz;
using Shared;
using NACH0.Models;
using Chatting;
using Pandaros.Settlers.Entities;

namespace NACH0.Happiness
{
    class Toilets : IHappinessCause
    {
        public static Dictionary<Colony, int> toiletCount_Dict = new Dictionary<Colony, int>();
        public static Pandaros.Settlers.localization.LocalizationHelper LocalizationHelper { get; private set; } = new Pandaros.Settlers.localization.LocalizationHelper(Nach0Config.Name + ".Happiness");

        public float Evaluate(Colony colony)
        {
            if (colony != null)
            {
                var cs = ColonyState.GetColonyState(colony);
                if (cs.ItemsInWorld.ContainsKey(Nach0ColonyBuiltIn.ItemTypes.TOILETHOLE.Id))
                {
                    var toiletCount = cs.ItemsInWorld[Nach0ColonyBuiltIn.ItemTypes.TOILETHOLE.Id];
                    int toiletsSupplied = toiletCount * 10;
                    int toiletsNeeded = colony.FollowerCount;

                    if (toiletsSupplied <= toiletsNeeded)
                    {
                        int happinessAmount = toiletsSupplied - toiletsNeeded;
                        return happinessAmount;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return colony.FollowerCount * -1;
                }
            }
            else
            {
                return 0;
            }

        }

        public string GetDescription(Colony colony, Players.Player player)
        {
            return LocalizationHelper.LocalizeOrDefault("NotEnoughToilets", player);
        }

    }

    [ChatCommandAutoLoader]
    public class Command : IChatCommand
    {
        public bool TryDoCommand(Players.Player player, string chat, List<string> splits)
        {
            var ps = PlayerState.GetPlayerState(player);
            var cs = ColonyState.GetColonyState(ps.Player.ActiveColony);
            int toiletCount;
            if (cs.ItemsInWorld.ContainsKey(Nach0ColonyBuiltIn.ItemTypes.TOILETHOLE.Id))
            {
                toiletCount = cs.ItemsInWorld[Nach0ColonyBuiltIn.ItemTypes.TOILETHOLE.Id];
            }
            else
            {
                toiletCount = 0;
            }
            if (player == null)
            {
                return false;
            }

            if (!chat.Equals("?nach0toilets"))
            {
                return false;
            }


            Chat.Send(player, "<color=blue>Number of toilets: " + toiletCount + "</color>");
            return true;
        }
    }
}
