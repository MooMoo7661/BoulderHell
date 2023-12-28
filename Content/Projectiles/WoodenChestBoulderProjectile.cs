using BoulderHell.Content.Configs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace BoulderHell.Content.Projectiles
{
    public class WoodenChestBoulderProjectile : ModProjectile
    {
        int lifeTime = 0;

        public override void SetDefaults()
        {
            Projectile.aiStyle = ProjAIStyleID.Boulder;
            Projectile.width = 32;
            Projectile.height = 28;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = 10;
            Projectile.damage = 80;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.scale = 1f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void AI()
        {
            TimerConfig config = ModContent.GetInstance<TimerConfig>();

            lifeTime++;
            if (lifeTime >= 300)
            {
                if (config.ChestBouldersDropLoot)
                {
                    foreach (Item item in CalculateChestLoot())
                    {
                        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(Projectile.position.ToTileCoordinates().X, Projectile.position.ToTileCoordinates().Y), Projectile.position, item);
                    }
                }

                SoundEngine.PlaySound(SoundID.Tink, Projectile.position);

                Projectile explosion = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.position, Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 30, 5f);
                explosion.hostile = true;
                explosion.friendly = false;
                explosion.scale = 1.2f;

                if (config.ChestBouldersGrief && (!Projectile.wet || (Projectile.wet && config.ChestBouldersGriefInWater)))
                    ExplodeNearbyTiles(Projectile.position.ToTileCoordinates());

                Projectile.Kill();
            }
        }

        public static void ExplodeNearbyTiles(Point position)
        {
            for (int i = -3; i < 3; i++)
            {
                for (int j = -3; j < 3; j++)
                {
                    WorldGen.KillTile(position.X + i, position.Y + j);
                }
            }
        }

        public static List<Item> CalculateChestLoot()
        {
            List<Item> loot = new List<Item>();

            switch(Main.rand.Next(0, 12))
            {
                case 0:
                    loot.Add(new Item(ItemID.Spear));
                    break;

                case 1:
                    loot.Add(new Item(ItemID.Blowpipe));
                    break;

                case 2:
                    loot.Add(new Item(ItemID.WoodenBoomerang));
                    break;

                case 3:
                    loot.Add(new Item(ItemID.Glowstick, Main.rand.Next(40, 75)));
                    break;

                case 4:
                    loot.Add(new Item(ItemID.ThrowingKnife, Main.rand.Next(150, 300)));
                    break;

                case 5:
                    loot.Add(new Item(ItemID.Aglet));
                    break;

                case 6:
                    loot.Add(new Item(ItemID.ClimbingClaws));
                    break;

                case 7:
                    loot.Add(new Item(ItemID.Umbrella));
                    break;

                case 8:
                    loot.Add(new Item(ItemID.CordageGuide));
                    break;

                case 9:
                    loot.Add(new Item(ItemID.WandofSparking));
                    break;

                case 10:
                    loot.Add(new Item(ItemID.Radar));
                    break;

                case 11:
                    loot.Add(new Item(ItemID.PortableStool));
                    break;
            }

            //Secondary loot
            if (Main.rand.NextBool(6))
                loot.Add(new Item(ItemID.HerbBag, Main.rand.Next(1, 5)));
            if (Main.rand.NextBool(6))
                loot.Add(new Item(ItemID.CanOfWorms, Main.rand.Next(1, 5)));
            if (Main.rand.NextBool(3))
                loot.Add(new Item(ItemID.Grenade, Main.rand.Next(3, 6)));
            if (Main.rand.NextBool())
                loot.Add(new Item(ItemID.Rope, Main.rand.Next(50, 101)));
            if (Main.rand.NextBool())
                loot.Add(new Item(ItemID.HealingPotion, Main.rand.Next(3, 6)));
            if (!Main.rand.NextBool(3))
                loot.Add(new Item(ItemID.RecallPotion, Main.rand.Next(3, 6)));
            if (Main.rand.NextBool())
                loot.Add(new Item(ItemID.SilverCoin, Main.rand.Next(10, 30)));
            if (Main.rand.NextBool())
                loot.Add(new Item(ItemID.Wood, Main.rand.Next(50, 100)));

            if (!Main.rand.NextBool(3))
            {
                if (Main.rand.NextBool())
                {
                    loot.Add(new Item(ItemID.WoodenArrow, Main.rand.Next(25, 51)));
                }
                else
                {
                    loot.Add(new Item(ItemID.Shuriken, Main.rand.Next(25, 51)));
                }
            }

            if (!Main.rand.NextBool())
            {
                if (Main.rand.NextBool())
                {
                    loot.Add(new Item(ItemID.Torch, Main.rand.Next(10, 21)));
                }
                else
                {
                    loot.Add(new Item(ItemID.Bottle, Main.rand.Next(10, 21)));
                }
            }

            if (Main.rand.NextBool(2))
            {
                if (GenVars.copper == TileID.Copper)
                {
                    loot.Add(new Item(ItemID.CopperBar, Main.rand.Next(3, 11)));
                }
                else
                {
                    loot.Add(new Item(ItemID.TinBar, Main.rand.Next(3, 11)));
                }
            }

            if (Main.rand.NextBool(2))
            {
                if (GenVars.iron == TileID.Iron)
                {
                    loot.Add(new Item(ItemID.IronBar, Main.rand.Next(3, 11)));
                }
                else
                {
                    loot.Add(new Item(ItemID.LeadBar, Main.rand.Next(3, 11)));
                }
            }

            if (!Main.rand.NextBool(3))
            {
                if (Main.rand.Next(0, 7) == 1)
                    loot.Add(new Item(ItemID.IronskinPotion, Main.rand.Next(0, 2)));
                else if (Main.rand.Next(0, 7) == 2)
                    loot.Add(new Item(ItemID.ShinePotion, Main.rand.Next(0, 2)));
                else if (Main.rand.Next(0, 7) == 3)
                    loot.Add(new Item(ItemID.NightOwlPotion, Main.rand.Next(0, 2)));
                else if (Main.rand.Next(0, 7) == 4)
                    loot.Add(new Item(ItemID.SwiftnessPotion, Main.rand.Next(0, 2)));
                else if (Main.rand.Next(0, 7) == 5)
                    loot.Add(new Item(ItemID.MiningPotion, Main.rand.Next(0, 2)));
                else
                    loot.Add(new Item(ItemID.BuilderPotion, Main.rand.Next(0, 2)));
            }

            return loot;
        }
    }
}
