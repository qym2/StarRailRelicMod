using StarRailRelic.Common.EntitySource;
using System.Linq.Expressions;

namespace StarRailRelic.Utils
{
    public static class MyTools
    {
        #region 属性
        /// <summary>
        /// 根据天色的亮度所对应的时间
        /// </summary>
        /// <remarks>
        /// 由于Terraria的Main.time是根据Main.dayTime来调整的
        /// <code>
        /// if (Main.dayTime)
        /// {
        ///     Main.time = //从4:31到19:30为0到54000
        /// }
        /// else
        /// {
        ///     Main.time = //从19:31到4:30为0到32400
        /// }
        /// </code>
        /// 导致没有一个字段用来存储一整天的时间
        /// 本属性则是从12:01到第二天12:00为0到86400
        /// </remarks>
        public static double TimeBySunlight
        {
            get
            {
                if (Main.dayTime)
                {
                    if (Main.time >= 27000)
                    {
                        return (int)(Main.time - 27000);
                    }
                    else
                    {
                        return (int)(Main.time + 59400);
                    }
                }
                else
                {
                    return (int)(Main.time + 27000);
                }
            }
        }

        /// <summary>
        /// 最大时间值
        /// </summary>
        public const double TimeMax = 86400;
        #endregion

        #region Item拓展方法
        /// <summary>
        /// 检查给定的物品是否是有效的遗器，并输出对应的 ModRelic 实例。
        /// <para>有效的遗物必须满足以下条件：</para>
        /// <list type="bullet">
        /// <item><description>物品不为空。</description></item>
        /// <item><description>物品类型大于 ItemID.None。</description></item>
        /// <item><description>物品堆叠数量大于 0。</description></item>
        /// <item><description>物品的 ModItem 是 ModRelic 类型。</description></item>
        /// </list>
        /// </summary>
        /// <param name="item">要检查的物品实例。</param>
        /// <param name="relic">如果物品是有效的遗物，将输出对应的 ModRelic 实例；否则为 null。</param>
        /// <returns>如果物品是有效的遗物返回 true，否则返回 false。</returns>
        public static bool IsValidRelic(this Item item, out ModRelic relic)
        {
            if (item != null && item.type > ItemID.None && item.stack > 0 && item.ModItem is ModRelic modRelic)
            {
                relic = modRelic;
            }
            else
            {
                relic = null;
            }

            return relic != null;
        }

        public static bool IsValidRelic(this Item item)
        {
            ModRelic relic;
            if (item != null && item.type > ItemID.None && item.stack > 0 && item.ModItem is ModRelic modRelic)
            {
                relic = modRelic;
            }
            else
            {
                relic = null;
            }

            return relic != null;
        }

