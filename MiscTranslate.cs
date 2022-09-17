using FargowiltasSouls;
using FargowiltasSouls.Buffs.Boss;
using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.Buffs.Souls;
using FargowiltasSouls.Buffs;
using FargowiltasSouls.Items.Accessories.Enchantments;
using FargowiltasSouls.Projectiles;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework.Input;
using FargowiltasSouls.EternityMode.Content.Enemy.Jungle;
using MonoMod.Utils;
using FargowiltasSouls.UI;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Projectiles.Masomode;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.NPCs.DeviBoss;
using FargowiltasSouls.EternityMode.Content.Boss.HM;

namespace FargoSoulsCN
{
    public class MiscTranslate : ModSystem
    {
        public List<ILHook> ilHooks;
        public List<Hook> hooks;
        public override void Load()
        {
            On.Terraria.Main.DrawInterface_33_MouseText += Main_DrawInterface_33_MouseText;
            On.Terraria.UI.Chat.ChatManager.ParseMessage += ChatManager_ParseMessage;
            hooks = new List<Hook>();
            ilHooks = new List<ILHook>();
            MonoModHooks.RequestNativeAccess();
            hooks.Add(new Hook(typeof(FargoSoulsPlayer).GetMethod("PreKill"), PreKill));
            hooks.Add(new Hook(typeof(Fused).GetMethod("Update", new Type[] { typeof(Player), typeof(int).MakeByRefType() }), Update));
            hooks.Add(new Hook(typeof(FakeHeart).GetMethod("CanHitPlayer"), CanHitPlayer));
            void ILTranslate(Type type, string methodName, string en, string zh, Type[] types, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance)
            {
                ilHooks.Add(new ILHook(type.GetMethod(methodName, flags, types), new ILContext.Manipulator(il =>
                {
                    var c = new ILCursor(il);
                    if (!c.TryGotoNext(i => i.MatchLdstr(en)))
                        return;
                    c.Index++;
                    c.Emit(OpCodes.Pop);
                    c.Emit(OpCodes.Ldstr, zh);
                })));
            }
            ILTranslate(typeof(Derpling), "AI", " was sucked dry.", "被吸干了。", new Type[] { typeof(NPC) });
            ILTranslate(typeof(UISearchBar), "DrawChildren", "Search...", "搜索……", new Type[] { typeof(SpriteBatch) }, BindingFlags.NonPublic | BindingFlags.Instance);
            #region Betsy
            void BetsyTranslate(string en, string zh, bool twice = false)
            {
                ilHooks.Add(new ILHook(typeof(Betsy).GetMethod("PreAI"), new ILContext.Manipulator(il =>
                {
                    var c = new ILCursor(il);
                    if (!c.TryGotoNext(i => i.MatchLdstr(en)))
                        return;
                    c.Index++;
                    c.Emit(OpCodes.Pop);
                    c.Emit(OpCodes.Ldstr, zh);
                    if (twice)
                    {
                        if (!c.TryGotoNext(i => i.MatchLdstr(en)))
                            return;
                        c.Index++;
                        c.Emit(OpCodes.Pop);
                        c.Emit(OpCodes.Ldstr, zh);
                    }
                })));
            }
            BetsyTranslate("CRINGE", "你怕了？");
            BetsyTranslate("<User ", "<玩家", true);
            #endregion
            foreach (MethodInfo info in typeof(Fused).GetMethods())
            {

                if (info.Name == "Update")
                {
                    Mod.Logger.Warn(info.GetParameters());

                }
            }
            foreach (ILHook iLHook in ilHooks)
            {
                if (iLHook is not null)
                    iLHook.Apply();
            }
            foreach (Hook hook in hooks)
            {
                if (hook is not null)
                    hook.Apply();
            }
        }

        private List<Terraria.UI.Chat.TextSnippet> ChatManager_ParseMessage(On.Terraria.UI.Chat.ChatManager.orig_ParseMessage orig, string text, Color baseColor)
        {
            if (text == "Eternity Mode is enabled")
                text = "永恒模式已开启";
            if (text == "Masochist Mode is enabled")
                text = "受虐模式已开启";
            return orig.Invoke(text, baseColor);
        }
        private static void Main_DrawInterface_33_MouseText(On.Terraria.Main.orig_DrawInterface_33_MouseText orig, Main self)
        {
            if (Main.hoverItemName == "Configure Accessory Effects")
                Main.hoverItemName = "设置饰品效果";
            orig.Invoke(self);
        }

