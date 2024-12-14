namespace StarRailRelic.Common.Players
{
    public class CommonAccessoryPlayer : ModPlayer
    {
        public bool IsStarRailCursor { get; set; }

        public override void ResetEffects()
        {
            IsStarRailCursor = false;
        }
    }
}
