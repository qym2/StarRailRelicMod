namespace StarRailRelic.Content.Items.Relic.Out.One.Wind
{
    public abstract class WindSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Wind;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<RangedDamageClass>() += 5f / 100f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsWindFourSet = true;
        }
    }

    public class WindRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Wind;
    }

    public class WindHead : WindSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class WindHands : WindSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class WindBody : WindSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class WindFeet : WindSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
