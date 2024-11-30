namespace StarRailRelic.Content.Items.Relic.In.Two.Sigonia
{
    public abstract class SigoniaSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Sigonia;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetCritChance<GenericDamageClass>() += 2;
        }

        public override void ModifyTwoSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer)
        {
            modPlayer.IsSigoniaTwoSet = true;
        }
    }

    public class SigoniaSphere : SigoniaSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class SigoniaRope : SigoniaSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
