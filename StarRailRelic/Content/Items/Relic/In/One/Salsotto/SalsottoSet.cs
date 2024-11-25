namespace StarRailRelic.Content.Items.Relic.In.One.Salsotto
{
    public abstract class SalsottoSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Salsotto;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetCritChance<GenericDamageClass>() += 4f;

            if (player.GetTotalCritChance<MeleeDamageClass>() >= 50)
            {
                player.GetDamage<MeleeDamageClass>() += 8f / 100f;
            }
            if (player.GetTotalCritChance<MagicDamageClass>() >= 50)
            {
                player.GetDamage<MagicDamageClass>() += 8f / 100f;
            }
        }
    }

    public class SalsottoSphere : SalsottoSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class SalsottoRope : SalsottoSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
