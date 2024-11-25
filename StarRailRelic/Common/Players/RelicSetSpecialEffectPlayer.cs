using HarmonyLib;

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
        private const int messengerBoostDuration = 600;

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
        }

        public override void UpdateLifeRegen()
        {
            if (IsHealingFourSet)
            {
                Player.lifeRegen += 8;
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
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (IsQuantumFourSet && (modifiers.DamageType == DamageClass.Melee || modifiers.DamageType == DamageClass.MeleeNoSpeed))
            {
                modifiers.ArmorPenetration += 4;
            }

            if (IsImaginaryFourSet)
            {
                float distance = Vector2.Distance(Player.position, target.position);

                float distanceThreshold = 600;

                if (distance <= distanceThreshold)
                {
                    float critChance = Player.GetCritChance<GenericDamageClass>() + 8;
                    if(Main.rand.NextFloat() * 100 < critChance)
                    {
                        modifiers.SetCrit();
                    }
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
                    modifiers.SourceDamage *= 2.8f;
                    lightningNoDamageDone = false;
                }
            }

            if (IsDeadWatersTwoSet)
            {
                if(target.DebuffType().Length != 0)
                {
                    modifiers.SourceDamage *= 1.06f;
                }
            }

            if (IsDeadWatersFourSet)
            {
                if (target.DebuffType().Length >= 3)
                {
                    modifiers.CritDamage += 9 / 100f;
                }
                else if (target.DebuffType().Length >= 2)
                {
                    modifiers.CritDamage += 6 / 100f;
                }
            }

            if (IsDotFourSet)
            {
                if (target.lifeRegen < 0)
                {
                    modifiers.Defense.Flat -= 6;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if ((hit.DamageType == DamageClass.SummonMeleeSpeed || hit.DamageType == DamageClass.Summon) && IsIceFourSet)
            {
                iceBoostTimer = iceBoostDuration;
            }

            if(physicsLastDamageType != null)
            {
                if(hit.DamageType == DamageClass.SummonMeleeSpeed && physicsLastDamageType == DamageClass.Summon)
                {
                    if (physicsBoostStacks < physicsBoostMaxStacks)
                    {
                        physicsBoostStacks++;
                    }
                    physicsBoostTimer = physicsBoostDuration;
                }
                if(hit.DamageType == DamageClass.SummonMeleeSpeed && physicsLastDamageType == DamageClass.SummonMeleeSpeed)
                {
                    physicsBoostStacks = 0;
                    physicsBoostTimer = 0;
                }
            }
            physicsLastDamageType = hit.DamageType;
        }

        public override void OnConsumeAmmo(Item weapon, Item ammo)
        {
            if (IsWindFourSet)
            {
                windConsumedAmmo++;
            }
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
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

                if(Main.rand.Next(100) < 50)
                {
                    Player.RestoreLife(10);
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
        }
    }
}
