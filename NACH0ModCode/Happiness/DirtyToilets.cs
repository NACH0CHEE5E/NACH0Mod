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
    class DirtyToilets : IHappinessCause
    {
        public static Pandaros.Settlers.localization.LocalizationHelper LocalizationHelper { get; private set; } = new Pandaros.Settlers.localization.LocalizationHelper(Nach0Config.Name + ".Happiness");

        public float Evaluate(Colony colony)
        {
            ServerLog.LogAsyncMessage(new LogMessage("<color=blue>1</color>", UnityEngine.LogType.Log));
            if (colony != null && colony.FollowerCount > 15 && RoamingJobManager.Objectives.TryGetValue(colony, out var toiletLocations))
            {
                ServerLog.LogAsyncMessage(new LogMessage("<color=blue>2</color>", UnityEngine.LogType.Log));
                var DirtyToiletCount = 0;
                var toilets = toiletLocations.Where(s => s.Value?.RoamingJobSettings?.ObjectiveCategory == "toilet").Select(s => s.Value).ToList();
                foreach (var toilet in toilets)
                {
                    if (toilet.ActionEnergy.TryGetValue(ToiletConstants.CLEAN, out var levelOfClean))
                    {
                        //var levelOfClean = toilet.ActionEnergy[ToiletConstants.CLEAN];
                        if (levelOfClean <= 0.45f)
                        {
                            DirtyToiletCount++;
                        }
                    }
                }
                int DirtyToilets = DirtyToiletCount * -2;
                return DirtyToilets;
            }
            else
            {
                ServerLog.LogAsyncMessage(new LogMessage("<color=blue>3</color>", UnityEngine.LogType.Log));
                return 0;
            }

        }

        public string GetDescription(Colony colony, Players.Player player)
        {
            return LocalizationHelper.LocalizeOrDefault("DirtyToilets", player);
        }

    }

}
