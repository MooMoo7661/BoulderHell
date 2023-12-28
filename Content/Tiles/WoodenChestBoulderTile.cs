using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.Audio;
using BoulderHell.Content.Items;
using BoulderHell.Content.Projectiles;

namespace BoulderHell.Content.Tiles
{
    public class WoodenChestBoulderTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileSpelunker[Type] = true;
            Main.tileContainer[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileOreFinderPriority[Type] = 500;
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.AvoidedByNPCs[Type] = true;
            TileID.Sets.InteractibleByNPCs[Type] = true;
            TileID.Sets.FriendlyFairyCanLureTo[Type] = true;

            DustType = DustID.WoodFurniture;
            AdjTiles = new int[] { TileID.Boulder };

            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            RegisterItemDrop(ItemID.None);

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.newTile.AnchorInvalidTiles = new int[] {
                TileID.MagicalIceBlock,
                TileID.Boulder,
                TileID.BouncyBoulder,
                TileID.LifeCrystalBoulder,
                TileID.RollingCactus
            };
            TileObjectData.newTile.StyleHorizontal = false;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
        }

        public static string MapChestName(string name, int i, int j)
        {
            int left = i;
            int top = j;
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX % 36 != 0)
            {
                left--;
            }

            if (tile.TileFrameY != 0)
            {
                top--;
            }

            int chest = Chest.FindChest(left, top);
            if (chest < 0)
            {
                return Language.GetTextValue("LegacyChestType.0");
            }

            if (Main.chest[chest].name == "")
            {
                return name;
            }

            return name + ": " + Main.chest[chest].name;
        }

        public override bool RightClick(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int right = i;
            int bottom = j;
            if (tile.TileFrameX % 36 == 0)
            {
                right++;
            }

            if (tile.TileFrameY == 0)
            {
                bottom++;
            }

            Projectile.NewProjectile(WorldGen.GetItemSource_FromTileBreak(right, bottom), right * 16f, bottom * 16f, 0f, 0f,
                ModContent.ProjectileType<WoodenChestBoulderProjectile>(), 70, 10f, Main.myPlayer, 0f, 0f);

            WorldGen.KillTile(i, j, false, false, true);            
            
            return true;
        }

        public override bool CanDrop(int i, int j)
        {
            return false;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ItemID.Chest;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }
    }
}
