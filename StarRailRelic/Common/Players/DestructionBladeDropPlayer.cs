namespace StarRailRelic.Common.Players
{
    public class DestructionBladeDropPlayer : ModPlayer
    {
        public override void OnHurt(Player.HurtInfo info)
        {
            if (info.DamageSource.SourceProjectileType > -1 && info.DamageSource.SourceNPCIndex > -1)
            {
                if (info.Damage > Player.statLifeMax2 / 20 && Main.rand.NextBool(10))
                {
                    NewItemSycn(Player.GetSource_Loot(), Player.position, ItemType<ShatteredBlade>());
                }
            }
        }
    }
}
