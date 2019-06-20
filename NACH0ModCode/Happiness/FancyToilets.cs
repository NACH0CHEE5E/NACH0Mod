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
    class FancyToilets : IHappinessCause
    {
        public static Pandaros.Settlers.localization.LocalizationHelper LocalizationHelper { get; private set; } = new Pandaros.Settlers.localization.LocalizationHelper(Nach0Config.Name + ".Happiness");

        public float Evaluate(Colony colony)
        {
            var cs = ColonyState.GetColonyState(colony);
            if (colony != null && cs.ItemsInWorld.ContainsKey(Nach0ColonyBuiltIn.ItemTypes.TOILET.Id))
            {
                var toiletCount = cs.ItemsInWorld[Nach0ColonyBuiltIn.ItemTypes.TOILET.Id];
                if (colony.FollowerCount >= toiletCount * 10)
                {
                    return toiletCount * 3;
                }
                else
                {
                    return colony.FollowerCount * 3 / 10;
                }
                
            }
            else
            {
                return 0;
            }

        }

        public string GetDescription(Colony colony, Players.Player player)
        {
            return LocalizationHelper.LocalizeOrDefault("FancyToilets", player);
        }

    }

}