        /// <summary>
        /// 绘制物品的发光蒙版，代码来自CalamityMod
        /// </summary>
        /// <param name="item">目标物品</param>
        /// <param name="spriteBatch">要绘制的SpriteBatch实例</param>
        /// <param name="rotation">物品的旋转角度</param>
        /// <param name="glowmaskTexture">发光蒙版的纹理</param>
        public static void DrawItemGlowmaskSingleFrame(this Item item, SpriteBatch spriteBatch, float rotation, Texture2D glowmaskTexture, float alpha = 1)
        {
            Vector2 origin = new(glowmaskTexture.Width / 2f, glowmaskTexture.Height / 2f);

            Color color = new(250, 250, 250);
            color *= alpha;

            spriteBatch.Draw(glowmaskTexture, item.Center - Main.screenPosition, null, color, rotation, origin, 1f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// 绘制物品的动态描边效果，代码来自SpearToJavelin
        /// </summary>
        /// <param name="item">目标物品</param>
        /// <param name="spriteBatch">要绘制的SpriteBatch实例</param>
        /// <param name="position">绘制位置</param>
        /// <param name="frame"></param>
        /// <param name="origin">绘制原点</param>
        /// <param name="scale">缩放</param>
        public static void DrawDynamicOutline(this Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Vector2 origin, float scale)
        {
            Texture2D tex = TextureAssets.Item[item.type].Value;
            float timer = Math.Abs((Main.GlobalTimeWrappedHourly % 1) - 0.5f) * 2f;
            float range = 1 + timer * 2f;
            Color color = Color.White * (1f / timer);
            color.A = (byte)(color.A * 0.5f);
            for (int i = 0; i < 6; i++)
            {
                Vector2 dir = ToRadians(i * 60).ToRotationVector2();
                spriteBatch.Draw(tex, position + dir * range, frame, color, 0f, origin, scale, 0, 0f);
            }
        }
        #endregion

        #region Player拓展方法
        /// <summary>
        /// 为目标 Player 恢复指定数量的生命，并可选择广播恢复效果给其他玩家。
        /// </summary>
        /// <param name="player">目标 Player 对象，表示需要恢复生命的玩家实例。</param>
        /// <param name="healAmount">恢复的生命值，必须为正值，表示希望恢复的具体生命数量。</param>
        /// <param name="broadcast">是否广播恢复效果。默认为 true，表示会向其他玩家显示恢复效果；如果不希望广播恢复效果，可以将此参数设置为 false。</param>
        /// <remarks>
        /// 该方法会自动检查目标 Player 的当前生命值是否超过最大生命值：
        /// <para>如果恢复后的生命值超过最大值，会将其设置为最大生命值。</para>
        /// </remarks>
        public static void RestoreLife(this Player player, float healAmount, bool broadcast = true)
        {
            player.statLife += (int)healAmount;

            if (player.statLife > player.statLifeMax2)
            {
                player.statLife = player.statLifeMax2;
            }

            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect((int)healAmount, broadcast);
            }
        }

        /// <summary>
        /// 获取玩家背包中第一件指定类型物品的索引.
        /// </summary>
        /// <param name="player">目标玩家实例.</param>
        /// <param name="itemType">要查找的物品类型的 ID.</param>
        /// <returns>返回第一件指定物品的索引；如果没有找到，则返回 -1.</returns>
        public static int GetFirstItemIndex(this Player player, int itemType)
        {
            for (int i = 0; i < player.inventory.Length; i++)
            {
                if (player.inventory[i].type == itemType)
                {
                    return i; // 找到第一件指定物品并返回其索引
                }
            }
            return -1; // 如果没有找到，返回-1
        }

        /// <summary>
        /// 统计玩家背包中指定类型物品的总数量.
        /// </summary>
        /// <param name="player">目标玩家实例.</param>
        /// <param name="itemType">要统计的物品类型的 ID.</param>
        /// <returns>返回指定物品类型在玩家背包中的总数量.</returns>
        public static int CountPlayerItems(this Player player, int itemType)
        {
            int count = 0;
            for (int i = 0; i < player.inventory.Length; i++)
            {
                // 检查当前物品类型是否是所需的物品类型
                if (player.inventory[i].type == itemType)
                {
                    count += player.inventory[i].stack; // 累加堆叠数量
                }
            }
            return count;
        }

        /// <summary>
        /// 从玩家背包中消耗指定数量的物品.
        /// </summary>
        /// <param name="player">目标玩家实例.</param>
        /// <param name="itemType">要消耗的物品类型的 ID.</param>
        /// <param name="amount">要消耗的物品数量.</param>
        public static void ConsumeItems(this Player player, int itemType, int amount)
        {
            // 从玩家背包中消耗指定数量的物品
            int remaining = amount;
            for (int i = 0; i < player.inventory.Length; i++)
            {
                if (player.inventory[i].type == itemType && player.inventory[i].stack > 0)
                {
                    if (player.inventory[i].stack >= remaining)
                    {
                        player.inventory[i].stack -= remaining; // 直接从当前物品格子中减去剩余数量
                        break;
                    }
                    else
                    {
                        remaining -= player.inventory[i].stack; // 减去当前堆叠的数量
                        player.inventory[i].SetDefaults();
                    }
                }
            }
        }

        /// <summary>
        /// 获取和当前玩家在同一队伍中的其他玩家
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Player[] GetTeammates(this Player player)
        {
            return Main.player.Where(p => p.active && p.team == player.team && p != player).ToArray();
        }

        /// <summary>
        /// 获取和当前玩家在同一队伍中的所有玩家，包括自己
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Player[] GetAllTeamPlayers(this Player player)
        {
            return Main.player.Where(p => p.active && p.team == player.team).ToArray();
        }
        #endregion

        #region Projectilet拓展方法
        /// <summary>
        /// 根据投射物的速度设置它的旋转
        /// </summary>
        /// <param name="Projectile">目标投射物</param>
        /// <param name="isInclined">图片是否为斜角</param>
        /// <param name="isReversed">投射物反向</param>
        /// <remarks>
        /// 根据投射物的速度设置它的旋转，并根据图片是否为斜角来调整旋转角度。
        /// </remarks>
        public static void RotateByVelocity(this Projectile Projectile, bool isDown = false, bool isInclined = true, bool isReversed = false, float offset = 0)
        {
            Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;

            Projectile.rotation = Projectile.velocity.ToRotation() + offset;

            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += Pi;
                if (isInclined)
                {
                    Projectile.rotation -= PiOver2;
                }
                if (isDown)
                {
                    Projectile.rotation -= PiOver2;
                }
            }

            if (isInclined)
            {
                Projectile.rotation += PiOver4;
            }
            if (isDown)
            {
                Projectile.rotation += (Pi + PiOver4);
            }

            if (isReversed)
            {
                Projectile.direction = Projectile.spriteDirection = -Projectile.spriteDirection;
            }
        }
        #endregion

