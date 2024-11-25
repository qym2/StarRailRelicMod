namespace StarRailRelic.Content.Items.Relic.In.One.Space
{
    public abstract class SpaceSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Space;
    }

    public class SpaceSphere : SpaceSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class SpaceRope : SpaceSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
