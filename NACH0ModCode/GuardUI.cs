using Pipliz;

using Chatting;
using NetworkUI;
using NetworkUI.Items;

using System.Collections.Generic;
using System.Linq;
using Pipliz.JSON;
using System.IO;

namespace NACH0.UI
{

    //open ui with command
    [ChatCommandAutoLoader]
    public class GuardUICommand : IChatCommand
    {
        public bool TryDoCommand(Players.Player player, string chat, List<string> splits)
        {
            if (player == null)
                return false;

            if (!chat.Equals("?guards"))
                return false;

            //Sends the UI to the player
            SendGuardUI.SendUI(player);

            return true;
        }
    }

    //draws ui
    [ModLoader.ModManager]
    public class SendGuardUI
    {

        public static void SendUI(Players.Player player)
        {
            NetworkMenu guardUI = new NetworkMenu();
            guardUI.Identifier = "GuardUI";
            guardUI.Width = 500;
            guardUI.Height = 600;

            Label label = new Label("Guards:");

            guardUI.Items.Add(label);
            guardUI.Items.Add(new EmptySpace(5));

            //SlingShot buttons
            Label slingshotlabel = new Label("Slingshot");
            ButtonCallback SlingshotButtonNight = new ButtonCallback("NACH0.UIButton.SlingShot.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback SlingshotButtonDay = new ButtonCallback("NACH0.UIButton.SlingShot.Day", new LabelData("Day", UnityEngine.Color.black));
            List<TupleStruct<IItem, int>> SlingshotHorizontalItems = new List<TupleStruct<IItem, int>>();

            SlingshotHorizontalItems.Add(new TupleStruct<IItem, int>(slingshotlabel, 150));
            SlingshotHorizontalItems.Add(new TupleStruct<IItem, int>(SlingshotButtonNight, 75));
            SlingshotHorizontalItems.Add(new TupleStruct<IItem, int>(SlingshotButtonDay, 75));

            HorizontalRow SlingshotHorizontalRow = new HorizontalRow(SlingshotHorizontalItems);
            guardUI.Items.Add(SlingshotHorizontalRow);

            //CompoundBow buttons
            Label compoundBowlabel = new Label("Compound Bow");
            ButtonCallback compoundBowButtonNight = new ButtonCallback("NACH0.UIButton.CompoundBow.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback compoundBowButtonDay = new ButtonCallback("NACH0.UIButton.CompoundBow.Day", new LabelData("Day", UnityEngine.Color.black));
            List<TupleStruct<IItem, int>> compoundBowHorizontalItems = new List<TupleStruct<IItem, int>>();

            compoundBowHorizontalItems.Add(new TupleStruct<IItem, int>(compoundBowlabel, 150));
            compoundBowHorizontalItems.Add(new TupleStruct<IItem, int>(compoundBowButtonNight, 75));
            compoundBowHorizontalItems.Add(new TupleStruct<IItem, int>(compoundBowButtonDay, 75));

            HorizontalRow compoundBowHorizontalRow = new HorizontalRow(compoundBowHorizontalItems);
            guardUI.Items.Add(compoundBowHorizontalRow);

            //Sword buttons
            Label swordlabel = new Label("Sword");
            ButtonCallback swordButtonNight = new ButtonCallback("NACH0.UIButton.Sword.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback swordButtonDay = new ButtonCallback("NACH0.UIButton.Sword.Day", new LabelData("Day", UnityEngine.Color.black));
            List<TupleStruct<IItem, int>> swordHorizontalItems = new List<TupleStruct<IItem, int>>();

            swordHorizontalItems.Add(new TupleStruct<IItem, int>(swordlabel, 150));
            swordHorizontalItems.Add(new TupleStruct<IItem, int>(swordButtonNight, 75));
            swordHorizontalItems.Add(new TupleStruct<IItem, int>(swordButtonDay, 75));

            HorizontalRow swordHorizontalRow = new HorizontalRow(swordHorizontalItems);
            guardUI.Items.Add(swordHorizontalRow);

            //Sniper buttons
            Label sniperlabel = new Label("Sniper");
            ButtonCallback sniperButtonNight = new ButtonCallback("NACH0.UIButton.Sniper.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback sniperButtonDay = new ButtonCallback("NACH0.UIButton.Sniper.Day", new LabelData("Day", UnityEngine.Color.black));
            List<TupleStruct<IItem, int>> sniperHorizontalItems = new List<TupleStruct<IItem, int>>();

            sniperHorizontalItems.Add(new TupleStruct<IItem, int>(sniperlabel, 150));
            sniperHorizontalItems.Add(new TupleStruct<IItem, int>(sniperButtonNight, 75));
            sniperHorizontalItems.Add(new TupleStruct<IItem, int>(sniperButtonDay, 75));

            HorizontalRow sniperHorizontalRow = new HorizontalRow(sniperHorizontalItems);
            guardUI.Items.Add(sniperHorizontalRow);

            //Ballista buttons
            Label ballistalabel = new Label("Ballista");
            ButtonCallback ballistaButtonNight = new ButtonCallback("NACH0.UIButton.Ballista.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback ballistaButtonDay = new ButtonCallback("NACH0.UIButton.Ballista.Day", new LabelData("Day", UnityEngine.Color.black));
            List<TupleStruct<IItem, int>> ballistaHorizontalItems = new List<TupleStruct<IItem, int>>();

            ballistaHorizontalItems.Add(new TupleStruct<IItem, int>(ballistalabel, 150));
            ballistaHorizontalItems.Add(new TupleStruct<IItem, int>(ballistaButtonNight, 75));
            ballistaHorizontalItems.Add(new TupleStruct<IItem, int>(ballistaButtonDay, 75));

            HorizontalRow ballistaHorizontalRow = new HorizontalRow(ballistaHorizontalItems);
            guardUI.Items.Add(ballistaHorizontalRow);

            guardUI.Items.Add(new EmptySpace(35));

            //sends ui
            NetworkMenuManager.SendServerPopup(player, guardUI);
        }
    }

    [ModLoader.ModManager]
    public static class GuardUIInteraction
    {
        //public static Dictionary<NetworkID, Dictionary<ushort, int>> warnings = new Dictionary<NetworkID, Dictionary<ushort, int>>();

        public static Dictionary<Players.Player, string> item_placer_dict = new Dictionary<Players.Player, string>();

        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerPushedNetworkUIButton, "NACH0.UIButton.OnPlayerPushedNetworkUIButton")]
        public static void OnPlayerPushedNetworkUIButton(ButtonPressCallbackData data)
        {
            string itemPrefix = "NACH0.Types.";
            string guard = ".Guard";
            string night = guard + ".Nightx+";
            string day = guard + ".Dayx+";
            if (data.ButtonIdentifier.StartsWith("NACH0.UIButton."))
            {
                switch (data.ButtonIdentifier)
                {
                    case "NACH0.UIButton.SlingShot.Night":
                        item_placer_dict[data.Player] = itemPrefix + "Slingshot" + night;
                        AfterItemTypeChanged(data.Player);
                        return;
                    case "NACH0.UIButton.SlingShot.Day":
                        item_placer_dict[data.Player] = itemPrefix + "Slingshot" + day;
                        AfterItemTypeChanged(data.Player);
                        return;
                    case "NACH0.UIButton.CompoundBow.Night":
                        item_placer_dict[data.Player] = itemPrefix + "CompoundBow" + night;
                        AfterItemTypeChanged(data.Player);
                        return;
                    case "NACH0.UIButton.CompoundBow.Day":
                        item_placer_dict[data.Player] = itemPrefix + "CompoundBow" + day;
                        AfterItemTypeChanged(data.Player);
                        return;
                    case "NACH0.UIButton.Sword.Night":
                        item_placer_dict[data.Player] = itemPrefix + "Sword" + night;
                        AfterItemTypeChanged(data.Player);
                        return;
                    case "NACH0.UIButton.Sword.Day":
                        item_placer_dict[data.Player] = itemPrefix + "Sword" + day;
                        AfterItemTypeChanged(data.Player);
                        return;
                    case "NACH0.UIButton.Sniper.Night":
                        item_placer_dict[data.Player] = itemPrefix + "Sniper" + night;
                        AfterItemTypeChanged(data.Player);
                        return;
                    case "NACH0.UIButton.Sniper.Day":
                        item_placer_dict[data.Player] = itemPrefix + "Sniper" + day;
                        AfterItemTypeChanged(data.Player);
                        return;
                    case "NACH0.UIButton.Ballista.Night":
                        item_placer_dict[data.Player] = itemPrefix + "Ballista" + night;
                        AfterItemTypeChanged(data.Player);
                        return;
                    case "NACH0.UIButton.Ballista.Day":
                        item_placer_dict[data.Player] = itemPrefix + "Ballista" + day;
                        AfterItemTypeChanged(data.Player);
                        return;
                }
            }
            
        }
        public static void AfterItemTypeChanged(Players.Player player)
        {
            Chat.Send(player, "<color=blue>Item_Placer Type set to: " + GuardUIInteraction.item_placer_dict[player] + "</color>");
        }
    }
}
