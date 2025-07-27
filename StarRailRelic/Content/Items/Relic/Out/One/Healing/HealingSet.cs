
namespace StarRailRelic.Content.Items.Relic.Out.One.Healing
{
    public abstract class HealingSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Healing;

        public override void ModifyTwoSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsHealingTwoSet = true;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsHealingFourSet = true;
        }
    }

    public class HealingRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Healing;
    }

    public class HealingHead : HealingSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class HealingHands : HealingSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class HealingBody : HealingSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class HealingFeet : HealingSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
