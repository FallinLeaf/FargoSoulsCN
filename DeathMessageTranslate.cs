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

namespace FargoSoulsCN
{
    public class DeathMessageTranslate : ModSystem
    {
        //public List<ILHook> ilHooks;
        public List<Hook> hooks;
        public override void Load()
        {
            hooks = new List<Hook>();
            MonoModHooks.RequestNativeAccess();
            hooks.Add(new Hook(typeof(FargoSoulsPlayer).GetMethod("PreKill"), PreKill));

            /*foreach (ILHook iLHook in ilHooks)
            {
                if (iLHook is not null)
                    iLHook.Apply();
            }*/
            foreach (Hook hook in hooks)
            {
                if (hook is not null)
                    hook.Apply();
            }
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
        public override void Unload()
        {
            /*foreach (ILHook iLHook in ilHooks)
            {
                if (iLHook is not null)
                    iLHook.Dispose();
            }
            ilHooks = null;*/
            foreach (Hook hook in hooks)
            {
                if (hook is not null)
                    hook.Dispose();
            }
            hooks = null;
        }
    }
}