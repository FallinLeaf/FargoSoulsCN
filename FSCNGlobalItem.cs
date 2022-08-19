using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Items.Accessories.Forces;
using FargowiltasSouls.Items.Accessories.Enchantments;
using static Terraria.ModLoader.ModContent;

namespace FargoSoulsCN
{
    public class FSCNGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            void ReplaceForceTooltip(int line, string text)
            {
                tooltips.RemoveAt(line);
                tooltips.Insert(line, new TooltipLine(Mod, "tooltip_zh", $"{text}"));
            }
            if (item.type == ItemType<CosmoForce>())
            {
                ReplaceForceTooltip(10, $"[i:{ModContent.ItemType<StardustEnchant>()}] 按下“冻结”键后会冻结时间，持续9秒，有60秒冷却时间");
            }
            else if (item.type == ItemType<EarthForce>())
            {
                ReplaceForceTooltip(4, $"[i:{ModContent.ItemType<CobaltEnchant>()}] 额外获得一次爆炸二段跳，受击时你会剧烈爆炸（译者注：大地之力只有受击爆炸效果，没有爆炸跳跃效果）");
                ReplaceForceTooltip(5, $"[i:{ModContent.ItemType<MythrilEnchant>()}] 在一段时间不攻击后开始攻击会短暂地提高武器使用速度");
                ReplaceForceTooltip(10, $"[i:{ModContent.ItemType<AdamantiteEnchant>()}] 你发射的所有弹幕都会分裂成两个，造成50%伤害且伤害频率翻倍，弹幕增加与其一半伤害相等的护甲穿透");
            }
            else if (item.type == ItemType<LifeForce>())
            {
                ReplaceForceTooltip(7, $"[i:{ModContent.ItemType<SpiderEnchant>()}] 仆从和哨兵可以造成暴击，且有30%基础暴击率，但暴击伤害只有1.5倍而不是2倍");
            }
            else if (item.type == ItemType<NatureForce>())
            {
                //6, 7, new 8, 10, 11, new 13
                ReplaceForceTooltip(6, $"[i:{ModContent.ItemType<MoltenEnchant>()}] 所有敌怪在狱火圈内时多受到50%伤害");
                ReplaceForceTooltip(7, $"[i:{ModContent.ItemType<RainEnchant>()}] 召唤一个微型雨云跟着你的光标");
                tooltips.Insert(8, new TooltipLine(Mod, "tooltip_zh", $"[i:{ModContent.ItemType<RainEnchant>()}] 拥有浮游圈的效果"));
                ReplaceForceTooltip(10, $"[i:{ModContent.ItemType<SnowEnchant>()}] 按下“冻结”键后将一切冻结15秒");
                ReplaceForceTooltip(11, $"[i:{ModContent.ItemType<ChlorophyteEnchant>()}] 召唤一圈叶状水晶攻击附近的敌人");
                tooltips.Insert(13, new TooltipLine(Mod, "tooltip_zh", $"[i:{ModContent.ItemType<JungleEnchant>()}] 你能进行短程冲刺"));
            }
            else if (item.type == ItemType<ShadowForce>())
            {
                //7, 8, 9, 10, 11, new 12 13, new 16
                ReplaceForceTooltip(7, $"[i:{ModContent.ItemType<NecroEnchant>()}] 拥有骨头手套的效果");
                ReplaceForceTooltip(8, $"[i:{ModContent.ItemType<NinjaEnchant>()}] 你可以扔出烟雾弹、传送至烟雾弹的位置获得先发制人增益");
                ReplaceForceTooltip(9, $"[i:{ModContent.ItemType<CrystalAssassinEnchant>()}] 获得水晶刺客冲刺");
                ReplaceForceTooltip(10, $"[i:{ModContent.ItemType<CrystalAssassinEnchant>()}] 拥有挥发明胶的效果");
                ReplaceForceTooltip(11, $"[i:{ModContent.ItemType<SpookyEnchant>()}] 你的召唤物能进行额外的镰刀攻击");
                tooltips.Insert(12, new TooltipLine(Mod, "tooltip_zh", $"[i:{ModContent.ItemType<MonkEnchant>()}] 不攻击可以进行一次武僧冲刺"));
                tooltips.Insert(13, new TooltipLine(Mod, "tooltip_zh", $"[i:{ModContent.ItemType<ShinobiEnchant>()}] 朝墙壁冲刺时会直接穿过去"));
                tooltips.Insert(16, new TooltipLine(Mod, "tooltip)zh", $"[i:{ModContent.ItemType<DarkArtistEnchant>()}] 召唤一个爆炸烈焰仆从，在充能完毕后会移动至光标位置"));
            }
            else if (item.type == ItemType<SpiritForce>())
            {
                ReplaceForceTooltip(4, $"[i:{ModContent.ItemType<FossilEnchant>()}] 受到致死伤害时会以200生命值重生并爆出几根骨头");
                ReplaceForceTooltip(5, $"[i:{ModContent.ItemType<FossilEnchant>()}] 接触停止移动的骨头时会回复20点生命值");
                ReplaceForceTooltip(10, $"[i:{ModContent.ItemType<AncientHallowEnchant>()}] 大幅提升召唤物攻击速度，但攻击力会降低，且会增加20点护甲穿透");
            }
            else if (item.type == ItemType<TerraForce>())
            {
                //4,5,new 6
                ReplaceForceTooltip(7, $"[i:{ModContent.ItemType<TinEnchant>()}] 被击中后会降低暴击率");
                ReplaceForceTooltip(8, $"[i:{ModContent.ItemType<IronEnchant>()}] 右键进行盾牌格挡");
                tooltips.Insert(9, new TooltipLine(Mod, "tooltip_zh", $"[i:{ModContent.ItemType<IronEnchant>()}] 在受伤前格挡以格挡此次攻击"));
            }
            else if (item.type == ItemType<WillForce>())
            {
                ReplaceForceTooltip(8, $"[i:{ModContent.ItemType<RedRidingEnchant>()}] 连续攻击命中会获得额外伤害加成");
            }
        }
    }
}
