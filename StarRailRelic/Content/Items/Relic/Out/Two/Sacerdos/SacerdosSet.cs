namespace StarRailRelic.Content.Items.Relic.Out.Two.Sacerdos
{
    public abstract class SacerdosSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Sacerdos;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.moveSpeed += 0.08f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsSacerdosFourSet = true;
        }
    }

    public class SacerdosRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Sacerdos;
    }

    public class SacerdosHead : SacerdosSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class SacerdosHands : SacerdosSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class SacerdosBody : SacerdosSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class SacerdosFeet : SacerdosSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
