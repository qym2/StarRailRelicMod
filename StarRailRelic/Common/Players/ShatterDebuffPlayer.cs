namespace StarRailRelic.Common.Players
{
    public class ShatterDebuffPlayer : ModPlayer
    {
        public bool isDebuff;

        public override void ResetEffects()
        {
            isDebuff = false;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (isDebuff)
            {
                g *= 0.32f;
                r *= 0.66f;
            }
        }
    }
}
