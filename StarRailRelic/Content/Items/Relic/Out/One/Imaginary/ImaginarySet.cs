namespace StarRailRelic.Content.Items.Relic.Out.One.Imaginary
{
    public abstract class ImaginarySet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Imaginary;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<RangedDamageClass>() += 5f / 100f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsImaginaryFourSet = true;
        }
    }

    public class ImaginaryHead : ImaginarySet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class ImaginaryHands : ImaginarySet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class ImaginaryBody : ImaginarySet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class ImaginaryFeet : ImaginarySet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
