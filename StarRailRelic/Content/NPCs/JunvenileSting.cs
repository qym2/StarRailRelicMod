using StarRailRelic.Content.Projectiles;
using StarRailRelic.Content.Projectiles.Hostile;
using Terraria.ModLoader.Utilities;

namespace StarRailRelic.Content.NPCs
{
    public class JunvenileSting : ModNPC
    {
        public Player TargetPlayer => Main.player[NPC.target];

        public float TargetDistanceX => TargetPlayer.Center.X - NPC.Center.X;
        public float TargetDistanceY => TargetPlayer.Center.Y - NPC.Center.Y;
        public float TargetDistance => Pythagorean(TargetDistanceX, TargetDistanceY);
        public Vector2 TargetDistanceVector2 => TargetPlayer.Center - NPC.Center;

        private enum State
        {
            Wander,
            Dash,
            Stop
        }

        private State CurrentState
        {
            get => (State)NPC.ai[1];
            set
            {
                NPC.ai[1] = (float)value;
                if (value == State.Stop)
                {
                    if (NPC.netSpam > 10)
                    {
                        NPC.netSpam = 10;
                    }
                }
                NPC.netUpdate = true;
            }
        }

        private float StopTimer
        {
            get => NPC.ai[2];
            set
            {
                NPC.ai[2] = value;
            }
        }

        private float ChangeToDashTimer
        {
            get => NPC.ai[2];
            set
            {
                NPC.ai[2] = value;
            }
        }

        public sealed override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 12;

            NPCSets.NPCBestiaryDrawOffset.Add(Type, new() { Frame = null, Velocity = 1f, Position = Vector2.Zero });
        }

        public override void SetDefaults()
        {
            NPC.width = 90;
            NPC.height = 70;

            NPC.damage = 20;
            NPC.lifeMax = 150;
            NPC.defense = 6;
            NPC.noGravity = true;

            NPC.knockBackResist = 0.4f;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("StarRailRelic/Assets/Music/StingDeath") with
            {
                Volume = 0.7f,
                MaxInstances = 0
            };

            NPC.value = 0;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            List<IBestiaryInfoElement> bestiary = [new FlavorTextBestiaryInfoElement(this.GetLocalizedValue("BestiaryInfo"))];
            bestiary.AddRange([BestiaryBiomes.TheCorruption]);

            bestiaryEntry.Info.AddRange(bestiary);
        }

        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 11;

            NPC.frameCounter += 0.5f;

            if (NPC.frameCounter > 2.5f)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedMoss, hit.HitDirection, -1f);
                }
            }
            else
            {
                for (int i = 0; i < 40; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedMoss, 2 * hit.HitDirection, -2f);
                }

            }

            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                int headGoreType = Mod.Find<ModGore>("JunvenileSting_Head").Type;
                int tailGoreType = Mod.Find<ModGore>("JunvenileSting_Tail").Type;
                int wingGoreType = Mod.Find<ModGore>("JunvenileSting_Wing").Type;

                IEntitySource entitySource = NPC.GetSource_Death();

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-12, 13), Main.rand.Next(-12, 13)), headGoreType);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-12, 13), Main.rand.Next(-12, 13)), tailGoreType);
                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-12, 13), Main.rand.Next(-12, 13)), wingGoreType);
                }
            }
        }

        public override void OnKill()
        {
            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ProjectileType<EntomonEulogy>(), NPC.lifeMax / 12, 5, Main.myPlayer);
        }

        public override void AI()
        {
            if (NPC.velocity.X > 0)
            {
                NPC.spriteDirection = NPC.direction = -1;
            }
            if (NPC.velocity.X < 0)
            {
                NPC.spriteDirection = NPC.direction = 1;
            }

            NPC.BounceWhenTileCollide();

            NPC.TargetClosest();
            if (TargetPlayer.dead)
            {
                NPCLeave(0.04f, 10);
                return;
            }

            if (CurrentState == State.Wander)
            {
                NPC.SetVelocity(1.5f, 4f);

                ChangeToDashTimer++;

                if (ChangeToDashTimer >= 240f)
                {
                    CurrentState = State.Dash;
                    ChangeToDashTimer = 0f;
                    NPC.target = 255;
                }
            }
            else if (CurrentState == State.Dash)
            {
                NPC.velocity = 5f / TargetDistance * TargetDistanceVector2;
                CurrentState = State.Stop;
            }
            else if (CurrentState == State.Stop)
            {
                StopTimer++;
                if (StopTimer >= 40f)
                {
                    NPC.velocity *= 0.98f;

                    NPC.LimitVelocity();
                }

                if (StopTimer >= 120)
                {
                    StopTimer = 0f;
                    NPC.target = 255;
                    CurrentState = State.Wander;
                }
            }

            NPC.EscapeFromWater();
        }

        public void NPCLeave(float leaveVelocity, int leaveTime)
        {
            NPC.velocity.Y -= leaveVelocity;
            NPC.EncourageDespawn(leaveTime);
        }

        public override bool? CanFallThroughPlatforms()
        {
            return true;
        }
    }

    public class WildJunvenileSting : JunvenileSting
    {
        public override string Texture => (GetType().Namespace + "." + "JunvenileSting").Replace('.', '/');

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (WorldGen.crimson)
            {
                return SpawnCondition.Crimson.Chance * 0.2f;
            }
            else
            {
                return SpawnCondition.Corruption.Chance * 0.2f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<RottenStingcarapace>(), 1, 1, 3));
        }
    }
}
