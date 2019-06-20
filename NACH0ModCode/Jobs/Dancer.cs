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

namespace NACH0.Jobs.Dancer
{
    public class dancerJobType : CSType
    {
        public override string name => Nach0Config.TypePrefix + "DancerBlock";
        public override string icon => Nach0Config.ModIconFolder + "Dancer.png";
        public override string onPlaceAudio => "stonePlace";
        public override string onRemoveAudio => "stoneDelete";
        public override string sideall => "planks";
        public override List<string> categories => new List<string>() { "job", "NACH0" }; // whatever you want really
    }
    public class DancerSettings : IBlockJobSettings
    {
        static NPCType _Settings;
        public virtual float NPCShopGameHourMinimum { get { return TimeCycle.Settings.SleepTimeEnd; } }
        public virtual float NPCShopGameHourMaximum { get { return TimeCycle.Settings.SleepTimeStart; } }

        static DancerSettings()
        {
            NPCType.AddSettings(new NPCTypeStandardSettings
            {
                keyName = Nach0Config.JobPrefix + "Dancer",
                printName = "Dancer",
                maskColor1 = new Color32(30, 77, 153, 255),
                type = NPCTypeID.GetNextID(),
                inventoryCapacity = 1f
            });

            _Settings = NPCType.GetByKeyNameOrDefault(Nach0Config.JobPrefix + "Dancer");
        }

        public virtual ItemTypes.ItemType[] BlockTypes => new[]
        {
            ItemTypes.GetType(Nach0Config.TypePrefix + "Dancer") // the block type placed
        };

        public NPCType NPCType => _Settings;

        public virtual InventoryItem RecruitmentItem => new InventoryItem(Nach0Config.TypePrefix + "BasicInstrument");

        public virtual bool ToSleep => !TimeCycle.IsDay;

        public Pipliz.Vector3Int GetJobLocation(BlockJobInstance instance)
        {
            if (instance is RoamingJob roamingJob)
                return roamingJob.OriginalPosition;

            return Pipliz.Vector3Int.invalidPos;
        }

        public void OnGoalChanged(BlockJobInstance instance, NPCBase.NPCGoal goalOld, NPCBase.NPCGoal goalNew)
        {

        }

        public void OnNPCAtJob(BlockJobInstance instance, ref NPCBase.NPCState state)
        {
            instance.OnNPCAtJob(ref state);
        }

        public void OnNPCAtStockpile(BlockJobInstance instance, ref NPCBase.NPCState state)
        {

        }
    }
    public class Dancer : RoamingJob
    {
        public static string JOB_NAME = Nach0Config.JobPrefix + "Dancer";
        public static string JOB_ITEM_KEY = Nach0Config.TypePrefix + "DancerBlock";
        public static string JOB_RECIPE = Nach0Config.Name + ".crafter.DancerBlock";

        public Dancer(IBlockJobSettings settings, Pipliz.Vector3Int position, ItemTypes.ItemType type, ByteReader reader) :
            base(settings, position, type, reader)
        {
        }

        public Dancer(IBlockJobSettings settings, Pipliz.Vector3Int position, ItemTypes.ItemType type, Colony colony) :
            base(settings, position, type, colony)
        {
        }


        public override List<string> ObjectiveCategories => new List<string>() { "dancing" };
        public override string JobItemKey => JOB_ITEM_KEY;
        public override List<ItemId> OkStatus => new List<ItemId>
            {
                Nach0ColonyBuiltIn.ItemTypes.DANCER.Id,
                ItemId.GetItemId("Pandaros.Settlers.Waiting")
            };
    }

    [ModLoader.ModManager]
    public static class DancerModEntries
    {
        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, Nach0Config.Name + ".Dancer")]
        [ModLoader.ModCallbackProvidesFor("create_savemanager")]
        public static void AfterDefiningNPCTypes()
        {
            ServerManager.BlockEntityCallbacks.RegisterEntityManager(
                new BlockJobManager<Dancer>(
                    new DancerSettings(),
                    (setting, pos, type, bytedata) => new Dancer(setting, pos, type, bytedata),
                    (setting, pos, type, colony) => new Dancer(setting, pos, type, colony)
                )
            );
        }
    }
}