        #region 生成源扩展
#nullable enable
        /// <summary>
        /// 生成一个来自NPC被杀死的源，可用于获取NPC是被谁杀死的   
        /// </summary>
        /// <param name="npc">被杀死的NPC</param>
        /// <param name="context">上下文</param>
        /// <returns>来自NPC被杀死的源</returns>
        public static IEntitySource GetSource_NPCKilledBy(this NPC npc, string? context = null)
        {
            return new EntitySource_NPCKilledBy(npc, context);
        }

        /// <summary>
        /// 生成一个来自UI的源，以获取UI的实例
        /// </summary>
        /// <param name="uiState">UI</param>
        /// <param name="context">上下文</param>
        /// <returns>来自UI的源</returns>
        public static IEntitySource GetSource_FromUI(this UIState uiState, string? context = null)
        {
            return new EntitySource_UI(uiState, context);
        }

        /// <summary>
        /// 生成一个来自生成射弹的主人射弹源，以获取主人射弹的实例
        /// </summary>
        /// <param name="projectile">目标射弹</param>
        /// <param name="context">上下文</param>
        /// <returns>主人射弹的源</returns>
        public static IEntitySource GetSource_OwnerProjectile(this Projectile projectile, string? context = null)
        {
            return new EntitySource_OwnerProjectile(projectile, context);
        }

        /// <summary>
        /// 生成一个来自生成射弹的主人NPC源，以获取主人NPC的实例
        /// </summary>
        /// <param name="projectile">目标射弹</param>
        /// <param name="context">上下文</param>
        /// <returns>主人射弹的源</returns>
        public static IEntitySource GetSource_OwnerNPC(this NPC npc, string? context = null)
        {
            return new EntitySource_OwnerNPC(npc, context);
        }
        #endregion

        #region NPC拓展方法
        public static int[] DebuffType(this NPC npc)
        {
            List<int> debuffType = [];

            for (int i = 0; i < NPC.maxBuffs; i++)
            {
                if (Main.debuff[npc.buffType[i]])
                {
                    debuffType.Add(npc.buffType[i]);
                }
            }

            return [.. debuffType];
        }
        
