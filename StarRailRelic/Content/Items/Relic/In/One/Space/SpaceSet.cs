namespace StarRailRelic.Content.Items.Relic.In.One.Space
{
    public abstract class SpaceSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Space;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetAttackSpeed<MeleeDamageClass>() += (6 + player.maxMinions * 3) / 100f;
            player.GetAttackSpeed<SummonMeleeSpeedDamageClass>() += (6 + player.maxMinions * 3) / 100f;
        }
    }

    public class SpaceRelic : InRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Space;
    }

    public class SpaceSphere : SpaceSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class SpaceRope : SpaceSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
