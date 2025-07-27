namespace StarRailRelic.Content.Items.Relic.Out.One.Fire
{
    public abstract class FireSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Fire;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<MagicDamageClass>() += 5f / 100f;
        }

        public override void UpdateRelicSetFour(Player player)
        {
            player.manaCost -= 12f / 100f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsFireFourSet = true;
        }
    }

    public class FireRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Fire;
    }

    public class FireHead : FireSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class FireHands : FireSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class FireBody : FireSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class FireFeet : FireSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
