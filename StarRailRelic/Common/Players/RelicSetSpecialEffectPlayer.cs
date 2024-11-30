using Terraria.WorldBuilding;

namespace StarRailRelic.Common.Players
{
    public class RelicSetSpecialEffectPlayer : ModPlayer
    {
        public bool IsQuantumFourSet { get; set; }

        public bool IsGuardFourSet { get; set; }
        private int guardCDTimer;
        private const int guardCDDuration = 480;

        public bool IsFireFourSet { get; set; }
        private int fireBoostTimer;
        private const int fireBoostDuration = 180;

        public bool IsHealingTwoSet { get; set; }
        public bool IsHealingFourSet { get; set; }

        public bool IsIceFourSet { get; set; }
        private int iceBoostTimer;
        private const int iceBoostDuration = 180;

        public bool IsImaginaryFourSet { get; set; }

        public bool IsLifeTwoSet { get; set; }
        public bool IsLifeFourSet { get; set; }
        private int lifeBoostTimer;
        private const int lifeBoostDuration = 600;

        public bool IsLightningFourSet { get; set; }
        private bool lightningNoDamageDone;

        public bool IsMessengerFourSet { get; set; }
        private int messengerBoostTimer;
        private const int messengerBoostDuration = 1800;

        public bool IsPhysicsFourSet { get; set; }
        private int physicsBoostTimer;
        private const int physicsBoostDuration = 300;
        private DamageClass physicsLastDamageType;
        private int physicsBoostStacks;
        private const float physicsBoostMaxStacks = 5;

        public bool IsWindFourSet { get; set; }
        private int windConsumedAmmo;
        private int windBoostTimer;
        private const int windBoostDuration = 600;

        public bool IsThiefTwoSet { get; set; }
        public bool IsThiefFourSet { get; set; }

        public bool IsDeadWatersTwoSet { get; set; }
        public bool IsDeadWatersFourSet { get; set; }

        public bool IsDotFourSet { get; set; }

        public bool IsDukeFourSet { get; set; }

        public bool IsFeixiaoFourSet { get; set; }
        private int feixiaoBoostTimer;
        private const int feixiaoBoostDuration = 120;
        private bool feixiaoSuperAmmo;

        public bool IsIronFourSet { get; set; }

        public bool IsSacerdosFourSet { get; set; }
        private int sacerdosBoostTimer;
        private const int sacerdosBoostDuration = 1800;

        public bool IsWatchTwoSet { get; set; }
        public bool IsWatchFourSet { get; set; }
        private int watchBoostTimer;
        private const int watchBoostDuration = 1200;

        public bool IsBanditryTwoSet { get; set; }

        public bool IsDifferentiatorTwoSet { get; set; }

        public bool IsGlamothTwoSet { get; set; }

        public bool IsKeelTwoSet { get; set; }

        public bool IsVonwacqTwoSet { get; set; }
        private int vonwacqBoostTimer;
        private const int vonwacqBoostDuration = 180;

        public bool IsXianzhouTwoSet { get; set; }

        public bool IsBananaTwoSet { get; set; }

        public bool IsSigoniaTwoSet { get; set; }
        public int sigoniaBoostTimer;
        public const int sigoniaBoostDuration = 300;
        public int sigoniaBoostStacks;
        public const float sigoniaBoostMaxStacks = 10;

