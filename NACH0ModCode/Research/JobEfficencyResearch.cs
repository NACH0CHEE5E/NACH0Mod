using Pandaros.Settlers.ColonyManagement;
using Pandaros.Settlers.Models;
using System.Collections.Generic;
using Pandaros.Settlers.Research;
using Pandaros.Settlers;
using NACH0.Models;
using Science;

namespace NACH0.Research
{
    public class Nach0JobResearch
    {
        private const string SCIENCEBAGREQ = ColonyBuiltIn.Research.SCIENCEBAGBASIC;
        private const int BAG_COST = 2;
        private const int COIN_COST = 5;

        public class CopperWorkerTrainingResearch : IPandaResearch
        {
            public Dictionary<ItemId, int> RequiredItems => new Dictionary<ItemId, int>()
            {
                { ColonyBuiltIn.ItemTypes.COPPERPARTS, 5 },
                { ColonyBuiltIn.ItemTypes.COPPERNAILS, 7 },
                { ColonyBuiltIn.ItemTypes.SCIENCEBAGBASIC, BAG_COST },
                { ColonyBuiltIn.ItemTypes.BRONZECOIN, COIN_COST }
            };

            public int NumberOfLevels => 5;

            public float BaseValue => 0.05f;

            public List<string> Dependancies => new List<string>()
            {
                SCIENCEBAGREQ,
                ColonyBuiltIn.Research.FINERYFORGE
            };

            public int BaseIterationCount => 10;
            public string IconDirectory => Nach0Config.ModIconFolder;
            public List<IResearchableCondition> Conditions => null;

            public bool AddLevelToName => true;

            public string name => Nach0ColonyBuiltIn.NpcTypes.COPPERWORKER;

            public void OnRegister()
            {

            }

            public void ResearchComplete(object sender, ResearchCompleteEventArgs e)
            {
                SettlerManager.ApplyJobCooldownsToNPCs(e.Manager.Colony);
            }
        }
        public class BrickMakerTrainingResearch : IPandaResearch
        {
            public Dictionary<ItemId, int> RequiredItems => new Dictionary<ItemId, int>()
            {
                { ColonyBuiltIn.ItemTypes.BRICKS, 5 },
                { ColonyBuiltIn.ItemTypes.SCIENCEBAGBASIC, BAG_COST },
                { ColonyBuiltIn.ItemTypes.BRONZECOIN, COIN_COST }
            };

            public int NumberOfLevels => 5;

            public float BaseValue => 0.05f;

            public List<string> Dependancies => new List<string>()
            {
                SCIENCEBAGREQ,
                ColonyBuiltIn.Research.FINERYFORGE
            };

            public int BaseIterationCount => 10;
            public string IconDirectory => Nach0Config.ModIconFolder;
            public List<IResearchableCondition> Conditions => null;

            public bool AddLevelToName => true;

            public string name => Nach0ColonyBuiltIn.NpcTypes.BRICKMAKER;

            public void OnRegister()
            {

            }

            public void ResearchComplete(object sender, ResearchCompleteEventArgs e)
            {
                SettlerManager.ApplyJobCooldownsToNPCs(e.Manager.Colony);
            }
        }
        public class FletcherTrainingResearch : IPandaResearch
        {
            public Dictionary<ItemId, int> RequiredItems => new Dictionary<ItemId, int>()
            {
                { ColonyBuiltIn.ItemTypes.BRONZEARROW, 5 },
                { ColonyBuiltIn.ItemTypes.CROSSBOWBOLT, 5 },
                { ColonyBuiltIn.ItemTypes.SCIENCEBAGBASIC, BAG_COST },
                { ColonyBuiltIn.ItemTypes.BRONZECOIN, COIN_COST }
            };

            public int NumberOfLevels => 5;

            public float BaseValue => 0.05f;

            public List<string> Dependancies => new List<string>()
            {
                SCIENCEBAGREQ,
                ColonyBuiltIn.Research.FINERYFORGE
            };

