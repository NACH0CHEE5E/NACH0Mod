using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NACH0
{
    class Nach0Config
    {
        public static string Name = "NACH0";
        public static string TypePrefix = Name + ".Types.";
        public static string ModName = "Mod";
        public static string ModFullName = Name + "." + ModName;
        public static string GamedataFolder = "gamedata";
        public static string ModsFolder = GamedataFolder + "/mods";
        public static string ModFolder = ModsFolder + "/" + Name + "/" + ModName;
        public static string ModGamedataFolder = ModFolder + "/gamedata";
        public static string ModIconFolder = ModGamedataFolder + "/textures/icons/";
    }
}
