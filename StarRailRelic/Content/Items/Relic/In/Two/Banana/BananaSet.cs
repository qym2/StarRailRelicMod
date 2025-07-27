namespace StarRailRelic.Content.Items.Relic.In.Two.Banana
{
    public abstract class BananaSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Banana;

        public override void ModifyTwoSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsBananaTwoSet = true;
        }
    }

    public class BananaRelic : InRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Banana;
    }

    public class BananaSphere : BananaSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class BananaRope : BananaSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