        /// <summary>
        /// 检测目标npc是否是Boss
        /// </summary>
        /// <param name="npc">目标npc</param>
        /// <returns>是否是Boss</returns>
        public static bool IsBoss(this NPC npc)
        {
            return npc.boss ||
                   npc.type is NPCID.EaterofWorldsHead
                            or NPCID.EaterofWorldsBody
                            or NPCID.EaterofWorldsTail;
        }

        /// <summary>
        /// 通过NPC的位置获取其绘制的原点
        /// </summary>
        /// <param name="NPC">目标NPC</param>
        /// <param name="position">NPC的位置</param>
        /// <param name="screenPos">屏幕位置</param>
        /// <returns></returns>
        public static Vector2 GetNPCDrawPosition(this NPC NPC, Vector2 position, Vector2 screenPos)
        {
            Asset<Texture2D> textureAsset = TextureAssets.Npc[NPC.type];
            Vector2 halfSize = new(textureAsset.Width() / 2, textureAsset.Height() / Main.npcFrameCount[NPC.type] / 2);
            return new(position.X - screenPos.X + NPC.width / 2 - textureAsset.Width() * NPC.scale / 2f + halfSize.X * NPC.scale, position.Y - screenPos.Y + NPC.height - textureAsset.Height() * NPC.scale / Main.npcFrameCount[NPC.type] + 4f + halfSize.Y * NPC.scale + NPC.gfxOffY);
        }

        /// <summary>
        /// 通过加速度和速度大小、方向为实体平滑设置速度
        /// </summary>
        /// <param name="entity">目标实体</param>
        /// <param name="acceleration">加速度</param>
        /// <param name="targetVelocity">速度大小、方向</param>
        public static void SetVelocity(this Entity entity, float acceleration, Vector2 targetVelocity)
        {
            if (entity.velocity.X < targetVelocity.X)
            {
                entity.velocity.X += acceleration;
                if (entity.velocity.X < 0f && targetVelocity.X > 0f)
                {
                    entity.velocity.X += acceleration;
                }
            }
            else if (entity.velocity.X > targetVelocity.X)
            {
                entity.velocity.X -= acceleration;
                if (entity.velocity.X > 0f && targetVelocity.X < 0f)
                {
                    entity.velocity.X -= acceleration;
                }
            }

            if (entity.velocity.Y < targetVelocity.Y)
            {
                entity.velocity.Y += acceleration;
                if (entity.velocity.Y < 0f && targetVelocity.Y > 0f)
                {
                    entity.velocity.Y += acceleration;
                }
            }
            else if (entity.velocity.Y > targetVelocity.Y)
            {
                entity.velocity.Y -= acceleration;
                if (entity.velocity.Y > 0f && targetVelocity.Y < 0f)
                {
                    entity.velocity.Y -= acceleration;
                }
            }
        }

        /// <summary>
        /// 通过加速度和速度大小为NPC平滑设置速度
        /// </summary>
        /// <param name="entity">目标NPC</param>
        /// <param name="acceleration">加速度</param>
        /// <param name="speed">速度</param>
        public static void SetVelocity(this NPC entity, float acceleration, float speed)
        {

            if (entity.direction == -1 && entity.velocity.X > 0f - speed)
            {
                entity.velocity.X -= 0.1f;
                if (entity.velocity.X > speed)
                {
                    entity.velocity.X -= 0.1f;
                }
                else if (entity.velocity.X > 0f)
                {
                    entity.velocity.X += 0.05f;
                }

                if (entity.velocity.X < 0f - speed)
                {
                    entity.velocity.X = 0f - speed;
                }
            }
            else if (entity.direction == 1 && entity.velocity.X < speed)
            {
                entity.velocity.X += 0.1f;
                if (entity.velocity.X < 0f - speed)
                {
                    entity.velocity.X += 0.1f;
                }
                else if (entity.velocity.X < 0f)
                {
                    entity.velocity.X -= 0.05f;
                }

                if (entity.velocity.X > speed)
                {
                    entity.velocity.X = speed;
                }
            }

            if (entity.directionY == -1 && entity.velocity.Y > 0f - acceleration)
            {
                entity.velocity.Y -= 0.04f;
                if (entity.velocity.Y > acceleration)
                {
                    entity.velocity.Y -= 0.05f;
                }
                else if (entity.velocity.Y > 0f)
                {
                    entity.velocity.Y += 0.03f;
                }

                if (entity.velocity.Y < 0f - acceleration)
                {
                    entity.velocity.Y = 0f - acceleration;
                }
            }
            else if (entity.directionY == 1 && entity.velocity.Y < acceleration)
            {
                entity.velocity.Y += 0.04f;
                if (entity.velocity.Y < 0f - acceleration)
                {
                    entity.velocity.Y += 0.05f;
                }
                else if (entity.velocity.Y < 0f)
                {
                    entity.velocity.Y -= 0.03f;
                }

                if (entity.velocity.Y > acceleration)
                {
                    entity.velocity.Y = acceleration;
                }
            }
        }

