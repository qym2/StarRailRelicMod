
namespace StarRailRelic.Content.Items.Relic.Out.One.Ice
{
    public abstract class IceSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Ice;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<SummonDamageClass>() += 5f / 100f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsIceFourSet = true;
        }
    }

    public class IceHead : IceSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class IceHands : IceSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class IceBody : IceSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class IceFeet : IceSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
