namespace StarRailRelic.Content.Items.Relic.Out.One.Thief
{
    public abstract class ThiefSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Thief;

        public override void ModifyTwoSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsThiefTwoSet = true;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsThiefFourSet = true;
        }
    }

    public class ThiefHead : ThiefSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class ThiefHands : ThiefSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class ThiefBody : ThiefSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class ThiefFeet : ThiefSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