        /// <summary>
        /// 使NPC在碰撞时反弹
        /// </summary>
        /// <param name="npc">目标NPC</param>
        public static void BounceWhenTileCollide(this NPC npc)
        {
            if (!npc.noTileCollide)
            {
                if (npc.collideX)
                {
                    npc.velocity.X = npc.oldVelocity.X * -0.5f;
                    if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                    {
                        npc.velocity.X = 2f;
                    }

                    if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                    {
                        npc.velocity.X = -2f;
                    }
                }

                if (npc.collideY)
                {
                    npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                    if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                    {
                        npc.velocity.Y = 1f;
                    }

                    if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                    {
                        npc.velocity.Y = -1f;
                    }
                }
            }
        }
        #endregion

        #region Entity拓展方法
        /// <summary>
        /// 使实体碰到水面后就立刻脱离水面
        /// </summary>
        /// <param name="entity">目标实体</param>
        public static void EscapeFromWater(this Entity entity)
        {
            if (entity.wet)
            {
                if (entity.velocity.Y > 0f)
                {
                    entity.velocity.Y *= 0.95f;
                }

                entity.velocity.Y -= 0.5f;
                if (entity.velocity.Y < -4f)
                {
                    entity.velocity.Y = -4f;
                }

                if (entity is NPC npc && !npc.friendly)
                {
                    npc.TargetClosest();
                }
            }
        }

        /// <summary>
        /// 如果实体速度过小，就直接设置为0
        /// </summary>
        /// <param name="entity">目标实体</param>
        public static void LimitVelocity(this Entity entity)
        {
            if (entity.velocity.X > -0.1 && entity.velocity.X < 0.1)
            {
                entity.velocity.X = 0f;
            }
            if (entity.velocity.Y > -0.1 && entity.velocity.Y < 0.1)
            {
                entity.velocity.Y = 0f;
            }
        }
        #endregion

        #region 粒子效果
        public static void CreateVanillaFireExplosion(Vector2 position, float intensity = 1f)
        {
            // 火焰核心粒子（使用 Torch 和 Fire 类型）
            for (int i = 0; i < 15 * intensity; i++)
            {
                Dust fire = Dust.NewDustPerfect(
                    position,
                    Main.rand.NextBool() ? DustID.Torch : DustID.FlameBurst,
                    Main.rand.NextVector2Circular(10f, 10f) * intensity,
                    Scale: Main.rand.NextFloat(1f, 2.5f)
                );
                fire.noGravity = true;
                fire.fadeIn = 1.2f;
            }

            // 爆炸烟雾（Smoke 类型）
            for (int i = 0; i < 8 * intensity; i++)
            {
                Dust smoke = Dust.NewDustDirect(
                    position, 0, 0,
                    DustID.Smoke,
                    SpeedX: Main.rand.NextFloat(-3f, 3f) * intensity,
                    SpeedY: Main.rand.NextFloat(-2f, 0.5f) * intensity,
                    Alpha: 150
                );
                smoke.scale *= 1.5f;
            }

            // 余烬火花（CopperCoin 类型）
            for (int i = 0; i < 10 * intensity; i++)
            {
                Dust spark = Dust.NewDustPerfect(
                    position,
                    DustID.CopperCoin,
                    Main.rand.NextVector2Circular(8f, 8f) * intensity,
                    newColor: Color.OrangeRed
                );
                spark.noGravity = true;
                spark.scale = 0.8f;
            }
        }

