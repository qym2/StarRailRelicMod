namespace StarRailRelic.Content.Items.Relic.Out.One.Quantum
{
    public abstract class QuantumSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Quantum;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<MeleeDamageClass>() += 5f / 100f;
        }

        public override void UpdateRelicSetFour(Player player)
        {
            player.GetArmorPenetration<GenericDamageClass>() += 4;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsQuantumFourSet = true;
        }
    }

    public class QuantumRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Quantum;
    }

    public class QuantumHead : QuantumSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class QuantumHands : QuantumSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class QuantumBody : QuantumSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class QuantumFeet : QuantumSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
