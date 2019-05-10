using NetworkUI;
using Pipliz;
using Shared;
using System.Collections.Generic;
using UnityEngine;
using Chatting;

namespace NACH0.UI
{
    [ModLoader.ModManager]
    public class ItemPlace
    {
        static string itemName = "NACH0.Types.ItemPlacer";
        
        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerClicked, "NACH0.UI.ItemPlace.OnPlayerClick")]
        public static void PlaceItem(Players.Player player, PlayerClickedData data)
        {
           
            if (data.TypeSelected != ItemTypes.GetType(itemName).ItemIndex)
            {
                return;
            }
            if (data.TypeSelected == ItemTypes.GetType(itemName).ItemIndex)
            {
                if (data.ClickType == PlayerClickedData.EClickType.Left)
                {
                    SendGuardUI.SendUI(player);
                }
                else if (data.ClickType == PlayerClickedData.EClickType.Right)
                {
                    if (PlayerClickedData.EHitType.Block == data.HitType)
                    {
                        PlayerClickedData.VoxelHit voxelData = data.GetVoxelHit();
                        if (voxelData.SideHit == VoxelSide.yPlus)
                        {
                            /*if (voxelData.DistanceToHit <= 10)
                            {
                                
                            }*/
                            ServerManager.TryChangeBlock(voxelData.PositionBuild, ItemTypes.GetType(GuardUIInteraction.item_placer_dict[player]).ItemIndex);
                            //ServerManager.TryChangeBlock(voxelData.PositionBuild, ItemTypes.GetType("NACH0.Types.Slingshot.Guard.Night").ItemIndex);

                            Chat.Send(player, "<color=blue>Placed: " + GuardUIInteraction.item_placer_dict[player] + "</color>");
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
