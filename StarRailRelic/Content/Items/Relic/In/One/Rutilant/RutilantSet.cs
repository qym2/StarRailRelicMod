namespace StarRailRelic.Content.Items.Relic.In.One.Rutilant
{
    public abstract class RutilantSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Rutilant;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetCritChance<GenericDamageClass>() += 4f;

            if (player.GetTotalCritChance<SummonDamageClass>() >= 50)
            {
                player.GetDamage<SummonDamageClass>() += 8f / 100f;
            }
            if (player.GetTotalCritChance<RangedDamageClass>() >= 50)
            {
                player.GetDamage<RangedDamageClass>() += 8f / 100f;
            }
        }
    }

    public class RutilantSphere : RutilantSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class RutilantRope : RutilantSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
