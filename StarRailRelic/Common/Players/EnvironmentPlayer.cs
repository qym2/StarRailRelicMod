namespace StarRailRelic.Common.Players
{
    public class EnvironmentPlayer : ModPlayer
    {
        public HashSet<PlayerEnvironment> Environments = [];

        public Dictionary<RelicSet, bool> EnvironmentConditionListOut = [];
        public Dictionary<RelicSet, bool> EnvironmentConditionListIn = [];

        public RelicSet? ObtainableRelicsKeyOut { get; private set; }
        public RelicSet? ObtainableRelicsKeyIn { get; private set; }

        public bool isOutRelic;

        public override void UpdateEquips()
        {
            #region 环境检测
            UpdateEnvironment(PlayerEnvironment.Beach, Player.ZoneBeach);
            UpdateEnvironment(PlayerEnvironment.Corrupt, Player.ZoneCorrupt);
            UpdateEnvironment(PlayerEnvironment.Crimson, Player.ZoneCrimson);
            UpdateEnvironment(PlayerEnvironment.Desert, Player.ZoneDesert);
            UpdateEnvironment(PlayerEnvironment.DirtLayerHeight, Player.ZoneDirtLayerHeight);
            UpdateEnvironment(PlayerEnvironment.Dungeon, Player.ZoneDungeon);
            UpdateEnvironment(PlayerEnvironment.Forest, Player.ZoneForest);
            UpdateEnvironment(PlayerEnvironment.GemCave, Player.ZoneGemCave);
            UpdateEnvironment(PlayerEnvironment.Glowshroom, Player.ZoneGlowshroom);
            UpdateEnvironment(PlayerEnvironment.Granite, Player.ZoneGranite);
            UpdateEnvironment(PlayerEnvironment.Graveyard, Player.ZoneGraveyard);
            UpdateEnvironment(PlayerEnvironment.Hallow, Player.ZoneHallow);
            UpdateEnvironment(PlayerEnvironment.Hive, Player.ZoneHive);
            UpdateEnvironment(PlayerEnvironment.Jungle, Player.ZoneJungle);
            UpdateEnvironment(PlayerEnvironment.LihzhardTemple, Player.ZoneLihzhardTemple);
            UpdateEnvironment(PlayerEnvironment.Marble, Player.ZoneMarble);
            UpdateEnvironment(PlayerEnvironment.Meteor, Player.ZoneMeteor);
            UpdateEnvironment(PlayerEnvironment.NormalCaverns, Player.ZoneNormalCaverns);
            UpdateEnvironment(PlayerEnvironment.NormalSpace, Player.ZoneNormalSpace);
            UpdateEnvironment(PlayerEnvironment.NormalUnderground, Player.ZoneNormalUnderground);
            UpdateEnvironment(PlayerEnvironment.OldOneArmy, Player.ZoneOldOneArmy);
            UpdateEnvironment(PlayerEnvironment.OverworldHeight, Player.ZoneOverworldHeight);
            UpdateEnvironment(PlayerEnvironment.PeaceCandle, Player.ZonePeaceCandle);
            UpdateEnvironment(PlayerEnvironment.Purity, Player.ZonePurity);
            UpdateEnvironment(PlayerEnvironment.Rain, Player.ZoneRain);
            UpdateEnvironment(PlayerEnvironment.RockLayerHeight, Player.ZoneRockLayerHeight);
            UpdateEnvironment(PlayerEnvironment.Sandstorm, Player.ZoneSandstorm);
            UpdateEnvironment(PlayerEnvironment.ShadowCandle, Player.ZoneShadowCandle);
            UpdateEnvironment(PlayerEnvironment.Shimmer, Player.ZoneShimmer);
            UpdateEnvironment(PlayerEnvironment.SkyHeight, Player.ZoneSkyHeight);
            UpdateEnvironment(PlayerEnvironment.Snow, Player.ZoneSnow);
            UpdateEnvironment(PlayerEnvironment.TowerNebula, Player.ZoneTowerNebula);
            UpdateEnvironment(PlayerEnvironment.TowerSolar, Player.ZoneTowerSolar);
            UpdateEnvironment(PlayerEnvironment.TowerStardust, Player.ZoneTowerStardust);
            UpdateEnvironment(PlayerEnvironment.TowerVortex, Player.ZoneTowerVortex);
            UpdateEnvironment(PlayerEnvironment.UndergroundDesert, Player.ZoneUndergroundDesert);
            UpdateEnvironment(PlayerEnvironment.UnderworldHeight, Player.ZoneUnderworldHeight);
            UpdateEnvironment(PlayerEnvironment.WaterCandle, Player.ZoneWaterCandle);
            #endregion

            UpdateOutRelicCondition();
            UpdateInRelicCondition();
            FindFirstTrueKey();
        }

        private void UpdateOutRelicCondition()
        {
            EnvironmentConditionListOut[RelicSet.Feixiao] = Environments.Contains(PlayerEnvironment.SkyHeight);
            EnvironmentConditionListOut[RelicSet.Sacerdos] = Environments.Contains(PlayerEnvironment.Marble);
            EnvironmentConditionListOut[RelicSet.Watch] = Environments.Contains(PlayerEnvironment.Granite);
            EnvironmentConditionListOut[RelicSet.DeadWaters] = Environments.Contains(PlayerEnvironment.Graveyard);
            EnvironmentConditionListOut[RelicSet.Dot] = Environments.Contains(PlayerEnvironment.Dungeon);
            EnvironmentConditionListOut[RelicSet.Quantum] = Environments.Contains(PlayerEnvironment.Glowshroom);
            EnvironmentConditionListOut[RelicSet.Guard] = Environments.Contains(PlayerEnvironment.Snow) && !Main.dayTime;
            EnvironmentConditionListOut[RelicSet.Ice] = Environments.Contains(PlayerEnvironment.Snow);
            EnvironmentConditionListOut[RelicSet.Scholar] = Environments.Contains(PlayerEnvironment.Beach) && !Main.dayTime;
            EnvironmentConditionListOut[RelicSet.Wind] = Environments.Contains(PlayerEnvironment.Beach);
            EnvironmentConditionListOut[RelicSet.Imaginary] = Environments.Contains(PlayerEnvironment.Desert);
            EnvironmentConditionListOut[RelicSet.Messenger] = Environments.Contains(PlayerEnvironment.Hive);
            EnvironmentConditionListOut[RelicSet.Life] = Environments.Contains(PlayerEnvironment.Jungle);
            EnvironmentConditionListOut[RelicSet.Iron] = Environments.Contains(PlayerEnvironment.Crimson) && !Main.dayTime;
            EnvironmentConditionListOut[RelicSet.Healing] = Environments.Contains(PlayerEnvironment.Crimson);
            EnvironmentConditionListOut[RelicSet.Knight] = Environments.Contains(PlayerEnvironment.Corrupt);
            EnvironmentConditionListOut[RelicSet.Lightning] = Environments.Contains(PlayerEnvironment.DirtLayerHeight);
            EnvironmentConditionListOut[RelicSet.Duke] = Environments.Contains(PlayerEnvironment.UnderworldHeight) && !Main.dayTime;
            EnvironmentConditionListOut[RelicSet.Fire] = Environments.Contains(PlayerEnvironment.UnderworldHeight);
            EnvironmentConditionListOut[RelicSet.Physics] = Environments.Contains(PlayerEnvironment.RockLayerHeight);
            EnvironmentConditionListOut[RelicSet.Thief] = Environments.Contains(PlayerEnvironment.Forest) && !Main.dayTime;
            EnvironmentConditionListOut[RelicSet.Musketeer] = Environments.Contains(PlayerEnvironment.Forest);
        }

        private void UpdateInRelicCondition()
        {
            EnvironmentConditionListIn[RelicSet.Salsotto] = Environments.Contains(PlayerEnvironment.Meteor);
            EnvironmentConditionListIn[RelicSet.Differentiator] = Environments.Contains(PlayerEnvironment.SkyHeight) && !Main.dayTime;
            EnvironmentConditionListIn[RelicSet.Space] = Environments.Contains(PlayerEnvironment.SkyHeight);
            EnvironmentConditionListIn[RelicSet.Enterprise] = Environments.Contains(PlayerEnvironment.Marble);
            EnvironmentConditionListIn[RelicSet.Izumo] = Environments.Contains(PlayerEnvironment.Graveyard);
            EnvironmentConditionListIn[RelicSet.Sigonia] = Environments.Contains(PlayerEnvironment.Dungeon);
            EnvironmentConditionListIn[RelicSet.Penacony] = Environments.Contains(PlayerEnvironment.Glowshroom);
            EnvironmentConditionListIn[RelicSet.Belobog] = Environments.Contains(PlayerEnvironment.Snow);
            EnvironmentConditionListIn[RelicSet.Sunken] = Environments.Contains(PlayerEnvironment.Beach);
            EnvironmentConditionListIn[RelicSet.Wolves] = Environments.Contains(PlayerEnvironment.Desert);
            EnvironmentConditionListIn[RelicSet.Banana] = Environments.Contains(PlayerEnvironment.Hive);
            EnvironmentConditionListIn[RelicSet.Keel] = Environments.Contains(PlayerEnvironment.Jungle);
            EnvironmentConditionListIn[RelicSet.Glamoth] = Environments.Contains(PlayerEnvironment.Crimson);
            EnvironmentConditionListIn[RelicSet.Vonwacq] = Environments.Contains(PlayerEnvironment.Corrupt);
            EnvironmentConditionListIn[RelicSet.Kalpagni] = Environments.Contains(PlayerEnvironment.UnderworldHeight);
            EnvironmentConditionListIn[RelicSet.Rutilant] = Environments.Contains(PlayerEnvironment.RockLayerHeight);
            EnvironmentConditionListIn[RelicSet.Banditry] = Environments.Contains(PlayerEnvironment.Forest) && !Main.dayTime;
            EnvironmentConditionListIn[RelicSet.Xianzhou] = Environments.Contains(PlayerEnvironment.Forest);
        }

        private void FindFirstTrueKey()
        {
            if (!Player.GetModPlayer<SubworldPlayer>().InSubworld)
            {
                ObtainableRelicsKeyOut = null;
                foreach (KeyValuePair<RelicSet, bool> kvp in EnvironmentConditionListOut)
                {
                    if (kvp.Value)
                    {
                        ObtainableRelicsKeyOut = kvp.Key;
                        break;
                    }
                }

                ObtainableRelicsKeyIn = null;
                foreach (KeyValuePair<RelicSet, bool> kvp in EnvironmentConditionListIn)
                {
                    if (kvp.Value)
                    {
                        ObtainableRelicsKeyIn = kvp.Key;
                        break;
                    }
                }
            }
        }

        private void UpdateEnvironment(PlayerEnvironment environment, bool condition)
        {
            if (condition)
            {
                Environments?.Add(environment);
            }
            else
            {
                Environments?.Remove(environment);
            }
        }
    }
}
