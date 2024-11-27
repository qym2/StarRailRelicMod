namespace StarRailRelic.Content.Items.Relic.In.One.Enterprise
{
    public abstract class EnterpriseSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Enterprise;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.endurance += 4 / 100f;
            player.GetDamage<GenericDamageClass>() += player.endurance / 2;
        }
    }

    public class EnterpriseSphere : EnterpriseSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class EnterpriseRope : EnterpriseSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
