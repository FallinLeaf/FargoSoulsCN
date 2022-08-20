using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace FargoSoulsCN
{
    public class ChatMessageTranslate : ModSystem
    {
        public override void Load()
        {
            On.Terraria.Main.NewText_string_byte_byte_byte += Main_NewText_string_byte_byte_byte;
        }

        private static void Main_NewText_string_byte_byte_byte(On.Terraria.Main.orig_NewText_string_byte_byte_byte orig, string newText, byte R = byte.MaxValue, byte G = byte.MaxValue, byte B = byte.MaxValue)
        {
            if (newText == "皇家工蜂 已苏醒！" && R == 175 && G == 75 && B == 255)
                orig.Invoke("皇家工蜂已苏醒！", R, G, B);
            else if (newText == "机械骷髅王 的手臂又长出来了！" && R == 175 && G == 75 && B == 255)
                orig.Invoke("机械骷髅王的手臂又长出来了！", R, G, B);
            else if (newText == "机械骷髅王 进入了地牢守卫阶段！" && R == 175 && G == 75 && B == 255)
                orig.Invoke("机械骷髅王进入了地牢守卫阶段！", R, G, B);
            else if (newText == "骷髅王 的手臂又长出来了！" && R == 175 && G == 75 && B == 255)
                orig.Invoke("骷髅王的手臂又长出来了！", R, G, B);
            else if (newText == "骷髅王 进入了地牢守卫阶段！" && R == 175 && G == 75 && B == 255)
                orig.Invoke("骷髅王进入了地牢守卫阶段！", R, G, B);
            else
                orig.Invoke(newText, R, G, B);
        }

        public override void Unload()
        {
            On.Terraria.Main.NewText_string_byte_byte_byte -= Main_NewText_string_byte_byte_byte;
        }
    }
}