        public static void CreateNebulaVortex(Vector2 center, float intensity = 1f)
        {
            for (int i = 0; i < 30 * intensity; i++)
            {
                Dust d = Dust.NewDustPerfect(center, DustID.PurpleTorch,
                    Vector2.UnitX.RotatedBy(MathHelper.TwoPi * i / 30) * 5f * intensity);
                d.noGravity = true;
                d.scale = 1.5f * intensity;
                d.velocity += (center - d.position) * 0.02f; // 向心引力
            }
        }

        public static void CreateSakuraPetals(Vector2 center, int count = 20)
        {
            for (int i = 0; i < count; i++)
            {
                Dust d = Dust.NewDustPerfect(center, DustID.PinkStarfish,
                    new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(1f, 3f)));
                d.rotation = Main.rand.NextFloat(TwoPi);
                d.velocity.Y += 0.1f; // 重力模拟
                d.noGravity = false;
            }
        }

        public static void CreateHologram(Vector2 center, float size = 100f)
        {
            for (int x = -50; x <= 50; x += 10)
            {
                for (int y = -50; y <= 50; y += 10)
                {
                    Vector2 pos = center + new Vector2(x, y) * (size / 100f);
                    Dust d = Dust.NewDustPerfect(pos, DustID.BlueFairy,
                        new Vector2(0, (float)Math.Sin(Main.time * 3f + x) * 0.5f));
                    d.noGravity = true;
                    d.scale = 0.8f;
                }
            }
        }

        public static void CreateLavaEruption(Vector2 position, float power = 1f)
        {
            for (int i = 0; i < 20 * power; i++)
            {
                Dust d = Dust.NewDustDirect(position, 0, 0, DustID.Lava);
                d.velocity = Main.rand.NextVector2Circular(8f, 8f) * power;
                d.noGravity = true;
                d.scale = Main.rand.NextFloat(1f, 2f);

                // 拖尾效果
                Dust.NewDustPerfect(d.position, DustID.Torch,
                    d.velocity * 0.5f, Scale: d.scale * 0.7f);
            }
        }

