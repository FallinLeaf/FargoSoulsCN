using FargowiltasSouls;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using FargowiltasSouls.Buffs.Masomode;

namespace FargoSoulsCN
{
    public class FSCNPlayer : ModPlayer
    {
        public override void UpdateDead()
        {
            if (Player.respawnTimer > 0)
                Player.respawnTimer -= 6;
        }
    }
}