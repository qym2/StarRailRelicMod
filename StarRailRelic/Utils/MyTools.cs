using Microsoft.Xna.Framework.Graphics;
using StarRailRelic.Common.EntitySource;

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
        #endregion

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
    }
}
