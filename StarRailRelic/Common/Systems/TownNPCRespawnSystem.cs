using StarRailRelic.Content.NPCs.Town;

namespace StarRailRelic.Common.Systems
{
    public class TownNPCRespawnSystem : ModSystem
    {
        public static bool unlockedStelleSpawn = false;

        public override void ClearWorld()
        {
            unlockedStelleSpawn = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag[nameof(unlockedStelleSpawn)] = unlockedStelleSpawn;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            unlockedStelleSpawn = tag.GetBool(nameof(unlockedStelleSpawn));

            unlockedStelleSpawn |= NPC.AnyNPCs(NPCType<Stelle>());
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.WriteFlags(unlockedStelleSpawn);
        }

        public override void NetReceive(BinaryReader reader)
        {
            reader.ReadFlags(out unlockedStelleSpawn);
        }
    }
}
