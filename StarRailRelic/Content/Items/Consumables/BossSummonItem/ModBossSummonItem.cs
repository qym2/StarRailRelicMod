namespace StarRailRelic.Content.Items.Consumables.BossSummonItem
{
    public abstract class ModBossSummonItem : ModItem
    {
        public virtual List<int> BossTypes => [NPCID.EyeofCthulhu];

        public virtual SoundStyle BossSummonSound => SoundID.Roar;

        public virtual bool SummonCondition => true;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 3;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }

        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.maxStack = 20;

            Item.useAnimation = 30;
            Item.useTime = 30;

            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player)
        {
            bool anyNPCs = false;
            foreach (int bossType in BossTypes)
            {
                if (NPC.AnyNPCs(bossType))
                {
                    anyNPCs = true;
                    break;
                }
            }

            return !anyNPCs && SummonCondition;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(BossSummonSound, player.position);
                
                foreach (int bossType in BossTypes)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, bossType);
                    }
                    else
                    {
                        NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: bossType);
                    }
                }
            }

            return true;
        }
    }
}
