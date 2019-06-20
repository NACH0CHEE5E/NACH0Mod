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
using NACH0.Items.Toilets;

namespace NACH0.types
{
    public class PorcelainToiletBlock : CSType
    {
        public override string name { get; set; } = Nach0Config.TypePrefix + "PorcelainToilet";
        public override string icon { get; set; } = Nach0Config.ModIconFolder + "PorcelainToilet.png";
        public override bool? isPlaceable { get; set; } = true;
        public override string onPlaceAudio { get; set; } = "stonePlace";
        public override string onRemoveAudio { get; set; } = "stoneDelete";
        public override bool? isSolid { get; set; } = true;
        public override string sideall { get; set; } = Nach0Config.TexturePrefix + "PorcelainToilet";
        public override List<string> categories { get; set; } = new List<string>()
        {
            "essential",
            "toilet",
            "NACH0"
        };
    }
    public class CeramicToiletRegister : IRoamingJobObjective
    {
        public string name => "PorcelainToilet";
        public float WorkTime => 5f;
        public ItemId ItemIndex => Nach0ColonyBuiltIn.ItemTypes.TOILET.Id;
        public Dictionary<string, IRoamingJobObjectiveAction> ActionCallbacks { get; } = new Dictionary<string, IRoamingJobObjectiveAction>()
        {
            { ToiletConstants.CLEAN, new CleanToilet() }
        };

        public string ObjectiveCategory => "toilet";

        public void DoWork(Colony colony, RoamingJobState toiletState)
        {
            if ((!colony.OwnerIsOnline() && SettlersConfiguration.OfflineColonies) || colony.OwnerIsOnline())
                if (toiletState.GetActionEnergy(ToiletConstants.CLEAN) > 0 &&
                    toiletState.NextTimeForWork < Pipliz.Time.SecondsSinceStartDouble)
                {
                    if (TimeCycle.IsDay)
                    {
                        toiletState.SubtractFromActionEnergy(ToiletConstants.CLEAN, 0.05f);
                    }
                    else
                    {
                        toiletState.SubtractFromActionEnergy(ToiletConstants.CLEAN, 0.02f);
                    }

                    toiletState.NextTimeForWork = toiletState.RoamingJobSettings.WorkTime + Pipliz.Time.SecondsSinceStartDouble;


                }
        }
    }
    public class ToiletRegister : IRoamingJobObjective
    {
        public string name => "Toilet";
        public float WorkTime => 5f;
        public ItemId ItemIndex => Nach0ColonyBuiltIn.ItemTypes.TOILETHOLE.Id;
        public Dictionary<string, IRoamingJobObjectiveAction> ActionCallbacks { get; } = new Dictionary<string, IRoamingJobObjectiveAction>()
        {
            { ToiletConstants.CLEAN, new CleanToilet() }
        };

        public string ObjectiveCategory => "toilet";

        public void DoWork(Colony colony, RoamingJobState toiletState)
        {
            if ((!colony.OwnerIsOnline() && SettlersConfiguration.OfflineColonies) || colony.OwnerIsOnline())
                if (toiletState.GetActionEnergy(ToiletConstants.CLEAN) > 0 &&
                    toiletState.NextTimeForWork < Pipliz.Time.SecondsSinceStartDouble)
                {
                    if (TimeCycle.IsDay)
                    {
                        toiletState.SubtractFromActionEnergy(ToiletConstants.CLEAN, 0.05f);
                    }
                    else
                    {
                        toiletState.SubtractFromActionEnergy(ToiletConstants.CLEAN, 0.02f);
                    }
                    
                    toiletState.NextTimeForWork = toiletState.RoamingJobSettings.WorkTime + Pipliz.Time.SecondsSinceStartDouble;


                }
        }
    }
    public class CleanToilet : IRoamingJobObjectiveAction
    {
        public string name => ToiletConstants.CLEAN;

        public float TimeToPreformAction => 10f;

        public string AudioKey => "grassDelete";

        public ItemId ObjectiveLoadEmptyIcon => Nach0ColonyBuiltIn.ItemTypes.TOILETCLEANINDICATOR.Id;

        public ItemId PreformAction(Colony colony, RoamingJobState state)
        {
            var retval = Nach0ColonyBuiltIn.ItemTypes.TOILETCLEANINDICATOR.Id;

            if (!colony.OwnerIsOnline() && SettlersConfiguration.OfflineColonies || colony.OwnerIsOnline())
            {
                if (state.GetActionEnergy(ToiletConstants.CLEAN) < .75f)
                {
                    var repaired = false;
                    var requiredForClean = new List<InventoryItem>();
                    var returnedFromClean = new List<InventoryItem>();
                    var stockpile = colony.Stockpile;
                    int itemReqMult = 1;


                    if (state.GetActionEnergy(ToiletConstants.CLEAN) < .50f)
                    {
                        itemReqMult ++;
                    }
                    if (state.GetActionEnergy(ToiletConstants.CLEAN) < .30f)
                    {
                        itemReqMult ++;
                    }
                    if (state.GetActionEnergy(ToiletConstants.CLEAN) < .10f)
                    {
                        itemReqMult ++;
                    }

                    requiredForClean.Add(new InventoryItem(ColonyBuiltIn.ItemTypes.BUCKETWATER.Name, 1 * itemReqMult));
                    returnedFromClean.Add(new InventoryItem(ColonyBuiltIn.ItemTypes.BUCKETEMPTY.Name, 1 * itemReqMult));

                    if (stockpile.Contains(requiredForClean))
                    {
                        stockpile.TryRemove(requiredForClean);
                        repaired = true;
                        stockpile.Add(returnedFromClean);
                    }
                    else
                    {
                        foreach (var item in requiredForClean)
                            if (!stockpile.Contains(item))
                            {
                                retval = ItemId.GetItemId(item.Type);
                                break;
                            }
                    }

                    if (repaired)
                        state.ResetActionToMaxLoad(ToiletConstants.CLEAN);
                }
            }

            return retval;
        }
    }
}
