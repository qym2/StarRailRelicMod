namespace StarRailRelic.Content.Items.Relic.In.One.Glamoth
{
    public abstract class GlamothSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Glamoth;

        public override void ModifyTwoSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsGlamothTwoSet = true;
        }
    }

    public class GlamothRelic : InRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Glamoth;
    }

    public class GlamothSphere : GlamothSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class GlamothRope : GlamothSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
