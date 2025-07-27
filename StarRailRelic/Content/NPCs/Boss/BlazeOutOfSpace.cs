using StarRailRelic.Content.Projectiles.Hostile;
using Terraria;

namespace StarRailRelic.Content.NPCs.Boss
{
    [AutoloadBossHead]
    public class BlazeOutOfSpace : ModBoss
    {
        public override BossLootData BossLootData => new()
        {
            BossBagId = 0,
            TrophyId = -1,
            RelicId = -1,
            PetId = -1,
            ExpertItemIds = [0],

            SpecialItemDropRules =
            [
                ItemDropRule.Common(ItemType<RelicRemians>(), 1, 10, 30)
            ]
        };

        public override IBestiaryInfoElement[] BestiaryElse => [BestiaryBiomes.TheUnderworld];

        public override int FrameCount => 25;
        
        private enum State
        {
            Wander,
            ShootI,
            ShootII
        }

        private State CurrentState
        {
            get => (State)NPC.ai[0];
            set
            {
                NPC.ai[0] = (float)value;
                if (NPC.netSpam > 10)
                {
                    NPC.netSpam = 10;
                }
                NPC.netUpdate = true;
            }
        }

        private float SkillCount
        {
            get => NPC.ai[1];
            set
            {
                NPC.ai[1] = value;
            }
        }

        private float ChangeToSkillTimer
        {
            get => NPC.ai[2];
            set
            {
                NPC.ai[2] = value;
            }
        }

        private static int FrameHeight => 122;
        private int Frame => NPC.frame.Y / FrameHeight;

        public override void SetDefaultsBoss()
        {
            NPC.width = 120;
            NPC.height = 120;

            NPC.damage = 12;
            NPC.lifeMax = 2000;
            NPC.defense = 8;

            NPC.knockBackResist = 0.2f;

            NPC.HitSound = SoundID.NPCHit41;
            NPC.DeathSound = SoundID.NPCDeath43;

            NPC.value = Item.buyPrice(0, 1, 0, 0);

            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.npcSlots = 10;
            NPC.SpawnWithHigherTime(30);

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/破冬而行 Braving the Cold");
            }
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedBlazeOutOfSpace, -1);
        }

        public override void FindFrame(int frameHeight)
        {
        }

        public void FindFrame()
        {
            float frameSpeed = 2.5f;
            NPC.frameCounter += 0.5f;

            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;

                switch (CurrentState)
                {
                    case State.Wander:
                        SetFrameStartAndEnd(1, 8);
                        break;
                    case State.ShootI:
                        SetFrameStartAndEnd(9, 18);
                        break;
                    case State.ShootII:
                        SetFrameStartAndEnd(19, 25);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetFrameStartAndEnd(int startFrame, int endFrame)
        {
            if (Frame < (startFrame - 1))
            {
                NPC.frame.Y = (startFrame - 1) * FrameHeight;
            }
            NPC.frame.Y += FrameHeight;
            if (Frame >= endFrame)
            {
                NPC.frame.Y = (startFrame - 1) * FrameHeight;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int i = 0; i < hit.Damage / (float)NPC.lifeMax * 100f; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Stone, hit.HitDirection, -1f);
                }
            }
            else
            {
                for (int i = 0; i < 60; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Stone, 2 * hit.HitDirection, -2f);
                }
            }

            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                int headGoreType = Mod.Find<ModGore>("BlazeOutOfSpace_Head").Type;
                int handGoreType = Mod.Find<ModGore>("BlazeOutOfSpace_Hand").Type;
                int feetGoreType = Mod.Find<ModGore>("BlazeOutOfSpace_Feet").Type;

                IEntitySource entitySource = NPC.GetSource_Death();

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), headGoreType);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), handGoreType);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), feetGoreType);
            }
        }

        public override bool PreAI()
        {
            FindFrame();
            return base.PreAI();
        }

        public override void AI()
        {
            NPC.damage = NPC.GetAttackDamage_LerpBetweenFinalValues(15, 15);
            NPC.damage = NPC.GetAttackDamage_ScaledByStrength(NPC.damage);

            if (NPC.velocity.X > 0)
            {
                NPC.spriteDirection = NPC.direction = -1;
            }
            if (NPC.velocity.X < 0)
            {
                NPC.spriteDirection = NPC.direction = 1;
            }

            NPC.TargetClosest();
            if (TargetPlayer.dead)
            {
                CurrentState = State.Wander;
                NPCLeave(0.04f, 10);
                return;
            }

            float multipler = 1 / (2f - ((float)NPC.life / NPC.lifeMax));
            if (CurrentState == State.Wander)
            {
                NPC.SetVelocity(2.5f / multipler, 6f / multipler);

                ChangeToSkillTimer++;
                float summonTime = 200 * multipler;

                if (ChangeToSkillTimer >= summonTime)
                {
                    if (SkillCount % 3 == 2)
                    {
                        CurrentState = State.ShootI;
                    }
                    else
                    {
                        CurrentState = State.ShootII;
                    }

                    ChangeToSkillTimer = 0f;
                    NPC.target = 255;
                }
            }
            else if (CurrentState == State.ShootI)
            {
                NPC.velocity *= 0;
                NPC.spriteDirection = NPC.direction = TargetDistanceX > 0 ? -1 : 1;
                if ((Frame == 15 || Frame == 16 || Frame == 17) && (int)Main.time % 2 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPCCenter);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPCCenter, TargetDistanceVector2.SafeNormalize(Vector2.Zero).GetRandomRotated(15) * 10, ProjectileType<FireBall>(), NPC.damage / 4, 3, Main.myPlayer);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projectile);
                    }
                }
                if (Frame >= 17)
                {
                    AddSkillCount();
                    NPC.target = 255;
                    CurrentState = State.Wander;
                }
            }
            else if (CurrentState == State.ShootII)
            {
                NPC.velocity *= 0;
                NPC.spriteDirection = NPC.direction = TargetDistanceX > 0 ? -1 : 1;

                Vector2 shootCenter = NPCCenter + new Vector2(NPC.spriteDirection == 1 ? -50 : 50, 0);
                Vector2 targetDistanceVector2 = TargetPlayer.Center - shootCenter;
                if (Frame == 23 && (int)Main.time % 3 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20, shootCenter);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), shootCenter, targetDistanceVector2.SafeNormalize(Vector2.Zero).GetRandomRotated(25) * 10, ProjectileType<FireBall>(), NPC.damage / 4, 3, Main.myPlayer);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projectile);
                    }
                }

                if (Frame >= 24)
                {
                    AddSkillCount();
                    NPC.target = 255;
                    CurrentState = State.Wander;
                }
            }
        }

        private void AddSkillCount()
        {
            SkillCount++;
            if (SkillCount > int.MaxValue)
            {
                SkillCount = 0;
            }
        }
    }
}