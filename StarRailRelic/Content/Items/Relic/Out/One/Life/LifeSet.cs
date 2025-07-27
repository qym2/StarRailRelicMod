namespace StarRailRelic.Content.Items.Relic.Out.One.Life
{
    public abstract class LifeSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Life;

        public override void ModifyTwoSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsLifeTwoSet = true;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsLifeFourSet = true;
        }
    }

    public class LifeRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Life;
    }

    public class LifeHead : LifeSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class LifeHands : LifeSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class LifeBody : LifeSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class LifeFeet : LifeSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
