
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StarRailRelic.Content.NPCs
{
    public abstract class ModNormalNPC : ModNPC
    {
        public abstract int FrameCount { get; }

        public virtual IBestiaryInfoElement[] BestiaryElse => [BestiaryBiomes.Surface];
        public virtual float BestiaryVelocity => 1f;
        public virtual Vector2 BestiaryPosition => Vector2.Zero;
        public virtual int? BestiaryFrame => null;

        public Player TargetPlayer => Main.player[NPC.target];

        public float TargetDistanceX => TargetPlayer.Center.X - NPCCenter.X;
        public float TargetDistanceY => TargetPlayer.Center.Y - NPCCenter.Y;
        public float TargetDistance => Pythagorean(TargetDistanceX, TargetDistanceY);
        public Vector2 TargetDistanceVector2 => TargetPlayer.Center - NPCCenter;

        public virtual Vector2 NPCCenter => NPC.Center;

        public Color LightColor => Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16)) * ((255f - NPC.alpha) / 255f);

        public Asset<Texture2D> TextureAsset => GetTexture(Texture);
        public Texture2D TextureValue => TextureAsset.Value;
        public Texture2D TextureGlowValue => DrawGlow ? GetTextureValue(Texture + "_Glow") : NullTexture.Value;

        public virtual bool DrawGlow => false;
        public virtual bool DrawTailingCondition => false;
        public virtual bool DrawTailing => false;
        public virtual int DrawTailingInterval => 2;

        public Vector2 HalfSize => new(TextureAsset.Width() / 2, TextureAsset.Height() / FrameCount / 2);
        public SpriteEffects SpriteEffects => NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        private Vector2[] _oldPosition;
        protected Vector2[] OldPosition
        {
            get
            {
                if(_oldPosition == null)
                {
                    return _oldPosition = new Vector2[PositionNum];
                }
                else
                {
                    return _oldPosition;
                }
            }
            set
            {
                _oldPosition = value;
            }
        }
        protected virtual int PositionNum => 16;

        public sealed override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(OldPosition.Length);

            foreach (Vector2 position in OldPosition)
            {
                writer.Write(position.X);
                writer.Write(position.Y);
            }

            SendExtraAINPC(writer);
        }

        public virtual void SendExtraAINPC(BinaryWriter writer) { }

        public sealed override void ReceiveExtraAI(BinaryReader reader)
        {
            int length = reader.ReadInt32();

            if (OldPosition.Length != length)
            {
                OldPosition = new Vector2[length];
            }

            for (int i = 0; i < length; i++)
            {
                float x = reader.ReadSingle();
                float y = reader.ReadSingle();
                OldPosition[i] = new Vector2(x, y);
            }

            ReceiveExtraAINPC(reader);
        }

        public virtual void ReceiveExtraAINPC(BinaryReader reader) { }

        public sealed override void SetDefaults()
        {
            SetDefaultsNPC();

            PostSetDefaults();
        }

        public virtual void SetDefaultsNPC() { }

        public virtual void PostSetDefaults() { }

        public sealed override void SetStaticDefaults()
        {
            SetStaticDefaultsNPC();

            Main.npcFrameCount[Type] = FrameCount;

            NPCSets.NPCBestiaryDrawOffset.Add(Type, new() { Frame = BestiaryFrame, Velocity = BestiaryVelocity, Position = BestiaryPosition });
        }

        public virtual void SetStaticDefaultsNPC() { }

        public sealed override void OnSpawn(IEntitySource source)
        {
            OnSpawnNPC(source);
        }

        public virtual void OnSpawnNPC(IEntitySource source) { }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            List<IBestiaryInfoElement> bestiary = [new FlavorTextBestiaryInfoElement(this.GetLocalizedValue("BestiaryInfo"))];
            bestiary.AddRange(BestiaryElse);

            bestiaryEntry.Info.AddRange(bestiary);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (DrawTailing)
            {
                return false;
            }
            else
            {
                return base.PreDraw(spriteBatch, screenPos, drawColor);
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (DrawTailing)
            {
                if ((int)(Main.time % DrawTailingInterval) == 0)
                {
                    for (int i = OldPosition.Length - 1; i > 0; i--)
                    {
                        OldPosition[i] = OldPosition[i - 1];
                    }
                    OldPosition[0] = NPC.position;
                }

                if (DrawTailingCondition)
                {
                    for (int i = OldPosition.Length - 1; i > 0; i--)
                    {
                        if (OldPosition[i] != Vector2.Zero)
                        {
                            Main.spriteBatch.Draw(TextureValue, NPC.GetNPCDrawPosition(OldPosition[i], screenPos), NPC.frame, LightColor * (0.5f - 0.05f * i), NPC.rotation, HalfSize, NPC.scale * (1 - 0.02f * i), SpriteEffects, 0);
                            if (DrawGlow)
                            {
                                Main.spriteBatch.Draw(TextureGlowValue, NPC.GetNPCDrawPosition(OldPosition[i], screenPos), NPC.frame, Color.White * (0.5f - 0.05f * i), NPC.rotation, HalfSize, NPC.scale * (1 - 0.02f * i), SpriteEffects, 0);
                            }
                        }
                    }
                }
                Main.spriteBatch.Draw(TextureValue, NPC.GetNPCDrawPosition(NPC.position, screenPos), NPC.frame, LightColor, NPC.rotation, HalfSize, NPC.scale, SpriteEffects, 0);
                if (DrawGlow)
                {
                    Main.spriteBatch.Draw(TextureGlowValue, NPC.GetNPCDrawPosition(NPC.position, screenPos), NPC.frame, Color.White, NPC.rotation, HalfSize, NPC.scale, SpriteEffects, 0);
                }
            }
        }

        public void NPCLeave(float leaveVelocity, int leaveTime)
        {
            NPC.velocity.Y -= leaveVelocity;
            NPC.EncourageDespawn(leaveTime);
        }

        public void PlayFrame(int frameHeight, bool playerFrameByVelocity = false, float frameSpeed = 2.5f)
        {
            int startFrame = 0;
            int finalFrame = FrameCount - 1;

            NPC.frameCounter += 0.5f;

            if (playerFrameByVelocity)
            {
                NPC.frameCounter += NPC.velocity.Length() / 10f;
            }
            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }
    }
}
