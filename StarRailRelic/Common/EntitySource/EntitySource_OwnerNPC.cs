namespace StarRailRelic.Common.EntitySource
{
    public class EntitySource_OwnerNPC : IEntitySource
    {
        public NPC OwnerNPC { get; }

#nullable enable
        public string? Context { get; }

        public EntitySource_OwnerNPC(NPC ownerNPC, string? context = null)
        {
            OwnerNPC = ownerNPC;
            Context = context;
        }
    }
}