        public override void ResetEffects()
        {
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(bool) && property.CanWrite)
                {
                    property.SetValue(this, false);
                }
            }
        }

        public override void GetHealMana(Item item, bool quickHeal, ref int healValue)
        {
        }

        public void OnHealMana()
        {
            if (IsFireFourSet)
            {
                fireBoostTimer = fireBoostDuration;
            }

            if (IsLightningFourSet)
            {
                lightningNoDamageDone = true;
            }
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if (IsHealingTwoSet)
            {
                healValue = (int)(healValue * 1.2f);
            }
        }

        public void OnHealLife()
        {
            if (IsMessengerFourSet)
            {
                messengerBoostTimer = messengerBoostDuration;
            }

            if (IsSacerdosFourSet)
            {
                Player[] players = Player.GetTeammates();
                foreach (Player p in players)
                {
                    p.GetModPlayer<RelicSetSpecialEffectPlayer>().sacerdosBoostTimer = sacerdosBoostDuration;
                }
                sacerdosBoostTimer = sacerdosBoostDuration;
            }
        }

        public override void UpdateLifeRegen()
        {
            if (IsHealingFourSet)
            {
                Player.lifeRegen += 8;
            }

            if (IsXianzhouTwoSet)
            {
                Player.lifeRegen += 4;

                if(Player.velocity.Length() == 0)
                {
                    Player.lifeRegen += 12;
                }
            }
        }

        public override void UpdateEquips()
        {
            if (IsGuardFourSet)
            {
                if (guardCDTimer > 0)
                {
                    guardCDTimer--;
                }
                if (guardCDTimer <= 0 && Player.statLife <= Player.statLifeMax2 * 0.50f)
                {
                    Player.RestoreLife(Player.statLifeMax2 * 0.04f);
                    guardCDTimer = guardCDDuration;
                }
            }

            if (IsFireFourSet)
            {
                if (fireBoostTimer > 0)
                {
                    Player.GetDamage<MagicDamageClass>() += 10f / 100f;
                    fireBoostTimer--;
                }
            }

            if (IsIceFourSet)
            {
                if (iceBoostTimer > 0)
                {
                    Player.GetAttackSpeed<MeleeDamageClass>() += 10f / 100f;
                    Player.GetAttackSpeed<SummonMeleeSpeedDamageClass>() += 10f / 100f;
                    iceBoostTimer--;
                }
            }

            if (IsLifeFourSet)
            {
                if(lifeBoostTimer > 0)
                {
                    Player.GetCritChance<GenericDamageClass>() += 8;
                    lifeBoostTimer--;
                }
            }

            if (IsMessengerFourSet)
            {
                if (messengerBoostTimer > 0)
                {
                    Player[] players = Player.GetTeammates();
                    foreach (Player p in players)
                    {
                        p.runAcceleration *= 1.75f;
                        p.maxRunSpeed *= 1.15f;
                        p.accRunSpeed *= 1.15f;
                        p.runSlowdown *= 1.75f;
                    }
                    Player.runAcceleration *= 1.75f;
                    Player.maxRunSpeed *= 1.15f;
                    Player.accRunSpeed *= 1.15f;
                    Player.runSlowdown *= 1.75f;
                    messengerBoostTimer--;
                }
            }

            if (IsPhysicsFourSet)
            {
                if(physicsBoostTimer > 0)
                {
                    Player.GetDamage<GenericDamageClass>() += 5 * physicsBoostStacks / 100f;
                    physicsBoostTimer--;
                }
                else
                {
                    physicsBoostStacks = 0;
                }
            }

            if (IsWindFourSet)
            {
                if(windConsumedAmmo >= 40)
                {
                    windConsumedAmmo -= 40;
                    windBoostTimer = windBoostDuration;
                }

                if(windBoostTimer > 0)
                {
                    Player.runAcceleration *= 2.25f;
                    Player.maxRunSpeed *= 1.25f;
                    Player.accRunSpeed *= 1.25f;
                    Player.runSlowdown *= 2.25f;
                    windBoostTimer--;
                }
            }

            if (IsFeixiaoFourSet)
            {
                if (!feixiaoSuperAmmo && Player.HeldItem.DamageType == DamageClass.Ranged)
                {
                    feixiaoBoostTimer++;

                    if (feixiaoBoostTimer >= feixiaoBoostDuration)
                    {
                        feixiaoSuperAmmo = true;
                        feixiaoBoostTimer = 0;
                    }
                }
            }

            if (sacerdosBoostTimer > 0)
            {
                sacerdosBoostTimer--;
            }

            if (IsWatchFourSet)
            {
                if(watchBoostTimer > 0)
                {
                    Player[] players = Player.GetTeammates();
                    foreach (Player p in players)
                    {
                        p.GetDamage<GenericDamageClass>() += 12 / 100f;
                    }
                    Player.GetDamage<GenericDamageClass>() += 12 / 100f;
                    watchBoostTimer--;
                }
            }

            if (IsGlamothTwoSet)
            {
                float maxHealthSpeed = 0f;
                float maxHealth = 0f;

                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && npc.boss)
                    {
                        if (npc.lifeMax > maxHealth)
                        {
                            maxHealth = npc.lifeMax;
                            maxHealthSpeed = npc.velocity.Length();
                        }
                    }
                }

                if (maxHealthSpeed > 0 && Player.velocity.Length() / maxHealthSpeed <= 2)
                {
                    Player.GetDamage<GenericDamageClass>() += 10 / 100f * Player.velocity.Length() / maxHealthSpeed;
                }
                else
                {
                    Player.GetDamage<GenericDamageClass>() += 20 / 100f;
                }
            }
            
            if (IsVonwacqTwoSet)
            {
                if(vonwacqBoostTimer > 0)
                {
                    Player.moveSpeed += 2f;
                    vonwacqBoostTimer--;
                }
            }

            if (IsSigoniaTwoSet)
            {
                if (sigoniaBoostTimer > 0)
                {
                    sigoniaBoostTimer--;
                }
                else
                {
                    sigoniaBoostStacks = 0;
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (IsQuantumFourSet && (modifiers.DamageType == DamageClass.Melee || modifiers.DamageType == DamageClass.MeleeNoSpeed))
            {
                modifiers.ArmorPenetration += 4;
            }
            
            if (IsImaginaryFourSet)
            {
                float distance = Vector2.Distance(Player.Center, target.Center);

                float distanceThreshold = 600;

                if (distance <= distanceThreshold)
                {
                    modifiers.AddModifiersCrit(Player, 8);
                }

                if(Player.velocity.Length() >= target.velocity.Length())
                {
                    modifiers.CritDamage += 15 / 100f;
                }
            }

            if (IsLightningFourSet)
            {
                if (lightningNoDamageDone && modifiers.DamageType == DamageClass.Magic)
                {
                    modifiers.AddModifiersAdditive(Player, 250 / 100f);
                    lightningNoDamageDone = false;
                }
            }

            if (IsDeadWatersTwoSet)
            {
                if(target.DebuffType().Length != 0)
                {
                    modifiers.AddModifiersAdditive(Player, 6 / 100f);
                }
            }

            if (IsDeadWatersFourSet)
            {
                if (target.DebuffType().Length >= 3)
                {
                    modifiers.CritDamage += 12 / 100f;
                }
                else if (target.DebuffType().Length >= 2)
                {
                    modifiers.CritDamage += 8 / 100f;
                }
            }

            if (IsDotFourSet)
            {
                if (target.lifeRegen < 0)
                {
                    modifiers.ArmorPenetration += 8;
                }
            }

            if (IsDukeFourSet && (modifiers.DamageType == DamageClass.Magic || modifiers.DamageType == DamageClass.MagicSummonHybrid))
            {
                modifiers.DamageVariationScale *= 0;
                int ownProjectileCount = 0;
                foreach (Projectile projectile in Main.projectile)
                {
                    float distance = Vector2.Distance(Player.Center, projectile.Center);

                    float distanceThreshold = 1200;

                    if (distance <= distanceThreshold && projectile.active && projectile.owner == Player.whoAmI && (projectile.DamageType == DamageClass.Magic || projectile.DamageType == DamageClass.MagicSummonHybrid) && ownProjectileCount < 8)
                    {
                        ownProjectileCount++;
                    }
                }

                modifiers.AddModifiersAdditive(Player, ownProjectileCount * 3 / 100f);
            }

            if (IsIronFourSet)
            {
                float distance = Vector2.Distance(Player.Center, target.Center);

                float distanceThreshold1 = 300;
                float distanceThreshold2 = 200;

                if (distance <= distanceThreshold1)
                {
                    modifiers.ArmorPenetration += 5;
                }
                if (distance <= distanceThreshold2)
                {
                    modifiers.ArmorPenetration += 10;
                }
            }

            if (sacerdosBoostTimer > 0)
            {
                bool isAnyTeammatesSacerdosFourSet = false;

                Player[] players = Player.GetAllTeamPlayers();
                foreach (Player p in players)
                {
                    if (p.GetModPlayer<RelicSetSpecialEffectPlayer>().IsSacerdosFourSet)
                    {
                        isAnyTeammatesSacerdosFourSet = true;
                        break;
                    }
                }

                if(isAnyTeammatesSacerdosFourSet)
                {
                    modifiers.CritDamage += 12 / 100f;
                }

                sacerdosBoostTimer--;
            }

            if (IsDifferentiatorTwoSet)
            {
                modifiers.CritDamage += 10 / 100f;

                if (modifiers.CritDamage.Additive > 2.1)
                {
                    modifiers.AddModifiersCrit(Player, 8);
                }
            }

            if (true)
            {
                bool isAnyTeammatesKeelTwoSetAndEndurance35 = false;

                Player[] players = Player.GetAllTeamPlayers();
                foreach (Player p in players)
                {
                    if (p.GetModPlayer<RelicSetSpecialEffectPlayer>().IsKeelTwoSet && p.endurance > 0.35f)
                    {
                        isAnyTeammatesKeelTwoSetAndEndurance35 = true;
                        break;
                    }
                }

                if (isAnyTeammatesKeelTwoSetAndEndurance35)
                {
                    modifiers.CritDamage += 8 / 100f;
                }
            }

            if (IsBananaTwoSet)
            {
                modifiers.CritDamage += 6 / 100f;

                int minionCount = 0;
                foreach (Projectile projectile in Main.projectile)
                {
                    if(projectile.active && projectile.minion && projectile.owner == Player.whoAmI)
                    {
                        minionCount++;
                    }
                }

                if (minionCount >= 3)
                {
                    modifiers.CritDamage += 12 / 100f;
                }
            }

            if (IsSigoniaTwoSet)
            {
                if (sigoniaBoostTimer > 0)
                {
                    modifiers.CritDamage += 2 * sigoniaBoostStacks / 100f;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if ((hit.DamageType == DamageClass.SummonMeleeSpeed || hit.DamageType == DamageClass.Summon) && IsIceFourSet)
            {
                iceBoostTimer = iceBoostDuration;
            }

            if (IsPhysicsFourSet)
            {
                if (physicsLastDamageType != null)
                {
                    if (hit.DamageType == DamageClass.SummonMeleeSpeed && physicsLastDamageType == DamageClass.Summon)
                    {
                        if (physicsBoostStacks < physicsBoostMaxStacks)
                        {
                            physicsBoostStacks++;
                        }
                        physicsBoostTimer = physicsBoostDuration;
                    }
                    if (hit.DamageType == DamageClass.SummonMeleeSpeed && physicsLastDamageType == DamageClass.SummonMeleeSpeed)
                    {
                        physicsBoostStacks = 0;
                        physicsBoostTimer = 0;
                    }
                }
                physicsLastDamageType = hit.DamageType;
            }
        }

        public override void OnConsumeAmmo(Item weapon, Item ammo)
        {
            if (IsWindFourSet)
            {
                windConsumedAmmo++;
            }
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (feixiaoSuperAmmo && item.DamageType == DamageClass.Ranged)
            {
                damage = (int)(damage * (1 + (500 / 100f)));
                velocity *= 1 + (500 / 100f);
                knockback *= 1 + (500 / 100f);
                feixiaoSuperAmmo = false;
            }

            feixiaoBoostTimer = 0;
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (IsBanditryTwoSet)
            {
                float maxHealthSpeed = 0f;
                float maxHealth = 0f;

                foreach (NPC npc in Main.npc)
                {
                    if(npc.active && npc.boss)
                    {
                        if(npc.lifeMax > maxHealth)
                        {
                            maxHealth = npc.lifeMax;
                            maxHealthSpeed = npc.velocity.Length();
                        }
                    }
                }

                if(maxHealthSpeed > 0)
                {
                    if(Player.velocity.Length() > maxHealthSpeed)
                    {
                        if (Main.rand.Next(100) < 8)
                        {
                            Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
                            return true;
                        }
                    }
                }
                else if (Main.rand.Next(100) < 3)
                {
                    Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
                    return true;
                }
            }

            if (IsThiefTwoSet && !IsThiefFourSet && Main.rand.Next(100) < 3)
            {
                Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
                return true;
            }

            if (IsThiefFourSet)
            {
                if (Main.rand.Next(100) < 8)
                {
                    Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
                    return true;
                }

                if (Main.rand.Next(100) < 50)
                {
                    Player.RestoreLife(10);
                }
            }

            if (IsWatchTwoSet && !IsWatchFourSet && Main.rand.Next(100) < 3)
            {
                Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
                return true;
            }

            if (IsWatchFourSet)
            {
                if (Main.rand.Next(100) < 8)
                {
                    Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
                    watchBoostTimer = watchBoostDuration;
                    return true;
                }
            }

            return false;
        }

        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            health = StatModifier.Default;

            if (IsLifeTwoSet)
            {
                health *= 1 + (5 / 100f);
            }

            mana = StatModifier.Default;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if(IsLifeFourSet && info.Damage >= Player.statLifeMax2 * 0.20f)
            {
                lifeBoostTimer = lifeBoostDuration;
            }

            if (IsVonwacqTwoSet)
            {
                vonwacqBoostTimer = vonwacqBoostDuration;
            }
        }
    }
}
