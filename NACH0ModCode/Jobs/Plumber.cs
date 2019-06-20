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
    [ModLoader.ModManager]
    class PlumberModEntires
    {
        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, Nach0Config.Name + ".Jobs.PlumberModEntries")]
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

    public class PlumberSettings : IBlockJobSettings
    {
        static NPCType _Settings;
        public virtual float NPCShopGameHourMinimum { get { return TimeCycle.Settings.SleepTimeEnd; } }
        public virtual float NPCShopGameHourMaximum { get { return TimeCycle.Settings.SleepTimeStart; } }

        static PlumberSettings()
        {
            NPCType.AddSettings(new NPCTypeStandardSettings
            {
                keyName = Plumber.JOB_NAME,
                printName = "Machinist Day",
                maskColor1 = new Color32(242, 132, 29, 255),
                type = NPCTypeID.GetNextID(),
                inventoryCapacity = 1f
            });

            _Settings = NPCType.GetByKeyNameOrDefault(Plumber.JOB_NAME);
        }

        public virtual ItemTypes.ItemType[] BlockTypes => new[]
        {
            ItemTypes.GetType(Plumber.JOB_ITEM_KEY)
        };

        public virtual NPCType NPCType => _Settings;

        public virtual InventoryItem RecruitmentItem => new InventoryItem(ColonyBuiltIn.ItemTypes.COPPERTOOLS.Name);

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
        public static string JOB_NAME = Nach0Config.JobPrefix + ".Plumber";
        public static string JOB_ITEM_KEY = Nach0Config.TypePrefix + ".PlumberTable";
        public static string JOB_RECIPE = Nach0Config.Name + ".crafter.PlumberTable";

        public Plumber(IBlockJobSettings settings, Pipliz.Vector3Int position, ItemTypes.ItemType type, ByteReader reader) :
            base(settings, position, type, reader)
        {
        }

        public Plumber(IBlockJobSettings settings, Pipliz.Vector3Int position, ItemTypes.ItemType type, Colony colony) :
            base(settings, position, type, colony)
        {
        }


        public override List<string> ObjectiveCategories => new List<string>() { Items.Toilets.ToiletConstants.PLUMBING };
        public override string JobItemKey => JOB_ITEM_KEY;
        public override List<ItemId> OkStatus => new List<ItemId>
            {
                Nach0ColonyBuiltIn.ItemTypes.TOILETCLEANINDICATOR.Id
            };
    }
    public class MachinistTexture : CSTextureMapping
    {
        public const string NAME = GameLoader.NAMESPACE + ".MachinistBenchTop";
        public override string name => NAME;
        public override string albedo => GameLoader.BLOCKS_ALBEDO_PATH + "MachinistBenchTop.png";
        public override string normal => GameLoader.BLOCKS_NORMAL_PATH + "MachinistBenchTop.png";
        public override string height => GameLoader.BLOCKS_HEIGHT_PATH + "MachinistBenchTop.png";
    }

    public class PlumberJobType : CSType
    {
        public override string name => Plumber.JOB_ITEM_KEY;
        public override string icon => Nach0Config.ModIconFolder + "Plumbertable.png";
        public override string onPlaceAudio => "woodPlace";
        public override string onRemoveAudio => "woodDeleteLight";
        public override string sideall => ColonyBuiltIn.ItemTypes.STONEBRICKS;
        public override string sideyp => PlumberTexture.NAME;
        public override List<string> categories => new List<string>() { "job", GameLoader.NAMESPACE };
    }
}
