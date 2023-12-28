using BoulderHell.Content.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static tModPorter.ProgressUpdate;

namespace BoulderHell.Content.WorldGeneration
{
    public class WoodenChestBoulderSpawning : ModSystem
    {
        public bool[] canHaveFakeChest = ItemID.Sets.Factory.CreateBoolSet(TileID.Grass, TileID.Stone, TileID.ClayBlock, TileID.Dirt, TileID.SnowBlock, TileID.IceBlock, TileID.Mud, TileID.JungleGrass, TileID.LivingWood, TileID.Marble, TileID.Granite);
        public bool[] canHaveFakeChestInFront = ItemID.Sets.Factory.CreateBoolSet(WallID.DirtUnsafe, WallID.FlowerUnsafe, WallID.MudUnsafe, WallID.SnowWallUnsafe);
        public override void PostWorldGen()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    Tile tile3 = Framing.GetTileSafely(x + 1, y);

                    if (y < Main.worldSurface + 100 && y > Main.worldSurface - 300 && canHaveFakeChestInFront[tile.WallType] && canHaveFakeChestInFront[tile3.WallType])
                    {
                        if (Main.rand.NextBool(30) && canHaveFakeChest[tile.TileType] && canHaveFakeChest[tile3.TileType] && tile3.HasTile && tile.HasTile)
                        {
                            WorldGen.PlaceObject(x, y - 1, ModContent.TileType<WoodenChestBoulderTile>(), true);
                        }
                    }
                }
            }

        }
    }
}
