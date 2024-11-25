namespace StarRailRelic.Common.NPCs
{
    public class SpawnNPC : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.GetModPlayer<SubworldPlayer>().InSubworld)
            {
                spawnRate = int.MaxValue;
                maxSpawns = 0;
            }
        }
    }
}
