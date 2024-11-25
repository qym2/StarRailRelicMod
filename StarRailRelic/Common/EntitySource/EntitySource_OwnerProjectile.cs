namespace StarRailRelic.Common.EntitySource
{
    public class EntitySource_OwnerProjectile : IEntitySource
    {
        public Projectile OwnerProjectile { get; }

#nullable enable
        public string? Context { get; }

        public EntitySource_OwnerProjectile(Projectile ownerProjectile, string? context = null)
        {
            OwnerProjectile = ownerProjectile;
            Context = context;
        }
    }
}
