namespace StarRailRelic.Common.Players
{
    public class TrailblazePowerPlayer : ModPlayer
    {
        public int TrailblazePower { get; set; }

        private float timer;
        private const float timeInterval = 15f * 60;

        public override void PostUpdate()
        {
            timer++;

            if (timer >= timeInterval)
            {
                TrailblazePower++;
                timer -= timeInterval;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag["TrailblazePower"] = TrailblazePower;
            tag["TrailblazePowerTimer"] = timer;
        }

        public override void LoadData(TagCompound tag)
        {
            TrailblazePower = tag.GetInt("TrailblazePower");
            timer = tag.GetFloat("TrailblazePowerTimer");
        }
    }
}
