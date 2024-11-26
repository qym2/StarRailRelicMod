namespace StarRailRelic.Content.Items.Relic.Out.Two.Iron
{
    public abstract class IronSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Iron;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.endurance += 3 / 100f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsIronFourSet = true;
        }
    }

    public class IronHead : IronSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class IronHands : IronSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class IronBody : IronSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class IronFeet : IronSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
