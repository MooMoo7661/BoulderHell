using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace BoulderHell.Content.Configs
{
    public class TimerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(300)]
        [Range(300, 600)]
        [Slider]
        public int ChestExplosionTimer { get; set; }

        [DefaultValue(true)]
        public bool ChestBouldersDropLoot { get; set; }

        [DefaultValue(true)]
        public bool ChestBouldersGrief { get; set; }

        [DefaultValue(true)]
        public bool ChestBouldersGriefInWater { get; set; }
    }
}
