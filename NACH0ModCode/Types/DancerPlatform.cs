using BlockTypes;
using Jobs;
using NPC;
using Pandaros.Settlers.Items;
using Pandaros.Settlers.Jobs.Roaming;
using Pandaros.Settlers.Models;
using Pipliz;
using Recipes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using NACH0.Models;
using Pandaros.Settlers;
using NACH0.Items.Dancer;

namespace NACH0.types
{
    public class DancerPlatformBlock : CSType
    {
        public override string name { get; set; } = Nach0Config.TypePrefix + "DancerPlatform";
        public override string icon { get; set; } = Nach0Config.ModIconFolder + "DancerPlatform.png";
        public override bool? isPlaceable { get; set; } = true;
        public override string onPlaceAudio { get; set; } = "stonePlace";
        public override string onRemoveAudio { get; set; } = "stoneDelete";
        public override bool? isSolid { get; set; } = true;
        public override string sideall { get; set; } = Nach0Config.TexturePrefix + "DancerPlatform";
        public override List<string> categories { get; set; } = new List<string>()
        {
            "essential",
            "dancing",
            "NACH0"
        };
    }
    public class DancerPlatformRegister : IRoamingJobObjective
    {
        public string name => "DancerPlatform";
        public float WorkTime => 5f;
        public ItemId ItemIndex => Nach0ColonyBuiltIn.ItemTypes.DANCERPLATFORM.Id;
        public Dictionary<string, IRoamingJobObjectiveAction> ActionCallbacks { get; } = new Dictionary<string, IRoamingJobObjectiveAction>()
        {
            { DancerConstants.DANCE, new DanceAtPlatform() }
        };

        public string ObjectiveCategory => "dancing";

        public void DoWork(Colony colony, RoamingJobState dancerState)
        {
            if ((!colony.OwnerIsOnline() && SettlersConfiguration.OfflineColonies) || colony.OwnerIsOnline())
                if (dancerState.GetActionEnergy(DancerConstants.DANCE) > 0 &&
                    dancerState.NextTimeForWork < Pipliz.Time.SecondsSinceStartDouble)
                {
                    if (TimeCycle.IsDay)
                    {
                        dancerState.SubtractFromActionEnergy(DancerConstants.DANCE, 0.05f);
                    }
                    else
                    {
                        dancerState.SubtractFromActionEnergy(DancerConstants.DANCE, 0.02f);
                    }

                    dancerState.NextTimeForWork = dancerState.RoamingJobSettings.WorkTime + Pipliz.Time.SecondsSinceStartDouble;


                }
        }
    }
    public class DanceAtPlatform : IRoamingJobObjectiveAction
    {
        public string name => DancerConstants.DANCE;

        public float TimeToPreformAction => 10f;

        public string AudioKey => "grassDelete";

        public ItemId ObjectiveLoadEmptyIcon => Nach0ColonyBuiltIn.ItemTypes.DANCER.Id;

        public ItemId PreformAction(Colony colony, RoamingJobState dancerPlatformState)
        {
            var retval = Nach0ColonyBuiltIn.ItemTypes.DANCER.Id;

            if (!colony.OwnerIsOnline() && SettlersConfiguration.OfflineColonies || colony.OwnerIsOnline())
            {
                if (dancerPlatformState.GetActionEnergy(DancerConstants.DANCE) < .75f)
                {
                    var repaired = false;

                    if (repaired)
                    {
                        dancerPlatformState.ResetActionToMaxLoad(DancerConstants.DANCE);
                    }
                }
            }

            return retval;
        }
    }
}
