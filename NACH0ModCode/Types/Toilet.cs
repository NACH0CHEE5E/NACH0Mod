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
    public class ToiletRegister : IRoamingJobObjective
    {
        public string name => "Toilet";
        public float WorkTime => 4;
        public ItemId ItemIndex => Nach0ColonyBuiltIn.ItemTypes.TOILETHOLE.Id;
        public Dictionary<string, IRoamingJobObjectiveAction> ActionCallbacks { get; } = new Dictionary<string, IRoamingJobObjectiveAction>()
        {
            { ToiletConstants.CLEAN, new CleanToilet() }
        };

        public string ObjectiveCategory => ToiletConstants.PLUMBING;

        public void DoWork(Colony colony, RoamingJobState toiletState)
        {
            if ((!colony.OwnerIsOnline() && SettlersConfiguration.OfflineColonies) || colony.OwnerIsOnline())
                if (toiletState.GetActionEnergy(ToiletConstants.CLEAN) > 0 &&
                    toiletState.NextTimeForWork < Pipliz.Time.SecondsSinceStartDouble)
                {
                    toiletState.SubtractFromActionEnergy(ToiletConstants.CLEAN, 0.05f);

                    

                }
        }
    }
    public class CleanToilet : IRoamingJobObjectiveAction
    {
        public string name => ToiletConstants.CLEAN;

        public float TimeToPreformAction => 10;

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

                    requiredForClean.Add(new InventoryItem(ColonyBuiltIn.ItemTypes.BUCKETWATER.Name, 1));
                    returnedFromClean.Add(new InventoryItem(ColonyBuiltIn.ItemTypes.BUCKETEMPTY.Name, 1));

                    if (state.GetActionEnergy(ToiletConstants.CLEAN) < .50f)
                    {
                        requiredForClean.Add(new InventoryItem(ColonyBuiltIn.ItemTypes.BUCKETWATER.Name, 1));
                        returnedFromClean.Add(new InventoryItem(ColonyBuiltIn.ItemTypes.BUCKETEMPTY.Name, 1));
                    }

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
