/*using FargowiltasSouls;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargoSoulsCN
{
    public class FSCNPlayer : ModPlayer
    {
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            FargoSoulsPlayer fargoSoulsPlayer = Player.GetModPlayer<FargoSoulsPlayer>();
            bool MatchDamageSource(string source)
            {
                var name = fargoSoulsPlayer.GetType().GetField(source);
                bool value = (bool)name.GetValue(fargoSoulsPlayer);
                if (value)
                    return true;
                return false;
            }
            PlayerDeathReason DeathByLocalization(string key)
            {
                string death = Language.GetTextValue($"Mods.FargowiltasSouls.DeathMessage.{key}");
                return PlayerDeathReason.ByCustomReason($"{Player.name}{death}");
            }
            bool dot = damage == 10.0 && hitDirection == 0;
            if ((MatchDamageSource("GodEater") || MatchDamageSource("FlamesoftheUniverse") || MatchDamageSource("CurseoftheMoon") || MatchDamageSource("MutantFang")) && dot)
                damageSource = DeathByLocalization("DivineWrath");

            if (MatchDamageSource("Infested") && dot)
                damageSource = DeathByLocalization("Infested");

            if (MatchDamageSource("Anticoagulation") && dot)
                damageSource = DeathByLocalization("Anticoagulation");

            if (MatchDamageSource("Rotting") && dot)
                damageSource = DeathByLocalization("Rotting");

            if (MatchDamageSource("Shadowflame") && dot)
                damageSource = DeathByLocalization("Shadowflame");

            if (MatchDamageSource("NanoInjection") && dot)
                damageSource = DeathByLocalization("NanoInjection");
            return true;
        }
    }
}*/