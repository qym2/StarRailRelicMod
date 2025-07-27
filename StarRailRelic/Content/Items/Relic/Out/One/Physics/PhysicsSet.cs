namespace StarRailRelic.Content.Items.Relic.Out.One.Physics
{
    public abstract class PhysicsSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Physics;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<SummonDamageClass>() += 5f / 100f;
        }

        public override void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsPhysicsFourSet = true;
        }
    }

    public class PhysicsRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Physics;
    }

    public class PhysicsHead : PhysicsSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class PhysicsHands : PhysicsSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class PhysicsBody : PhysicsSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class PhysicsFeet : PhysicsSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
