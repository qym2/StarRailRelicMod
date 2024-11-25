namespace StarRailRelic.Content.Items.Relic.Out.One.Messenger
{
    public abstract class MessengerSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Messenger;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.moveSpeed += 0.08f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsMessengerFourSet = true;
        }
    }

    public class MessengerHead : MessengerSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class MessengerHands : MessengerSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class MessengerBody : MessengerSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class MessengerFeet : MessengerSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
