namespace StarRailRelic.Content.NPCs.Boss
{
    [AutoloadBossHead]
    public class SwarmTrueSting : ModBoss
    {
        public override BossLootData BossLootData => new()
        {
            BossBagId = ItemType<SwarmTrueStingBossBag>(),
            TrophyId = -1,
            RelicId = -1,
            PetId = -1,
            ExpertItemIds = [0],

            ItemDropRules =
            [
                //ItemDropRule.OneFromOptions(1, ItemType<Crusher>(), ItemType<RockFist>()),
                ItemDropRule.Common(ItemType<RelicRemians>(), 1, 50, 150),
                ItemDropRule.Common(ItemType<SwarmgnawedCarapace>(), 3, 1, 2),
            ]
        };

        public override IBestiaryInfoElement[] BestiaryElse => [BestiaryBiomes.TheCorruption];

        public override int FrameCount => 12;
        public override bool DrawTailing => true;
        public override bool DrawTailingCondition => (Main.expertMode || CurrentState == State.EXDash) && NPC.life <= NPC.lifeMax / 2;

        private enum State
        {
            Wander,
            Dash,
            Stop,
            Shoot,
            EXDash
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

        private float ChangeToDashTimer
        {
            get => NPC.ai[2];
            set
            {
                NPC.ai[2] = value;
            }
        }

