namespace StarRailRelic.Content.Items.Relic.In.Two.Kalpagni
{
    public abstract class KalpagniSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Kalpagni;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<MagicDamageClass>() += 6 / 100f; 

            if (player.HasBuff(BuffID.ManaSickness))
            {
                if (player.buffTime[player.FindBuffIndex(BuffID.ManaSickness)] > 300)
                {
                    player.manaCost -= 0.30f;
                }
                else
                {
                    player.manaCost -= 0.10f;
                }
            }
        }
    }

    public class KalpagniSphere : KalpagniSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class KalpagniRope : KalpagniSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
