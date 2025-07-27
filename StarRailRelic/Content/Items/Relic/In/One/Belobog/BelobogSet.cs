namespace StarRailRelic.Content.Items.Relic.In.One.Belobog
{
    public abstract class BelobogSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Belobog;

        public override void UpdateRelicSetTwo(Player player)
        {
            if (player.endurance >= 0.35f)
            {
                player.statDefense *= 1.2f;
            }
            else
            {
                player.statDefense *= 1.1f;
            }
        }
    }

    public class BelobogRelic : InRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Belobog;
    }

    public class BelobogSphere : BelobogSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class BelobogRope : BelobogSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
