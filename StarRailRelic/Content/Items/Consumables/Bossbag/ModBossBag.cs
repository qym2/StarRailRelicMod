namespace StarRailRelic.Content.Items.Consumables.Bossbag
{
    public abstract class ModBossBag : ModItem
    {
        public abstract bool PreHardmode { get; }
        public virtual int Rare => ItemRarityID.Purple;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = PreHardmode;

            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.width = 32;
            Item.height = 32;
            Item.rare = Rare;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public static void SetBossBagItemLoot<T>(ItemLoot itemLoot) where T : ModBoss
        {
            BossLootData bossLootData = GetInstance<T>().BossLootData;

            foreach (IItemDropRule itemDropRule in bossLootData.ItemDropRules)
            {
                if(itemDropRule != null)
                {
                    itemLoot.Add(itemDropRule);
                }
            }
            if (bossLootData.ExpertItemIds.Length > 0)
            {
                foreach (int itemId in bossLootData.ExpertItemIds)
                {
                    if (itemId > 0)
                    {
                        itemLoot.Add(ItemDropRule.NotScalingWithLuck(itemId));
                    }
                }
            }
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<T>()));
        }
    }
}