        public static void CreateAurora(Vector2 start, float width = 400f, float speed = 1f)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 pos = start + new Vector2(Main.rand.NextFloat(width), 0);
                Dust d = Dust.NewDustPerfect(pos, DustID.GemEmerald,
                    new Vector2(0, Main.rand.NextFloat(-1f, -0.5f) * speed));
                d.scale = Main.rand.NextFloat(1f, 1.8f);
                d.noGravity = true;
                d.color = Color.Lerp(Color.Cyan, Color.Purple, Main.rand.NextFloat());
            }
        }

        public static void CreateMagicRune(Vector2 center, float size = 100f)
        {
            // 绘制圆形符文
            for (int i = 0; i < 36; i++)
            {
                Vector2 pos = center + Vector2.UnitX.RotatedBy(TwoPi * i / 36) * size;
                Dust d = Dust.NewDustPerfect(pos, DustID.MagicMirror,
                    Vector2.Zero, Scale: 1.5f);
                d.noGravity = true;
                d.velocity = Vector2.UnitY * (float)Math.Sin(Main.time * 3f + i) * 0.3f;
            }
        }
        #endregion

        #region 数学计算

        /// <summary>
        /// 根据三角形的两直角边长计算斜边
        /// </summary>
        /// <param name="a">a边</param>
        /// <param name="b">b边</param>
        /// <returns></returns>
        public static float Pythagorean(float a, float b)
        {
            return (float)Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// 标准化角度，使其在0-360之间
        /// </summary>
        /// <param name="angle">目标角度</param>
        public static void NormalizeAngle(ref float angle)
        {
            if (angle < 0f)
            {
                angle += Pi * 2;
            }
            else if (angle > Pi * 2)
            {
                angle -= Pi * 2;
            }
        }

        public static Vector2 RotateVector(Vector2 v, float degrees)
        {
            float radians = (float)(degrees * Math.PI / 180);
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            return new Vector2(
                v.X * cos - v.Y * sin,
                v.X * sin + v.Y * cos
            );
        }

        public static Vector2 GetRandomRotated(this Vector2 original, float maxAngleRadians)
        {
            float randomAngle = Main.rand.NextFloatDirection() * ToRadians(maxAngleRadians);
            return original.RotatedBy(randomAngle);
        }
        #endregion

        #region 其它
        public static void SpawnClusteredDusts(float timer, Player player, Vector2 position, int dustID)
        {
            float aTime = Clamp(150 - timer, 0f, 150f);

            float targetRot3 = PiOver2 + player.direction * (float)Math.Pow(1f - aTime / 150f, 0.6f);
            Vector2 vector = Vector2.Zero;

            float cos1 = (float)Math.Cos(-targetRot3);
            float sin1 = (float)Math.Sin(-targetRot3);

            Vector3 originalVector = Vector3.UnitX * 80f;

            float rotatedX = originalVector.X * cos1 - originalVector.Y * sin1;
            float rotatedY = originalVector.X * sin1 + originalVector.Y * cos1;

            Vector3 v = new(rotatedX, rotatedY, originalVector.Z);

            float angle2 = -0.8f;
            float cos2 = (float)Math.Cos(angle2);
            float sin2 = (float)Math.Sin(angle2);

            float newRotatedX = v.X;
            float newRotatedY = v.Y * cos2 - v.Z * sin2;
            float newRotatedZ = v.Y * sin2 + v.Z * cos2;

            v = new Vector3(newRotatedX, newRotatedY, newRotatedZ);

            Vector2 vector2 = -500f / (v.Z - 500f) * new Vector2(v.X, v.Y);
            vector = Vector2.Lerp(vector, vector2, 0.1f);

            for (int i = 0; i <= 6; i++)
            {
                if (Main.rand.NextBool(5))
                {
                    Vector2 r = Main.rand.NextVector2Unit();
                    Dust dust = Dust.NewDustDirect(position + (float)Math.Pow(i / 6f, 0.5) * vector + r * aTime, 10, 10, dustID, 0f, 0f, 0, new Color(200, 50, 80), 2f);
                    dust.velocity = -r * 4f;
                    dust.position += Main.rand.NextVector2Unit() * 5f;
                    dust.noGravity = true;
                }
            }
        }

        public static void AddModifiersAdditive(this ref NPC.HitModifiers modifiers, Player player, float additive)
        {
            modifiers.FinalDamage *= 1 + (additive / player.GetTotalDamage<MagicDamageClass>().Additive);
        }

        public static void AddModifiersCrit(this ref NPC.HitModifiers modifiers, Player player, float crit)
        {
            if (modifiers.DamageType == DamageClass.Default)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.Default) + crit)
                {
                    modifiers.SetCrit();
                }
            }
            else if (modifiers.DamageType == DamageClass.Generic)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.Generic) + crit)
                {
                    modifiers.SetCrit();
                }
            }
            else if (modifiers.DamageType == DamageClass.Magic)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.Magic) + crit)
                {
                    modifiers.SetCrit();
                }
            }
            else if (modifiers.DamageType == DamageClass.MagicSummonHybrid)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.MagicSummonHybrid) + crit)
                {
                    modifiers.SetCrit();
                }
            }
            else if (modifiers.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.Melee) + crit)
                {
                    modifiers.SetCrit();
                }
            }
            else if (modifiers.DamageType == DamageClass.MeleeNoSpeed)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.MeleeNoSpeed) + crit)
                {
                    modifiers.SetCrit();
                }
            }
            else if (modifiers.DamageType == DamageClass.Ranged)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.Ranged) + crit)
                {
                    modifiers.SetCrit();
                }
            }
            else if (modifiers.DamageType == DamageClass.Summon)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.Summon) + crit)
                {
                    modifiers.SetCrit();
                }
            }
            else if (modifiers.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.SummonMeleeSpeed) + crit)
                {
                    modifiers.SetCrit();
                }
            }
            else if (modifiers.DamageType == DamageClass.Throwing)
            {
                if (Main.rand.NextFloat() * 100 < player.GetTotalCritChance(DamageClass.Throwing) + crit)
                {
                    modifiers.SetCrit();
                }
            }
        }

        public static Item NewItemSycn(IEntitySource entitySource, Vector2 position, int type, int stack = 1)
        {
            int item = Item.NewItem(entitySource, position, type, stack);

            if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            }

            return Main.item[item];
        }

        public static NPC NewNPCSycn(IEntitySource entitySource, Vector2 position, int type)
        {
            int npc = NPC.NewNPC(entitySource, (int)position.X, (int)position.Y, type);

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.SyncNPC, number: npc);
            }

            return Main.npc[npc];
        }

        public static string GetName<T>(Expression<Func<T>> expr)
        {
            if (expr.Body is MemberExpression memberExpr)
            {
                return memberExpr.Member.Name;
            }
            return "Unknown";
        }

        public static void NewText<T>(Expression<Func<T>> expr)
        {
            string name = GetName(expr);
            T value = expr.Compile()();
            Main.NewText($"{name}: {value}");
        }

        /// <summary>
        /// 获取NPC的显示纹理（优先使用头像，没有则提取单帧）
        /// </summary>
        /// <param name="npcType">NPC类型ID</param>
        /// <returns>适合显示的纹理</returns>
        public static Texture2D GetNPCDisplayTexture(int npcType)
        {
            NPC tempNPC = new();
            tempNPC.SetDefaults(npcType);

            int headIndex = NPC.TypeToDefaultHeadIndex(npcType);
            int headIndexBoss = tempNPC.GetBossHeadTextureIndex();
            if (NPC.TypeToDefaultHeadIndex(npcType) > -1)
            {
                return TextureAssets.NpcHead[headIndex].Value;
            }
            else if (tempNPC.GetBossHeadTextureIndex() > -1)
            {
                return TextureAssets.NpcHeadBoss[headIndexBoss].Value;
            }

            Texture2D fullTexture = TextureAssets.Npc[npcType].Value;
            Rectangle frameRect = tempNPC.frame;

            // 创建仅包含单帧的新纹理
            Texture2D frameTexture = new(Main.graphics.GraphicsDevice, frameRect.Width, frameRect.Height);

            // 从完整纹理中读取像素并复制到新纹理
            Color[] pixels = new Color[frameRect.Width * frameRect.Height];
            fullTexture.GetData(0, frameRect, pixels, 0, pixels.Length);
            frameTexture.SetData(pixels);

            return frameTexture;
        }

        /// <summary>
        /// 将整数转换为罗马数字字符串（支持 1-3999）
        /// </summary>
        /// <param name="number">要转换的整数（范围 1-3999）</param>
        /// <returns>对应的罗马数字字符串，超出范围时返回原数字字符串</returns>
        public static string IntToRoman(int number)
        {
            if (number < 1 || number > 3999)
            {
                return number.ToString(); // 超出范围返回原数字
            }

            string[] romanSymbols = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < values.Length; i++)
            {
                while (number >= values[i])
                {
                    result.Append(romanSymbols[i]);
                    number -= values[i];
                }
            }

            return result.ToString();
        }
        #endregion
    }
}
