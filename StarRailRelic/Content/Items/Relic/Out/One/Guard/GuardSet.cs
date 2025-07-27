namespace StarRailRelic.Content.Items.Relic.Out.One.Guard
{
    public abstract class GuardSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Guard;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.endurance += 3 / 100f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsGuardFourSet = true;
        }
    }

    public class GuardRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Guard;
    }

    public class GuardHead : GuardSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class GuardHands : GuardSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class GuardBody : GuardSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class GuardFeet : GuardSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
