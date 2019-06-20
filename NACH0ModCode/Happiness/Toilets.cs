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
using Pandaros.Settlers.Jobs.Roaming;

using NACH0.Items.Toilets;

namespace NACH0.Happiness
{
    class Toilets : IHappinessCause
    {
        //public static Dictionary<Colony, int> toiletCount_Dict = new Dictionary<Colony, int>();
        public static Pandaros.Settlers.localization.LocalizationHelper LocalizationHelper { get; private set; } = new Pandaros.Settlers.localization.LocalizationHelper(Nach0Config.Name + ".Happiness");

        public float Evaluate(Colony colony)
        {
            if (colony != null && colony.FollowerCount > 15)
            {
                    //toiletCount = RoamingJobManager.Objectives[colony].Select(s => s.Value.RoamObjective == "toilet").ToList();
                    var toiletCount = 0;
                    //ServerLog.LogAsyncMessage(new LogMessage("<color=blue>Number of toilets: " + toiletCount + "    (before foreach)</color>", UnityEngine.LogType.Log));
                    var toilets = RoamingJobManager.Objectives[colony].Where(s => s.Value.RoamingJobSettings.ObjectiveCategory == "toilet").Select(s => s.Value).ToList();
                    foreach (var toilet in toilets)
                    {
                        //ServerLog.LogAsyncMessage(new LogMessage("<color=purple>The foreach runs</color>", UnityEngine.LogType.Log));
                        var levelOfClean = toilet.ActionEnergy[ToiletConstants.CLEAN];
                        if (levelOfClean > 0.50f)
                        {
                            toiletCount ++;
                            //ServerLog.LogAsyncMessage(new LogMessage("<color=blue>Number of toilets: " + toiletCount + "</color>", UnityEngine.LogType.Log));
                        }

                    }
                    //ServerLog.LogAsyncMessage(new LogMessage("<color=blue>Number of toilets: " + toiletCount + "</color>", UnityEngine.LogType.Log));
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
                return 0;
            }

        }

        public string GetDescription(Colony colony, Players.Player player)
        {
            return LocalizationHelper.LocalizeOrDefault("NotEnoughToilets", player);
        }

    }

    [ChatCommandAutoLoader]
    public class SeeToiletsCommand : IChatCommand
    {
        public bool TryDoCommand(Players.Player player, string chat, List<string> splits)
        {
            if (!chat.Equals("?nach0toilets"))
            {
                return false;
            }
            if (player == null)
            {
                return false;
            }
            var ps = PlayerState.GetPlayerState(player);
            var toiletCount = 0;
            //ServerLog.LogAsyncMessage(new LogMessage("<color=blue>Number of toilets: " + toiletCount + "    (before foreach)</color>", UnityEngine.LogType.Log));
            var toilets = RoamingJobManager.Objectives[ps.Player.ActiveColony].Where(s => s.Value.RoamingJobSettings.ObjectiveCategory == "toilet").Select(s => s.Value).ToList();
            foreach (var toilet in toilets)
            {
                //ServerLog.LogAsyncMessage(new LogMessage("<color=purple>The foreach runs</color>", UnityEngine.LogType.Log));
                var levelOfClean = toilet.ActionEnergy[ToiletConstants.CLEAN];
                if (levelOfClean > 0.60f)
                {
                    toiletCount++;
                    //ServerLog.LogAsyncMessage(new LogMessage("<color=blue>Number of toilets: " + toiletCount + "</color>", UnityEngine.LogType.Log));
                }
            }
            Chat.Send(player, "<color=blue>Number of toilets: " + toiletCount + "</color>");
            return true;
        }
    }
    /*[ChatCommandAutoLoader]
    public class DisableToiletsCommand : IChatCommand
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

            if (!chat.Equals("?nach0toiletsoff"))
            {
                return false;
            }


            Chat.Send(player, "<color=blue>Number of toilets: " + toiletCount + "</color>");
            return true;
        }
    }*/
}
