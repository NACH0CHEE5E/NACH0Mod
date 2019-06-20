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

namespace NACH0.Jobs
{
    public class plumberJobType : CSType
    {
        public override string name => Nach0Config.TypePrefix + "PlumberTable";
        public override string icon => Nach0Config.ModIconFolder + "PlumberTable.png";
        public override string onPlaceAudio => "stonePlace";
        public override string onRemoveAudio => "stoneDelete";
        public override string sideall => "planks";
        public override List<string> categories => new List<string>() { "job", "NACH0" }; // whatever you want really
    }
    public class PlumberSettings : IBlockJobSettings
    {
        static NPCType _Settings;
        public virtual float NPCShopGameHourMinimum { get { return TimeCycle.Settings.SleepTimeEnd; } }
        public virtual float NPCShopGameHourMaximum { get { return TimeCycle.Settings.SleepTimeStart; } }

        static PlumberSettings()
        {
            NPCType.AddSettings(new NPCTypeStandardSettings
            {
                keyName = Nach0Config.JobPrefix + "Plumber",
                printName = "Plumber",
                maskColor1 = new Color32(242, 132, 29, 255),
                type = NPCTypeID.GetNextID(),
                inventoryCapacity = 1f
            });

            _Settings = NPCType.GetByKeyNameOrDefault(Nach0Config.JobPrefix + "Plumber");
        }

        public virtual ItemTypes.ItemType[] BlockTypes => new[]
        {
            ItemTypes.GetType(Nach0Config.TypePrefix + "PlumberTable") // the block type placed
        };

        public NPCType NPCType => _Settings;

        public virtual InventoryItem RecruitmentItem => new InventoryItem("coppertools");

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
    public class Plumber : RoamingJob
    {
        public static string JOB_NAME = Nach0Config.JobPrefix + "Plumber";
        public static string JOB_ITEM_KEY = Nach0Config.TypePrefix + "PlumberTable";
        public static string JOB_RECIPE = Nach0Config.Name + ".crafter.Plumber";

        public Plumber(IBlockJobSettings settings, Pipliz.Vector3Int position, ItemTypes.ItemType type, ByteReader reader) :
            base(settings, position, type, reader)
        {
        }

        public Plumber(IBlockJobSettings settings, Pipliz.Vector3Int position, ItemTypes.ItemType type, Colony colony) :
            base(settings, position, type, colony)
        {
        }


        public override List<string> ObjectiveCategories => new List<string>() { "Toilets" };
        public override string JobItemKey => JOB_ITEM_KEY;
        public override List<ItemId> OkStatus => new List<ItemId>
            {
                Nach0ColonyBuiltIn.ItemTypes.TOILETCLEANINDICATOR.Id
            };
    }
    public class CleaningIcon : CSType
    {
        public override string name { get; set; } = Nach0Config.IndicatorTypePrefix + "ToiletClean";
        public override string icon { get; set; } = Nach0Config.ModIconFolder + "ToiletClean.png";
    }

    [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, Nach0Config.Name + ".Plumber")]
    [ModLoader.ModCallbackProvidesFor("create_savemanager")]
    public static void AfterDefiningNPCTypes()
    {
        ServerManager.BlockEntityCallbacks.RegisterEntityManager(
            new BlockJobManager<Plumber>(
                new PlumberSettings(),
                (setting, pos, type, bytedata) => new Plumber(setting, pos, type, bytedata),
                (setting, pos, type, colony) => new Plumber(setting, pos, type, colony)
            )
        );
    }
}
