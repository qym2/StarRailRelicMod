namespace StarRailRelic.Content.Items.Relic.In.Two.Izumo
{
    public abstract class IzumoSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Izumo;
    }

    public class IzumoSphere : IzumoSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class IzumoRope : IzumoSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