        private float SummonTimer
        {
            get => NPC.ai[3];
            set
            {
                NPC.ai[3] = value;
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

        private float DashCount
        {
            get => NPC.ai[1];
            set
            {
                NPC.ai[1] = value;
            }
        }

        private float EXDashCount
        {
            get => NPC.ai[3];
            set
            {
                NPC.ai[3] = value;
            }
        }

        private Vector2 TargetDistanceVector2First;
        private float TargetDistanceFirst;
        private bool isDivided;

        public override void SendExtraAINPC(BinaryWriter writer)
        {
            writer.Write(TargetDistanceVector2First.X);
            writer.Write(TargetDistanceVector2First.Y);
            writer.Write(TargetDistanceFirst);
            writer.Write(isDivided);
        }

        public override void ReceiveExtraAINPC(BinaryReader reader)
        {
            TargetDistanceVector2First = new(reader.ReadSingle(), reader.ReadSingle());
            TargetDistanceFirst = reader.ReadSingle();
            isDivided = reader.ReadBoolean();
        }

        private static int ServantCount
        {
            get
            {
                int servantCount = 0;
                foreach (NPC npc in Main.ActiveNPCs)
                {
                    if (npc.type == NPCType<JunvenileSting>() ||
                        npc.type == NPCType<LesserSting>())
                    {
                        servantCount++;
                    }
                }
                return servantCount;
            }
        }
                

        public override void SetDefaultsBoss()
        {
            NPC.width = 160;
            NPC.height = 115;

            NPC.damage = 40;
            NPC.lifeMax = 4200;
            NPC.defense = 15;
            NPC.scale = 1.3f;

            NPC.knockBackResist = 0f;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath66;

            NPC.value = Item.buyPrice(0, 7, 0, 0);

            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.npcSlots = 20;
            NPC.SpawnWithHigherTime(30);

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/异形容器 Aberrant Receptacle");
            }
        }

        public override void FindFrame(int frameHeight)
        {
            PlayFrame(frameHeight);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int i = 0; i < hit.Damage / (float)NPC.lifeMax * 100f; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedMoss, hit.HitDirection, -1f);
                }
            }
            else
            {
                for (int i = 0; i < 60; i++)
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
                int headGoreType = Mod.Find<ModGore>("SwarmTrueSting_Head").Type;
                int tailGoreType = Mod.Find<ModGore>("SwarmTrueSting_Tail").Type;
                int wingGoreType0 = Mod.Find<ModGore>("SwarmTrueSting_Wing_0").Type;
                int wingGoreType1 = Mod.Find<ModGore>("SwarmTrueSting_Wing_1").Type;

                IEntitySource entitySource = NPC.GetSource_Death();

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), headGoreType);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), tailGoreType);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), wingGoreType0);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), wingGoreType1);
                
                PunchCameraModifier modifier = new(NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, FullName);
                Main.instance.CameraModifiers.Add(modifier);
            }
        }

        public override void AI()
        {
            NPC.damage = NPC.GetAttackDamage_LerpBetweenFinalValues(40, 40);
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

            if (CurrentState == State.Wander)
            {
                Divide();

                float speed = (Main.expertMode && NPC.life <= NPC.lifeMax / 2) ? 20 : 8;
                float acceleration = (Main.expertMode && NPC.life <= NPC.lifeMax / 2) ? 0.1f : 0.08f;

                float xOffset;
                float yOffset;
                if (DashCount % 2 == 0)
                {
                    xOffset = 0;
                    yOffset = -200f;
                }
                else
                {
                    xOffset = (DashCount % 4 == 1) ? 400f : -400f;
                    yOffset = 0;
                    NPC.spriteDirection = NPC.direction = TargetDistanceX > 0 ? -1 : 1;
                }
                GetTargetVelocity(speed, out Vector2 targetVelocity, out _, xOffset, yOffset);
                NPC.SetVelocity(acceleration, targetVelocity);

                ChangeToDashTimer++;
                float summonTime = 300;

                if (ChangeToDashTimer >= summonTime)
                {
                    if (NPC.life <= NPC.lifeMax / 2 && (DashCount % 9 is 1 or 6))
                    {
                        CurrentState = State.EXDash;
                    }
                    else if (DashCount % 3 == 2)
                    {
                        CurrentState = State.Shoot;
                    }
                    else
                    {
                        CurrentState = State.Dash;
                    }

                    ChangeToDashTimer = 0f;
                    SummonTimer = 0f;
                    NPC.target = 255;
                }
                else
                {
                    if (!TargetPlayer.dead)
                    {
                        SummonTimer++;
                    }

                    float summonCD = DashCount % 2 == 0 ? 120 : 240;

                    if (SummonTimer >= summonCD)
                    {
                        SummonTimer = 0f;
                        Summon(5 / TargetDistance * TargetDistanceVector2);
                    }
                }
            }
            else if (CurrentState == State.Dash)
            {
                float dashSpeed = (Main.expertMode && NPC.life <= NPC.lifeMax / 2) ? 20 : 16;

                NPC.velocity = dashSpeed / TargetDistance * TargetDistanceVector2;
                CurrentState = State.Stop;
            }
            else if (CurrentState == State.Stop)
            {
                StopTimer++;
                if (StopTimer >= ((Main.expertMode && NPC.life <= NPC.lifeMax / 2) ? 2f : 5f))
                {
                    NPC.velocity *= 0.995f;

                    NPC.LimitVelocity();
                }

                int stopTime = (Main.expertMode && NPC.life <= NPC.lifeMax / 2) ? 15 : 30;

                if (StopTimer >= stopTime)
                {
                    StopTimer = 0f;
                    AddDashCount();
                    NPC.target = 255;
                    CurrentState = State.Wander;
                }
            }
            else if (CurrentState == State.Shoot)
            {
                NPC.spriteDirection = NPC.direction = TargetDistanceX > 0 ? -1 : 1;
                StopTimer++;
                NPC.velocity *= 0;

                if (StopTimer <= 40)
                {
                    if (StopTimer % 5 == 0)
                    {
                        Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPCCenter, TargetDistanceVector2.SafeNormalize(Vector2.Zero) * 10, ProjectileType<CorrosiveBarrage>(), NPC.damage / 4, 3, Main.myPlayer);
                    }
                }
                else
                {
                    AddDashCount();
                    StopTimer = 0f;
                    NPC.target = 255;
                    CurrentState = State.Wander;
                }
            }
            else if (CurrentState == State.EXDash)
            {
                NPC.damage = NPC.GetAttackDamage_LerpBetweenFinalValues(40, 40 + ServantCount);
                NPC.damage = NPC.GetAttackDamage_ScaledByStrength(NPC.damage);

                if (EXDashCount == 0 && StopTimer == 0)
                {
                    TargetDistanceFirst = TargetDistance;
                    TargetDistanceVector2First = TargetDistanceVector2;
                }
                float distance = TargetDistanceFirst;
                Vector2 vector = TargetDistanceVector2First;
                float dashSpeed = Main.expertMode ? 55 : 35;

                for (int i = 0; i < EXDashCount; i++)
                {
                    vector = RotateVector(vector, Main.expertMode ? 144 : 120);
                }

                if (StopTimer == 0)
                {
                    NPC.velocity = dashSpeed / distance * vector;
                }

                StopTimer++;
                NPC.velocity *= 0.99f;
                NPC.LimitVelocity();

                if (StopTimer >= 25)
                {
                    NPC.velocity *= 0;
                    StopTimer = 0f;
                    EXDashCount++;
                }

                if (EXDashCount > (Main.expertMode ? 4 : 2))
                {
                    EXDashCount = 0;
                    AddDashCount();
                    NPC.target = 255;
                    CurrentState = State.Wander;
                }
                else
                {
                    CurrentState = State.EXDash;
                }
            }
        }

        private void Divide()
        {
            if (!isDivided && NPC.life <= NPC.lifeMax / 2)
            {
                Summon(5 / TargetDistance * TargetDistanceVector2, true);
                isDivided = true;
            }
        }

        private void AddDashCount()
        {
            DashCount++;
            if (DashCount > 35)
            {
                DashCount = 0;
            }
        }

        private void Summon(Vector2 servantVelocity, bool isDividing = false)
        {
            if (!isDividing)
            {
                if (ServantCount >= 20)
                {
                    return;
                }
            }

            Vector2 servantPosition = NPC.Center + servantVelocity * 10f;
            SpawnServant(servantVelocity, servantPosition, isDividing);

            SoundEngine.PlaySound(SoundID.NPCHit1, servantPosition);
            for (int m = 0; m < 10; m++)
            {
                Dust.NewDust(servantPosition, 20, 20, DustID.Blood, servantVelocity.X * 0.4f, servantVelocity.Y * 0.4f);
            }
        }

        private void SpawnServant(Vector2 servantVelocity, Vector2 servantPosition, bool isDividing = false)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int servantIndex = NPC.NewNPC(NPC.GetSource_FromAI(), (int)servantPosition.X, (int)servantPosition.Y, isDividing ? NPCType<SwarmTrueStingDivided>() : ((DashCount - 2) % 3 == 0 ? NPCType<LesserSting>() : NPCType<JunvenileSting>()));
                NPC npc = Main.npc[servantIndex];
                npc.velocity.X = servantVelocity.X;
                npc.velocity.Y = servantVelocity.Y;

                if (isDividing)
                {
                    npc.life = npc.lifeMax / 2;
                    SwarmTrueSting swarmTrueSting = npc.ModNPC as SwarmTrueSting;
                    swarmTrueSting.isDivided = true;
                }

                if (Main.netMode == NetmodeID.Server && servantIndex < 200)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: servantIndex);
                }
            }

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
            }
        }

        private void GetTargetVelocity(float speed, out Vector2 targetVelocity, out float targetDistance, float TargetDistanceXOffset = 0, float TargetDistanceYOffset = 0)
        {
            float targetDistanceX = TargetDistanceX + TargetDistanceXOffset;
            float targetDistanceY = TargetDistanceY + TargetDistanceYOffset;

            targetDistance = Pythagorean(targetDistanceX, targetDistanceY);

            float speedFactor = speed / targetDistance;

            targetVelocity = new(targetDistanceX * speedFactor, targetDistanceY * speedFactor);
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedSwarmTrueSting, -1);
        }
    }

    [AutoloadBossHead]
    public class SwarmTrueStingDivided : SwarmTrueSting
    {
        public override string BossHeadTexture => (GetType().Namespace + "." + "SwarmTrueSting").Replace('.', '/') + "_Head_Boss";
        public override string Texture => (GetType().Namespace + "." + "SwarmTrueSting").Replace('.', '/');

        public override void SetDefaultsBoss()
        {
            base.SetDefaultsBoss();
            NPC.value = 0;
        }

        public override bool PreKill()
        {
            NPC.boss = false;
            return base.PreKill();
        }

        public override void OnKill() { }

        public override void ModifyNPCLoot(NPCLoot npcLoot) { }
    }
}
