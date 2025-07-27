namespace StarRailRelic.Content.Items.Relic.In.One.Keel
{
    public abstract class KeelSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Keel;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.endurance += 4 / 100f;
        }

        public override void ModifyTwoSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsKeelTwoSet = true;
        }
    }

    public class KeelRelic : InRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Keel;
    }

    public class KeelSphere : KeelSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class KeelRope : KeelSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
