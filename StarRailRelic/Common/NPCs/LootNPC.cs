namespace StarRailRelic.Common.NPCs
{
    public class LootNPC : GlobalNPC
    {
        public static int LootModifier => MainConfigs.Instance.SimplifiedMode ? 2 : 1;

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (!npc.friendly &&
                !npc.dontTakeDamage &&
                !npc.SpawnedFromStatue &&
                 npc.type != NPCID.TargetDummy &&
                !npc.IsBoss() &&
                npc.value > 0)
            {
                npcLoot.Add(ItemDropRule.ByCondition(
                    Condition.InClassicMode.ToDropCondition(ShowItemDropInUI.WhenConditionSatisfied),
                    ItemType<LostCrystal>(), 1,
                    (int)((npc.value + npc.extraValue) / 10 * 0.7f * LootModifier),
                    Math.Max(1 * LootModifier, (int)((npc.value + npc.extraValue) / 10 * 1.3f * LootModifier))));

                npcLoot.Add(ItemDropRule.ByCondition(
                    Condition.InExpertMode.ToDropCondition(ShowItemDropInUI.WhenConditionSatisfied),
                    ItemType<LostCrystal>(), 1,
                    (int)((npc.value + npc.extraValue) / 10 * 0.7f * 2.5f * LootModifier),
                    Math.Max(1 * LootModifier, (int)((npc.value + npc.extraValue) / 10 * 1.3f * 2.5f * LootModifier))));
            }
        }
    }
}
