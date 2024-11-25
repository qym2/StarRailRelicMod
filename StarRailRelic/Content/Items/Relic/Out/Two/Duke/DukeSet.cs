namespace StarRailRelic.Content.Items.Relic.Out.Two.Duke
{
    public abstract class DukeSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Duke;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<MagicDamageClass>() += 5f / 100f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsDukeFourSet = true;
        }
    }

    public class DukeHead : DukeSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class DukeHands : DukeSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class DukeBody : DukeSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class DukeFeet : DukeSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