            public int BaseIterationCount => 10;
            public string IconDirectory => Nach0Config.ModIconFolder;
            public List<IResearchableCondition> Conditions => null;

            public bool AddLevelToName => true;

            public string name => Nach0ColonyBuiltIn.NpcTypes.FLETCHER;

            public void OnRegister()
            {

            }

            public void ResearchComplete(object sender, ResearchCompleteEventArgs e)
            {
                SettlerManager.ApplyJobCooldownsToNPCs(e.Manager.Colony);
            }
        }
        public class ToolCrafterTrainingResearch : IPandaResearch
        {
            public Dictionary<ItemId, int> RequiredItems => new Dictionary<ItemId, int>()
            {
                { ColonyBuiltIn.ItemTypes.IRONSWORD, 3 },
                { ColonyBuiltIn.ItemTypes.BRONZEPICKAXE, 2 },
                { ColonyBuiltIn.ItemTypes.BRONZEAXE, 2 },
                { ColonyBuiltIn.ItemTypes.SCIENCEBAGBASIC, BAG_COST },
                { ColonyBuiltIn.ItemTypes.BRONZECOIN, COIN_COST }
            };

            public int NumberOfLevels => 5;

            public float BaseValue => 0.05f;

            public List<string> Dependancies => new List<string>()
            {
                SCIENCEBAGREQ,
                ColonyBuiltIn.Research.FINERYFORGE
            };

            public int BaseIterationCount => 10;
            public string IconDirectory => Nach0Config.ModIconFolder;
            public List<IResearchableCondition> Conditions => null;

            public bool AddLevelToName => true;

            public string name => Nach0ColonyBuiltIn.NpcTypes.TOOLCRAFTER;

            public void OnRegister()
            {

            }

            public void ResearchComplete(object sender, ResearchCompleteEventArgs e)
            {
                SettlerManager.ApplyJobCooldownsToNPCs(e.Manager.Colony);
            }
        }
        public class FreezerTrainingResearch : IPandaResearch
        {
            public Dictionary<ItemId, int> RequiredItems => new Dictionary<ItemId, int>()
            {
                { Nach0ColonyBuiltIn.ItemTypes.ICE, 5 },
                { ColonyBuiltIn.ItemTypes.SCIENCEBAGBASIC, BAG_COST },
                { ColonyBuiltIn.ItemTypes.BRONZECOIN, COIN_COST }
            };

            public int NumberOfLevels => 2;

            public float BaseValue => 0.05f;
            public string IconDirectory => Nach0Config.ModIconFolder;
            public List<IResearchableCondition> Conditions => null;

            public List<string> Dependancies => new List<string>()
            {
                SCIENCEBAGREQ,
                ColonyBuiltIn.Research.FINERYFORGE
            };

            public int BaseIterationCount => 10;

            public bool AddLevelToName => true;

            public string name => Nach0ColonyBuiltIn.NpcTypes.FREEZER;

            public void OnRegister()
            {

            }

            public void ResearchComplete(object sender, ResearchCompleteEventArgs e)
            {
                SettlerManager.ApplyJobCooldownsToNPCs(e.Manager.Colony);
            }
        }
        public class PaintMixerTrainingResearch : IPandaResearch
        {
            public Dictionary<ItemId, int> RequiredItems => new Dictionary<ItemId, int>()
            {
                { Nach0ColonyBuiltIn.ItemTypes.PAINTWHITE, 2 },
                { ColonyBuiltIn.ItemTypes.SCIENCEBAGBASIC, BAG_COST },
                { ColonyBuiltIn.ItemTypes.BRONZECOIN, COIN_COST }
            };

            public int NumberOfLevels => 3;

            public float BaseValue => 0.05f;

            public List<string> Dependancies => new List<string>()
            {
                SCIENCEBAGREQ,
                ColonyBuiltIn.Research.FINERYFORGE
            };

            public int BaseIterationCount => 10;

            public bool AddLevelToName => true;
            public string IconDirectory => Nach0Config.ModIconFolder;
            public List<IResearchableCondition> Conditions => null;