        private static bool PreKill(FargoSoulsPlayer orig, double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            bool retVal = true;

            if (orig.Player.statLife <= 0) //revives
            {
                if (orig.Player.whoAmI == Main.myPlayer && retVal && orig.AbomRebirth)
                {
                    if (!orig.WasHurtBySomething)
                    {
                        orig.Player.statLife = 1;
                        return false; //short circuits the rest, this is deliberate
                    }
                }

                if (orig.Player.whoAmI == Main.myPlayer && retVal && orig.MutantSetBonusItem != null && orig.Player.FindBuffIndex(ModContent.BuffType<MutantRebirth>()) == -1)
                {
                    orig.Player.statLife = orig.Player.statLifeMax2;
                    orig.Player.HealEffect(orig.Player.statLifeMax2);
                    orig.Player.immune = true;
                    orig.Player.immuneTime = 180;
                    orig.Player.hurtCooldowns[0] = 180;
                    orig.Player.hurtCooldowns[1] = 180;
                    string text = Language.GetTextValue($"Mods.{orig.Mod.Name}.Message.Revived");
                    Main.NewText(text, Color.LimeGreen);
                    orig.Player.AddBuff(ModContent.BuffType<MutantRebirth>(), 10800);
                    retVal = false;

                    Projectile.NewProjectile(orig.Player.GetSource_Accessory(orig.MutantSetBonusItem), orig.Player.Center, -Vector2.UnitY, ModContent.ProjectileType<GiantDeathray>(), (int)(7000 * orig.Player.ActualClassDamage(DamageClass.Magic)), 10f, orig.Player.whoAmI);
                }

                if (orig.Player.whoAmI == Main.myPlayer && retVal && orig.FossilEnchantItem != null && orig.Player.FindBuffIndex(ModContent.BuffType<FossilReviveCD>()) == -1)
                {
                    FossilEnchant.FossilRevive(orig.Player.GetModPlayer<FargoSoulsPlayer>());
                    retVal = false;
                }

                if (orig.Player.whoAmI == Main.myPlayer && retVal && orig.AbomWandItem != null && !orig.AbominableWandRevived)
                {
                    orig.AbominableWandRevived = true;
                    int heal = 1;
                    orig.Player.statLife = heal;
                    orig.Player.HealEffect(heal);
                    orig.Player.immune = true;
                    orig.Player.immuneTime = 120;
                    orig.Player.hurtCooldowns[0] = 120;
                    orig.Player.hurtCooldowns[1] = 120;
                    string text = Language.GetTextValue($"Mods.{orig.Mod.Name}.Message.Revived");
                    CombatText.NewText(orig.Player.Hitbox, Color.Yellow, text, true);
                    Main.NewText(text, Color.Yellow);
                    orig.Player.AddBuff(ModContent.BuffType<AbomRebirth>(), 300);
                    retVal = false;
                    for (int i = 0; i < 24; i++)
                    {
                        Projectile.NewProjectile(orig.Player.GetSource_Accessory(orig.AbomWandItem), orig.Player.Center, Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(4f, 16f),
                            ModContent.ProjectileType<StyxArmorScythe2>(), 0, 10f, Main.myPlayer, -60 - Main.rand.Next(60), -1);
                    }
                }
            }
            
            PlayerDeathReason DeathByLocalizationn(string key)
            {
                string death = Language.GetTextValue($"Mods.FargowiltasSouls.DeathMessage.{key}");
                return PlayerDeathReason.ByCustomReason($"{orig.Player.name}{death}");
            }

            //killed by damage over time
            if (damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                if (orig.GodEater || orig.FlamesoftheUniverse || orig.CurseoftheMoon || orig.MutantFang)
                    damageSource = DeathByLocalizationn("DivineWrath");

                if (orig.Infested)
                    damageSource = DeathByLocalizationn("Infested");

                if (orig.Anticoagulation)
                    damageSource = DeathByLocalizationn("Anticoagulation");

                if (orig.Rotting)
                    damageSource = DeathByLocalizationn("Rotting");

                if (orig.Shadowflame)
                    damageSource = DeathByLocalizationn("Shadowflame");

                if (orig.NanoInjection)
                    damageSource = DeathByLocalizationn("NanoInjection");
            }

            /*if (MutantPresence)
            {
                damageSource = PlayerDeathReason.ByCustomReason(orig.Player.name + " was penetrated.");
            }*/

            if (orig.StatLifePrevious > 0 && orig.Player.statLife > orig.StatLifePrevious)
                orig.StatLifePrevious = orig.Player.statLife;

            return retVal;
        }
        private static void Update(Fused orig, Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoSoulsPlayer>().Fused = true;

            if (player.buffTime[buffIndex] == 2)
            {
                player.immune = false;
                player.immuneTime = 0;
                int damage = (int)(Math.Max(player.statLife, player.statLifeMax) * 2.0 / 3.0);
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + "被炸成了碎片。"), damage, 0, false, false, true);
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<FusedExplosion>(), damage, 12f, Main.myPlayer);
            }
        }
        private static bool CanHitPlayer(FakeHeart orig, Player target)
        {
            if (orig.Projectile.Colliding(orig.Projectile.Hitbox, target.Hitbox))
            {
                if (target.GetModPlayer<FargoSoulsPlayer>().DevianttHeartItem == null)
                {
                    target.hurtCooldowns[0] = 0;
                    int defense = target.statDefense;
                    float endurance = target.endurance;
                    target.statDefense = 0;
                    target.endurance = 0;
                    target.Hurt(PlayerDeathReason.ByCustomReason(target.name + "感到了心碎。"), orig.Projectile.damage, 0, false, false, false, 0);
                    target.statDefense = defense;
                    target.endurance = endurance;

                    if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<DeviBoss>()))
                        target.AddBuff(ModContent.BuffType<Lovestruck>(), 240);
                }
                else
                {
                    target.statLife += 1;
                    target.HealEffect(1);
                }

                orig.Projectile.timeLeft = 0;
            }
            return false;
        }

        public override void Unload()
        {
            On.Terraria.Main.DrawInterface_33_MouseText -= Main_DrawInterface_33_MouseText;
            On.Terraria.UI.Chat.ChatManager.ParseMessage -= ChatManager_ParseMessage;
            foreach (ILHook iLHook in ilHooks)
            {
                if (iLHook is not null)
                    iLHook.Dispose();
            }
            ilHooks = null;
            foreach (Hook hook in hooks)
            {
                if (hook is not null)
                    hook.Dispose();
            }
            hooks = null;
        }
    }
}