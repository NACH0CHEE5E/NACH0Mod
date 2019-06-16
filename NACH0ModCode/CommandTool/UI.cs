using Pipliz;

using Chatting;
using NetworkUI;
using NetworkUI.Items;
using Science;
using System;

using System.Collections.Generic;
using System.Linq;
using Pipliz.JSON;
using System.IO;

namespace NACH0.CommandTool
{
    /*[ModLoader.ModManager]
    public class Warning
    {
        //Check every <increment> (miliseconds) the amount of the types observed
        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnUpdate, "NACH0.UI.ItemPlacerCheck")]
        public static void OnUpdate()
        {
            if (warehouse.Items.GetValueOrDefault(itemIndex, 0) < amount)
            {
                Chat.Send(player, "<color=red>Warning: You need more: " + ItemTypes.IndexLookup.GetName(itemIndex) + ", current amount: " + warehouse.Items.GetValueOrDefault(itemIndex, 0) + "</color>");
            }
        }
    }*/

    //open ui with command
    [ChatCommandAutoLoader]
    public class Command : IChatCommand
    {
        public bool TryDoCommand(Players.Player player, string chat, List<string> splits)
        {
            if (player == null)
                return false;

            if (!chat.Equals("?nach0tool"))
                return false;

            //Sends the UI to the player
            SendCommandUI.SendUI(player);

            return true;
        }
    }

