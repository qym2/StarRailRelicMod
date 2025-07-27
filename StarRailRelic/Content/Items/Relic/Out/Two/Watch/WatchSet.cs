
namespace StarRailRelic.Content.Items.Relic.Out.Two.Watch
{
    public abstract class WatchSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Watch;

        public override void ModifyTwoSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsWatchTwoSet = true;
        }

        public override void UpdateRelicSetFour(Player player)
        {
            player.accWatch = 3;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsWatchFourSet = true;
        }
    }

    public class WatchRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Watch;
    }

    public class WatchHead : WatchSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class WatchHands : WatchSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class WatchBody : WatchSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class WatchFeet : WatchSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
