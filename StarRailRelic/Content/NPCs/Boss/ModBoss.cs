namespace StarRailRelic.Content.NPCs.Boss
{
    public abstract class ModBoss : ModNormalNPC
    {
        public abstract BossLootData BossLootData { get; }
        public override int FrameCount { get; }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (BossLootData.BossBagId > 0)
            {
                npcLoot.Add(ItemDropRule.BossBag(BossLootData.BossBagId));
            }

            if (BossLootData.TrophyId > 0)
            {
                npcLoot.Add(ItemDropRule.Common(BossLootData.TrophyId, 10));
            }

            if (BossLootData.RelicId > 0)
            {
                npcLoot.Add(ItemDropRule.MasterModeCommonDrop(BossLootData.RelicId));
            }

            if (BossLootData.PetId > 0)
            {
                npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(BossLootData.PetId, 4));
            }

            if (BossLootData.SpecialItemDropRules != null)
            {
                foreach (IItemDropRule itemDropRule in BossLootData.SpecialItemDropRules)
                {
                    npcLoot.Add(itemDropRule);
                }
            }

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            SetBossItemLoot(notExpertRule);
            npcLoot.Add(notExpertRule);
        }

        public void SetBossItemLoot(LeadingConditionRule notExpertRule)
        {
            if (BossLootData.ItemDropRules != null)
            {
                foreach (IItemDropRule itemDropRule in BossLootData.ItemDropRules)
                {
                    if (itemDropRule != null)
                    {
                        notExpertRule.OnSuccess(itemDropRule);
                    }
                }
            }
        }

        public sealed override void SetStaticDefaultsNPC()
        {
            SetStaticDefaultsBoss();

            NPCSets.MPAllowedEnemies[Type] = true;

            NPCSets.BossBestiaryPriority.Add(Type);
        }

        public virtual void SetStaticDefaultsBoss() { }

        public sealed override void SetDefaultsNPC()
        {
            SetDefaultsBoss();
            NPC.boss = true;
        }

        public virtual void SetDefaultsBoss() { }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            //防骗伤
            cooldownSlot = ImmunityCooldownID.Bosses;
            return true;
        }
    }

    public struct BossLootData
    {
        public int BossBagId;
        public int TrophyId;
        public int RelicId;
        public int PetId;

        public int[] ExpertItemIds;

        public IItemDropRule[] ItemDropRules;
        public IItemDropRule[] SpecialItemDropRules;
    }
}