    //draws ui
    [ModLoader.ModManager]
    public class SendCommandUI
    {
        public static void SendUI(Players.Player player)
        {
            NetworkMenu commandUI = new NetworkMenu();
            commandUI.Identifier = "CommandToolUI";
            commandUI.Width = 500;
            commandUI.Height = 600;

            
            ButtonCallback GuardsButton = new ButtonCallback("NACH0.UIButton.Guards", new LabelData("Guards", UnityEngine.Color.black));

            //commandUI.Items.Add(label);
            //commandUI.Items.Add(new EmptySpace(5));

            //if (Res)
            //SlingShot buttons
            Label Guardslabel = new Label("Guards:");
            ButtonCallback BackButton = new ButtonCallback("NACH0.UIButton.Back", new LabelData("BACK", UnityEngine.Color.black));
            List<(IItem, int)> GuardsHeaderHorizontalItems = new List<(IItem, int)>();

            GuardsHeaderHorizontalItems.Add((Guardslabel, 150));
            //GuardsHeaderHorizontalItems.Add(new EmptySpace(75));
            GuardsHeaderHorizontalItems.Add((BackButton, 75));

            HorizontalRow GuardsHeaderHorizontalRow = new HorizontalRow(GuardsHeaderHorizontalItems);

            Label slingshotlabel = new Label("Slingshot");
            ButtonCallback SlingshotButtonNight = new ButtonCallback("NACH0.UIButton.SlingShot.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback SlingshotButtonDay = new ButtonCallback("NACH0.UIButton.SlingShot.Day", new LabelData("Day", UnityEngine.Color.black));
            List<(IItem, int)> SlingshotHorizontalItems = new List<(IItem, int)>();

            SlingshotHorizontalItems.Add((slingshotlabel, 150));
            SlingshotHorizontalItems.Add((SlingshotButtonNight, 75));
            SlingshotHorizontalItems.Add((SlingshotButtonDay, 75));

            HorizontalRow SlingshotHorizontalRow = new HorizontalRow(SlingshotHorizontalItems);
            

            //CompoundBow buttons
            Label compoundBowlabel = new Label("Compound Bow");
            ButtonCallback compoundBowButtonNight = new ButtonCallback("NACH0.UIButton.CompoundBow.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback compoundBowButtonDay = new ButtonCallback("NACH0.UIButton.CompoundBow.Day", new LabelData("Day", UnityEngine.Color.black));
            List<(IItem, int)> compoundBowHorizontalItems = new List<(IItem, int)>();

            compoundBowHorizontalItems.Add((compoundBowlabel, 150));
            compoundBowHorizontalItems.Add((compoundBowButtonNight, 75));
            compoundBowHorizontalItems.Add((compoundBowButtonDay, 75));

            HorizontalRow compoundBowHorizontalRow = new HorizontalRow(compoundBowHorizontalItems);
            

            //Sword buttons
            Label swordlabel = new Label("Sword");
            ButtonCallback swordButtonNight = new ButtonCallback("NACH0.UIButton.Sword.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback swordButtonDay = new ButtonCallback("NACH0.UIButton.Sword.Day", new LabelData("Day", UnityEngine.Color.black));
            List<(IItem, int)> swordHorizontalItems = new List<(IItem, int)>();

            swordHorizontalItems.Add((swordlabel, 150));
            swordHorizontalItems.Add((swordButtonNight, 75));
            swordHorizontalItems.Add((swordButtonDay, 75));

            HorizontalRow swordHorizontalRow = new HorizontalRow(swordHorizontalItems);
            

            //Sniper buttons
            Label sniperlabel = new Label("Sniper");
            ButtonCallback sniperButtonNight = new ButtonCallback("NACH0.UIButton.Sniper.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback sniperButtonDay = new ButtonCallback("NACH0.UIButton.Sniper.Day", new LabelData("Day", UnityEngine.Color.black));
            List<(IItem, int)> sniperHorizontalItems = new List<(IItem, int)>();

            sniperHorizontalItems.Add((sniperlabel, 150));
            sniperHorizontalItems.Add((sniperButtonNight, 75));
            sniperHorizontalItems.Add((sniperButtonDay, 75));

            HorizontalRow sniperHorizontalRow = new HorizontalRow(sniperHorizontalItems);
            

            //Ballista buttons
            Label ballistalabel = new Label("Ballista");
            ButtonCallback ballistaButtonNight = new ButtonCallback("NACH0.UIButton.Ballista.Night", new LabelData("Night", UnityEngine.Color.black));
            ButtonCallback ballistaButtonDay = new ButtonCallback("NACH0.UIButton.Ballista.Day", new LabelData("Day", UnityEngine.Color.black));
            List<(IItem, int)> ballistaHorizontalItems = new List<(IItem, int)>();

            ballistaHorizontalItems.Add((ballistalabel, 150));
            ballistaHorizontalItems.Add((ballistaButtonNight, 75));
            ballistaHorizontalItems.Add((ballistaButtonDay, 75));

            HorizontalRow ballistaHorizontalRow = new HorizontalRow(ballistaHorizontalItems);

            if (commandUIInteraction.item_placer_option_dict.ContainsKey(player))
            {
                if (commandUIInteraction.item_placer_option_dict[player].Equals("Guards"))
                {
                    commandUI.Items.Add(GuardsHeaderHorizontalRow);
                    if (player == null && player.ConnectionState != Players.EConnectionState.Connected || player.ActiveColony == null || player.ActiveColony.ScienceData == null)
                        return;

                    Science.ScienceKey SlingShotScienceKey = new Science.ScienceKey("NACH0.Research.Slingshot");
                    Science.ScienceKey CompoundBowScienceKey = new Science.ScienceKey("NACH0.Research.CompoundBow");
                    Science.ScienceKey SwordScienceKey = new Science.ScienceKey("NACH0.Research.SwordGuard");
                    Science.ScienceKey SniperScienceKey = new Science.ScienceKey("NACH0.Research.Sniper");
                    Science.ScienceKey BallistaScienceKey = new Science.ScienceKey("NACH0.Research.Ballista");

                    if (SlingShotScienceKey.IsCompleted(player.ActiveColony.ScienceData))
                        commandUI.Items.Add(SlingshotHorizontalRow);
                    if (CompoundBowScienceKey.IsCompleted(player.ActiveColony.ScienceData))
                        commandUI.Items.Add(compoundBowHorizontalRow);
                    if (SwordScienceKey.IsCompleted(player.ActiveColony.ScienceData))
                        commandUI.Items.Add(swordHorizontalRow);
                    if (SniperScienceKey.IsCompleted(player.ActiveColony.ScienceData))
                        commandUI.Items.Add(sniperHorizontalRow);
                    if (BallistaScienceKey.IsCompleted(player.ActiveColony.ScienceData))
                        commandUI.Items.Add(ballistaHorizontalRow);

                    //commandUI.Items.Add(SlingshotHorizontalRow);
                    //commandUI.Items.Add(compoundBowHorizontalRow);
                    //commandUI.Items.Add(swordHorizontalRow);
                    //commandUI.Items.Add(sniperHorizontalRow);
                    //commandUI.Items.Add(ballistaHorizontalRow);
                } else
                {
                    commandUI.Items.Add(GuardsButton);
                    commandUI.Items.Add(new EmptySpace(5));
                }
            } else
            {
                commandUI.Items.Add(GuardsButton);
                commandUI.Items.Add(new EmptySpace(5));
            }
            commandUI.Items.Add(new EmptySpace(35));

            //sends ui
            NetworkMenuManager.SendServerPopup(player, commandUI);
        }
    }

    [ModLoader.ModManager]
    public static class commandUIInteraction
    {
        //public static Dictionary<NetworkID, Dictionary<ushort, int>> warnings = new Dictionary<NetworkID, Dictionary<ushort, int>>();

        public static Dictionary<Players.Player, string> item_placer_dict = new Dictionary<Players.Player, string>();
        public static Dictionary<Players.Player, string> item_placer_option_dict = new Dictionary<Players.Player, string>();

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
                    case "NACH0.UIButton.Guards":
                        item_placer_option_dict[data.Player] = "Guards";
                        SendCommandUI.SendUI(data.Player);
                        item_placer_option_dict[data.Player] = "";
                        return;
                    case "NACH0.UIButton.Back":
                        item_placer_option_dict[data.Player] = "";
                        SendCommandUI.SendUI(data.Player);
                        return;
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
            //Chat.Send(player, "<color=blue>Item_Placer Type set to: " + commandUIInteraction.item_placer_dict[player] + "</color>");
        }
    }
}
