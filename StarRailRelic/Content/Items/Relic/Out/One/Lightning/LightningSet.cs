namespace StarRailRelic.Content.Items.Relic.Out.One.Lightning
{
    public abstract class LightningSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Lightning;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<MagicDamageClass>() += 5f / 100f;
        }

        public override void UpdateRelicSetFour(Player player)
        {
            player.manaCost += 100f / 100f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsLightningFourSet = true;
        }
    }

    public class LightningRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Lightning;
    }

    public class LightningHead : LightningSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class LightningHands : LightningSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class LightningBody : LightningSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class LightningFeet : LightningSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
