using NetworkUI;
using Pipliz;
using Shared;
using System.Collections.Generic;
using UnityEngine;
using Chatting;

namespace NACH0.CommandTool
{
    [ModLoader.ModManager]
    public class ItemPlace
    {
        static string itemName = Nach0Config.TypePrefix + "ItemPlacer";
        
        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerClicked, "NACH0.CommandTool.OnPlayerClick")]
        public static void PlaceItem(Players.Player player, PlayerClickedData data)
        {
           
            if (data.TypeSelected != ItemTypes.GetType(itemName).ItemIndex)
            {
                //Chat.Send(player, "<color=blue>Error 1</color>");
                return;
            } else if (data.TypeSelected == ItemTypes.GetType(itemName).ItemIndex)
            {
                if (data.ClickType == PlayerClickedData.EClickType.Left)
                {
                    //Chat.Send(player, "<color=blue>Error 2</color>");
                    SendCommandUI.SendUI(player);
                }
                else if (data.ClickType == PlayerClickedData.EClickType.Right)
                {
                    //Chat.Send(player, "<color=blue>Error 3</color>");
                    if (PlayerClickedData.EHitType.Block == data.HitType)
                    {
                        //Chat.Send(player, "<color=blue>Error 4</color>");
                        PlayerClickedData.VoxelHit voxelData = data.GetVoxelHit();
                        if (voxelData.SideHit == VoxelSide.yPlus)
                        {
                            if (commandUIInteraction.item_placer_dict.ContainsKey(player))
                            {
                                //Chat.Send(player, "<color=blue>Error 5</color>");
                                ServerManager.TryChangeBlock(voxelData.PositionBuild, ItemTypes.GetType(commandUIInteraction.item_placer_dict[player]).ItemIndex, player);
                            }
                            //ServerManager.TryChangeBlock(voxelData.PositionBuild, ItemTypes.GetType("NACH0.Types.Slingshot.Guard.Night").ItemIndex);

                            //Chat.Send(player, "<color=blue>Placed: " + commandUIInteraction.item_placer_dict[player] + "</color>");
                        }
                    }
                }
                else
                {
                    //Chat.Send(player, "<color=blue>Error 6</color>");
                    return;
                }
            } else
            {
                //Chat.Send(player, "<color=blue>Error 7</color>");
                return;
            }
        }
    }
}