            public string name => Nach0ColonyBuiltIn.NpcTypes.PAINTMIXER;

            public void OnRegister()
            {

            }

            public void ResearchComplete(object sender, ResearchCompleteEventArgs e)
            {
                SettlerManager.ApplyJobCooldownsToNPCs(e.Manager.Colony);
            }
        }
        public class PainterTrainingResearch : IPandaResearch
        {
            public Dictionary<ItemId, int> RequiredItems => new Dictionary<ItemId, int>()
            {
                { Nach0ColonyBuiltIn.ItemTypes.PAINTMAGENTA, 3 },
                { ColonyBuiltIn.ItemTypes.SCIENCEBAGBASIC, BAG_COST },
                { ColonyBuiltIn.ItemTypes.BRONZECOIN, COIN_COST }
            };

            public int NumberOfLevels => 2;

            public float BaseValue => 0.05f;

            public List<string> Dependancies => new List<string>()
            {
                SCIENCEBAGREQ,
                ColonyBuiltIn.Research.FINERYFORGE
            };

            public int BaseIterationCount => 10;
            public string IconDirectory => Nach0Config.ModIconFolder;
            public List<IResearchableCondition> Conditions => null;

            public bool AddLevelToName => true;

            public string name => Nach0ColonyBuiltIn.NpcTypes.PAINTER;

            public void OnRegister()
            {

            }

            public void ResearchComplete(object sender, ResearchCompleteEventArgs e)
            {
                SettlerManager.ApplyJobCooldownsToNPCs(e.Manager.Colony);
            }
        }
        public class StoneMasonTrainingResearch : IPandaResearch
        {
            public Dictionary<ItemId, int> RequiredItems => new Dictionary<ItemId, int>()
            {
                { ColonyBuiltIn.ItemTypes.STONEBRICKS, 5 },
                { ColonyBuiltIn.ItemTypes.SCIENCEBAGBASIC, BAG_COST },
                { ColonyBuiltIn.ItemTypes.BRONZECOIN, COIN_COST }
            };
            
            public int NumberOfLevels => 2;

            public float BaseValue => 0.05f;

            public List<string> Dependancies => new List<string>()
            {
                SCIENCEBAGREQ,
                ColonyBuiltIn.Research.FINERYFORGE
            };

            public int BaseIterationCount => 10;


            public bool AddLevelToName => true;

            public string name => ColonyBuiltIn.NpcTypes.STONEMASON;
            public string IconDirectory => Nach0Config.ModIconFolder;
            public List<IResearchableCondition> Conditions => null;

            public void OnRegister()
            {

            }

            public void ResearchComplete(object sender, ResearchCompleteEventArgs e)
            {
                SettlerManager.ApplyJobCooldownsToNPCs(e.Manager.Colony);
            }
        }
        public class DyerTrainingResearch : IPandaResearch
        {
            public Dictionary<ItemId, int> RequiredItems => new Dictionary<ItemId, int>()
            {
                { Nach0ColonyBuiltIn.ItemTypes.DYEMAGENTA, 5 },
                { ColonyBuiltIn.ItemTypes.SCIENCEBAGBASIC, BAG_COST },
                { ColonyBuiltIn.ItemTypes.BRONZECOIN, COIN_COST }
            };

            public int NumberOfLevels => 2;

            public float BaseValue => 0.05f;

            public List<string> Dependancies => new List<string>()
            {
                SCIENCEBAGREQ,
                ColonyBuiltIn.Research.FINERYFORGE
            };

            public int BaseIterationCount => 10;
            public string IconDirectory => Nach0Config.ModIconFolder;
            public List<IResearchableCondition> Conditions => null;

            public bool AddLevelToName => true;

            public string name => ColonyBuiltIn.NpcTypes.DYER;

            public void OnRegister()
            {

            }

            public void ResearchComplete(object sender, ResearchCompleteEventArgs e)
            {
                SettlerManager.ApplyJobCooldownsToNPCs(e.Manager.Colony);
            }
        }


    }
    
}

