using FargowiltasSouls;
using Terraria.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoSoulsCN
{
    public class FSCNGlobalBuff : GlobalBuff
    {
        public override void ModifyBuffTip(int type, ref string tip, ref int rare)
        {
            switch (type)
            {
                case BuffID.ShadowDodge:
                    tip = tip.Replace("Eternity Mode: Dodging will reduce your attack speed", "永恒模式：闪避会降低你的攻击速度");
                    break;
                case BuffID.IceBarrier:
                    tip = tip.Replace("Eternity Mode: 10% reduced damage", "永恒模式：你造成的伤害减少10%");
                    break;
                case BuffID.ManaSickness:
                    tip = tip.Replace("Eternity Mode: Halved attack speed", "永恒模式：攻击速度减半");
                    break;
            }
        }
    }
}
