using StarRailRelic.Content.EmoteBubbles;
using StarRailRelic.Content.Items.Weapons.Melee;
using Terraria.DataStructures;
using Terraria.GameContent.UI;

namespace StarRailRelic.Content.NPCs.Town
{
    [AutoloadHead]
    public class Stelle : ModNPC
    {
        public const string ShopName = "Shop";
        private readonly int npcID = NPCID.Stylist;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[npcID];

            NPCSets.ExtraFramesCount[Type] = NPCSets.ExtraFramesCount[npcID];
            NPCSets.AttackFrameCount[Type] = NPCSets.AttackFrameCount[npcID];
            NPCSets.AttackType[Type] = NPCSets.AttackType[npcID];
            NPCSets.HatOffsetY[Type] = NPCSets.HatOffsetY[npcID];
            NPCSets.NPCFramingGroup[Type] = NPCSets.NPCFramingGroup[npcID];
            NPCSets.AttackTime[Type] = NPCSets.AttackTime[npcID];
            NPCSets.DangerDetectRange[Type] = NPCSets.DangerDetectRange[npcID];
            NPCSets.AttackAverageChance[Type] = NPCSets.AttackAverageChance[npcID];

            NPCSets.FaceEmote[Type] = EmoteBubbleType<StelleEmote>();

            NPCSets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = 1f,
                Direction = -1
            };

            NPCSets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<CrimsonBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<CorruptionBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Like)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Love)

                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Princess, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Truffle, AffectionLevel.Love)
            ;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 15;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Stylist;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
				BestiaryBiomes.Surface,
				new FlavorTextBestiaryInfoElement(GetTextValue("Mods.StarRailRelic.NPCs.Stelle.BestiaryInfo"))
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 50;

            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke);
            }
        }

        #region Éú³É
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_SpawnNPC)
            {
                TownNPCRespawnSystem.unlockedStelleSpawn = true;
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (TownNPCRespawnSystem.unlockedStelleSpawn)
            {
                return true;
            }

            foreach (Player player in Main.ActivePlayers)
            {
                RelicPlayer modPlayer = player.GetModPlayer<RelicPlayer>();

                static bool RelicLevelMax(Item item)
                {
                    return item.ModItem is ModRelic modRelic && modRelic.level == modRelic.levelMax;
                }

                if (RelicLevelMax(modPlayer.HeadRelic) ||
                    RelicLevelMax(modPlayer.HandsRelic) ||
                    RelicLevelMax(modPlayer.BodyRelic) ||
                    RelicLevelMax(modPlayer.FeetRelic) ||
                    RelicLevelMax(modPlayer.LinkRopeRelic) ||
                    RelicLevelMax(modPlayer.PlanarSphereRelic))
                {
                    if (NPC.downedBoss1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        public override List<string> SetNPCNameList()
        {
            return [
                "Stelle",
                "Caelus",
                "Galactic Batter",
                "Raccoon"
            ];
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            chat.Add(GetTextValue("Mods.StarRailRelic.NPCs.Stelle.Dialogue.1"));
            chat.Add(GetTextValue("Mods.StarRailRelic.NPCs.Stelle.Dialogue.2"));
            chat.Add(GetTextValue("Mods.StarRailRelic.NPCs.Stelle.Dialogue.3"));
            chat.Add(GetTextValue("Mods.StarRailRelic.NPCs.Stelle.Dialogue.4"));
            chat.Add(GetTextValue("Mods.StarRailRelic.NPCs.Stelle.Dialogue.Common"), 2.0);
            chat.Add(GetTextValue("Mods.StarRailRelic.NPCs.Stelle.Dialogue.Rare"), 0.3);

            return chat;
        }
        
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Shop;

            if (Main.LocalPlayer.HeldItem.ModItem is ModRelic modRelic && modRelic.level == modRelic.levelMax)
            {
                button = GetTextValue("Mods.StarRailRelic.Exchange");
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                if (Main.LocalPlayer.HeldItem.ModItem is ModRelic modRelic && modRelic.level == modRelic.levelMax)
                {
                    SoundEngine.PlaySound(SoundID.Grab);
                    Main.npcChatText = GetTextValue("Mods.StarRailRelic.NPCs.Stelle.Dialogue.Exchange");

                    Main.LocalPlayer.HeldItem.TurnToAir();
                    Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ItemType<LostCrystal>(), 7000);

                    return;
                }

                shop = ShopName;
            }
        }

        public override void AddShops()
        {
            NPCShop npcShop = new NPCShop(Type, ShopName)
                .Add<WeaponChargingComponent>(Condition.TimeDay)
                .Add<LostCrystal>()
                ;

            //if (GetInstance<ExampleModConfig>().ExampleWingsToggle)
            //{
            //    npcShop.Add<ExampleWings>(ExampleConditions.InExampleBiome);
            //}

            npcShop.Register();
        }

        public override bool CanGoToStatue(bool toKingStatue) => !toKingStatue;

        #region ¹¥»÷
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = NPC.damage;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 15;
            randExtraCooldown = 8;
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = itemHeight = 56;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            Main.GetItemDrawFrame(ItemType<BaseballBat>(), out item, out itemFrame);
            itemSize = 56;
            if (NPC.ai[1] > NPCSets.AttackTime[NPC.type] * 0.66f)
            {
                offset.Y = 12f;
            }
        }
        #endregion

        public override int? PickEmote(Player closestPlayer, List<int> emoteList, WorldUIAnchor otherAnchor)
        {
            int type = EmoteID.EmoteNote;

            if (otherAnchor.entity is NPC { type: NPCID.Angler })
            {
                type = EmoteID.EmotionAnger;
            }
            if (otherAnchor.entity is NPC { type: NPCID.ArmsDealer })
            {
                type = EmoteID.EmotionAlert;
            }

            for (int i = 0; i < 4; i++)
            {
                emoteList.Add(type);
            }

            return base.PickEmote(closestPlayer, emoteList, otherAnchor);
        }
    }
}