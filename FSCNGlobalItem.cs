using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Items.Accessories.Forces;
using FargowiltasSouls.Items.Accessories.Enchantments;
using static Terraria.ModLoader.ModContent;
using static FargoSoulsCN.Utils;

namespace FargoSoulsCN
{
    public class FSCNGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            void ReplaceForceTooltip(string old, string New)
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Text.Contains(old))
                    {
                        tooltips.Remove(tooltips[i]);
                        tooltips.Insert(i, new TooltipLine(Mod, "tooltip_zh", New));
                        break;
                    }
                }
            }
            if (item.type == ItemType<CosmoForce>())
            {
                ReplaceForceTooltip($"[i:{ItemType<StardustEnchant>()}] 按下“冻结”键后会冻结时间，持续5秒，有60秒冷却时间", $"[i:{ItemType<StardustEnchant>()}] 按下“冻结”键后会冻结时间，持续9秒，有60秒冷却时间");
            }
            else if (item.type == ItemType<EarthForce>())
            {
                ReplaceForceTooltip($"[i:{ItemType<CobaltEnchant>()}] 你的弹幕有25%几率爆裂成碎片", $"[i:{ItemType<CobaltEnchant>()}] 额外获得一次爆炸二段跳，受击时你会剧烈爆炸（译者注：大地之力只有受击爆炸效果，没有爆炸跳跃效果）");
                ReplaceForceTooltip($"[i:{ItemType<MythrilEnchant>()}] 武器攻击速度增加20%", $"[i:{ItemType<MythrilEnchant>()}] 在一段时间不攻击后开始攻击会短暂地提高武器使用速度");
                tooltips.InsertAfter($"[i:{ItemType<MythrilEnchant>()}] 在一段时间不攻击后开始攻击会短暂地提高武器使用速度", new TooltipLine(Mod, "tooltip_zh", $"[i:{ItemType<MythrilEnchant>()}] 使用速度加成会在攻击5秒后消失，并在5秒不攻击后恢复"));
                ReplaceForceTooltip($"[i:{ItemType<AdamantiteEnchant>()}] 你每次发射的第二个弹幕会分裂成三个", $"[i:{ItemType<AdamantiteEnchant>()}] 你发射的所有弹幕都会分裂成两个，造成50%伤害且伤害频率翻倍，弹幕增加与其一半伤害相等的护甲穿透");
            }
            else if (item.type == ItemType<LifeForce>())
            {
                ReplaceForceTooltip($"[i:{ItemType<SpiderEnchant>()}] 仆从和哨兵可以造成暴击，且有30%基础暴击率", $"[i:{ItemType<SpiderEnchant>()}] 仆从和哨兵可以造成暴击，且有30%基础暴击率，但暴击伤害只有1.5倍而不是2倍");
            }
            else if (item.type == ItemType<NatureForce>())
            {
                //6, 7, new 8, 10, 11, new 13
                ReplaceForceTooltip($"[i:{ItemType<MoltenEnchant>()}] 你受到伤害时会剧烈爆炸并伤害附近的敌人", $"[i:{ItemType<MoltenEnchant>()}] 所有敌怪在狱火圈内时多受到50%伤害");
                ReplaceForceTooltip($"[i:{ItemType<RainEnchant>()}] 召唤一个微型雨云跟着你", $"[i:{ItemType<RainEnchant>()}] 召唤一个微型雨云跟着你的光标");
                tooltips.InsertAfter($"[i:{ItemType<RainEnchant>()}] 召唤一个微型雨云跟着你的光标", new TooltipLine(Mod, "tooltip_zh", $"[i:{ItemType<RainEnchant>()}] 拥有浮游圈的效果"));
                ReplaceForceTooltip($"[i:{ItemType<SnowEnchant>()}] 在你周围生成一个可以将弹幕速度减半的冰雪光环", $"[i:{ItemType<SnowEnchant>()}] 按下“冻结”键后将一切冻结15秒");
                ReplaceForceTooltip($"[i:{ItemType<ChlorophyteEnchant>()}] 召唤一圈叶状攻击附近的敌人", $"[i:{ItemType<ChlorophyteEnchant>()}] 召唤一圈叶状水晶攻击附近的敌人");
                tooltips.InsertAfter($"[i:{ItemType<JungleEnchant>()}] 使你获得孢子二段跳能力", new TooltipLine(Mod, "tooltip_zh", $"[i:{ItemType<JungleEnchant>()}] 你能进行短程冲刺"));
            }
            else if (item.type == ItemType<ShadowForce>())
            {
                //7, 8, 9, 10, 11, new 12 13, new 16
                ReplaceForceTooltip($"[i:{ItemType<CrystalAssassinEnchant>()}] 你的召唤物能进行额外的镰刀攻击", $"[i:{ItemType<NecroEnchant>()}] 拥有骨头手套的效果");
                ReplaceForceTooltip($"[i:{ItemType<MonkEnchant>()}] 每隔几秒，你能进行一次武僧冲刺", $"[i:{ItemType<CrystalAssassinEnchant>()}] 获得水晶刺客冲刺");
                ReplaceForceTooltip($"[i:{ItemType<SpookyEnchant>()}] 朝墙壁冲刺时会直接穿过去", $"[i:{ItemType<CrystalAssassinEnchant>()}] 拥有挥发明胶的效果");
                ReplaceForceTooltip($"[i:{ItemType<ShinobiEnchant>()}] 召唤一个爆炸烈焰仆从，在充能完毕后会移动至光标位置", $"[i:{ItemType<SpookyEnchant>()}] 你的召唤物能进行额外的镰刀攻击");
                tooltips.InsertAfter($"[i:{ItemType<SpookyEnchant>()}] 你的召唤物能进行额外的镰刀攻击", new TooltipLine(Mod, "tooltip_zh", $"[i:{ItemType<MonkEnchant>()}] 不攻击可以进行一次武僧冲刺"));
                tooltips.InsertAfter($"[i:{ItemType<MonkEnchant>()}] 不攻击可以进行一次武僧冲刺", new TooltipLine(Mod, "tooltip_zh", $"[i:{ItemType<ShinobiEnchant>()}] 朝墙壁冲刺时会直接穿过去"));
                tooltips.InsertAfter($"[i:{ItemType<ApprenticeEnchant>()}] 切换武器后，下次攻击的伤害增加150%", new TooltipLine(Mod, "tooltip)zh", $"[i:{ItemType<DarkArtistEnchant>()}] 召唤一个爆炸烈焰仆从，在充能完毕后会移动至光标位置"));
            }
            else if (item.type == ItemType<SpiritForce>())
            {
                ReplaceForceTooltip($"[i:{ItemType<FossilEnchant>()}] 受到致死伤害时会以50生命值重生并爆出几根骨头", $"[i:{ItemType<FossilEnchant>()}] 受到致死伤害时会以200生命值重生并爆出几根骨头");
                ReplaceForceTooltip($"[i:{ItemType<FossilEnchant>()}] 接触停止移动的骨头时会回复15点生命值", $"[i:{ItemType<FossilEnchant>()}] 接触停止移动的骨头时会回复20点生命值");
                ReplaceForceTooltip($"[i:{ItemType<AncientHallowEnchant>()}] 大大增加召唤物攻击速度，但攻击力会降低", $"[i:{ItemType<AncientHallowEnchant>()}] 大幅提升召唤物攻击速度，但攻击力会降低，且会增加20点护甲穿透");
            }
            else if (item.type == ItemType<TerraForce>())
            {
                //4,5,new 6
                ReplaceForceTooltip($"[i:{ItemType<IronEnchant>()}] 被击中后会降低暴击率", $"[i:{ItemType<IronEnchant>()}] 右键进行盾牌格挡");
                ReplaceForceTooltip($"[i:{ItemType<IronEnchant>()}] 右键进行盾牌格挡，如果时机正确则抵消这次伤害", $"[i:{ItemType<IronEnchant>()}] 在受伤前格挡以格挡此次攻击");
                tooltips.InsertAfter($"[i:{ItemType<ObsidianEnchant>()}] 你的攻击会引发爆炸", new TooltipLine(Mod, "tooltip_zh", $"[i:{ItemType<ObsidianEnchant>()}] 鞭子的范围增加50%"));
            }
            else if (item.type == ItemType<WillForce>())
            {
                ReplaceForceTooltip($"[i:{ItemType<RedRidingEnchant>()}] 双击'下'键后令箭雨倾斜在光标位置", $"[i:{ItemType<RedRidingEnchant>()}] 连续攻击命中会获得额外伤害加成");
            }
        }
    }
    public static class Utils
    {
        public static void InsertAfter(this List<TooltipLine> tooltips, string text, TooltipLine line)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Text.Contains(text))
                    tooltips.Insert(i + 1, line);
            }
        }
    }
}
