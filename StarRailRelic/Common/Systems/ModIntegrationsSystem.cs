namespace StarRailRelic.Common.Systems
{
    public class ModIntegrationsSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            DoBossChecklistIntegration();
        }

        private void DoBossChecklistIntegration()
        {
            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                nameof(SwarmTrueSting),
                4.5f,
                () => DownedBossSystem.downedSwarmTrueSting,
                NPCType<SwarmTrueSting>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ItemType<HoneycorruptGrub>(),
                    ["collectibles"] = new List<int>(),
                    ["despawnMessage"] = GetOrRegister($"Mods.StarRailRelic.NPCs.{nameof(SwarmTrueSting)}.DespawnMessage")
                }
            );

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                nameof(IceOutOfSpace) + nameof(BlazeOutOfSpace),
                1.5f,
                () => DownedBossSystem.downedIceOutOfSpace && DownedBossSystem.downedBlazeOutOfSpace,
                new List<int>() { NPCType<IceOutOfSpace>(), NPCType<BlazeOutOfSpace>() },
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ItemType<KeyOfFragmentum>(),
                    ["collectibles"] = new List<int>(),
                    ["despawnMessage"] = GetOrRegister($"Mods.StarRailRelic.NPCs.{nameof(IceOutOfSpace)}.DespawnMessage")
                }
            );
        }
    }
}
