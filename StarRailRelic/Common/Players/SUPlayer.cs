namespace StarRailRelic.Common.Players
{
    public class SUPlayer : ModPlayer
    {
        public string ChosenPath { get; set; } = "";
        public List<string> BannedPaths { get; set; } = new List<string>();
        //public List<Blessing> Blessings { get; set; } = new List<Blessing>();
        //public List<Occurrence> Occurrences { get; set; } = new List<Occurrence>();
        public int CosmicFragments { get; set; }
        //public int PathBlessingCount => Blessings.Count(b => b.Path == ChosenPath);
        public WaveType? VoteChoice { get; set; }

        public override void ResetEffects()
        {

        }

        public override void UpdateEquips()
        {
            if (GetInstance<SUEvent>().Ongoing)
            {
                Player.noBuilding = true;
                Player.AddBuff(BuffID.NoBuilding, 3);
            }
        }
    }
}
