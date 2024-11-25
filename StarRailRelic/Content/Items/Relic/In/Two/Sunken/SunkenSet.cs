namespace StarRailRelic.Content.Items.Relic.In.Two.Sunken
{
    public abstract class SunkenSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Sunken;
    }

    public class SunkenSphere : SunkenSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class SunkenRope : SunkenSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
