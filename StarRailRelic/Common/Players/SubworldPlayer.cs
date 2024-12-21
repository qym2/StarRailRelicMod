namespace StarRailRelic.Common.Players
{
    public class SubworldPlayer : ModPlayer
    {
        public bool InSubworld { get; set; } = false;

        public Vector2 oldPosition;
        public bool downedAllNPC;
        public readonly List<NPC> summonedNPCs = [];

        private float exitCheckTimer;

        public static Dictionary<RelicSet, SubworldInfor> NPCToSummonLists => new()
        {
            {RelicSet.Thief, new SubworldInfor([
                new(NPCID.DemonEye, new Vector2(240, 240)),
                new(NPCID.DemonEye2, new Vector2(240, -240)),
                new(NPCID.CataractEye, new Vector2(-240, -240)),
                new(NPCID.CataractEye2, new Vector2(-240, 240)),
                new(NPCID.GreenEye, new Vector2(320, 320)),
                new(NPCID.GreenEye2, new Vector2(320, -320)),
                new(NPCID.PurpleEye, new Vector2(-320, -320)),
                new(NPCID.PurpleEye2, new Vector2(-320, 320)),
                new(NPCID.ArmedZombie, new Vector2(320, 0)),
                new(NPCID.SlimedZombie, new Vector2(240, 0)),
                new(NPCID.PincushionZombie, new Vector2(-240, 0)),
                new(NPCID.ArmedTorchZombie, new Vector2(-320, 0)),], "NormalRuin", new Point16(-65, -33), false, 8000)
            },
            {RelicSet.Musketeer, new SubworldInfor([
                new(NPCID.FlyingFish, new Vector2(240, 240)),
                new(NPCID.FlyingFish, new Vector2(240, -240)),
                new(NPCID.FlyingFish, new Vector2(-240, -240)),
                new(NPCID.FlyingFish, new Vector2(-240, 240)),
                new(NPCID.FlyingFish, new Vector2(320, 320)),
                new(NPCID.FlyingFish, new Vector2(320, -320)),
                new(NPCID.FlyingFish, new Vector2(-320, -320)),
                new(NPCID.FlyingFish, new Vector2(-320, 320)),
                new(NPCID.BlackSlime, new Vector2(320, 0)),
                new(NPCID.BlackSlime, new Vector2(240, 0)),
                new(NPCID.BlackSlime, new Vector2(-240, 0)),
                new(NPCID.BlackSlime, new Vector2(-320, 0)),], "NormalRuin", new Point16(-65, -33), true, 8000)
            },
            {RelicSet.Feixiao, new SubworldInfor([
                new(NPCID.Harpy, new Vector2(240, 240)),
                new(NPCID.Harpy, new Vector2(240, -240)),
                new(NPCID.Harpy, new Vector2(-240, -240)),
                new(NPCID.Harpy, new Vector2(-240, 240)),
                new(NPCID.Harpy, new Vector2(320, 320)),
                new(NPCID.Harpy, new Vector2(320, -320)),
                new(NPCID.Harpy, new Vector2(-320, -320)),
                new(NPCID.Harpy, new Vector2(-320, 320)),], "SkyRuin", new Point16(-65, -33), true, 15000)
            },
            {RelicSet.Sacerdos, new SubworldInfor([
                new(NPCID.GreekSkeleton, new Vector2(240, 240)),
                new(NPCID.GreekSkeleton, new Vector2(240, -240)),
                new(NPCID.GreekSkeleton, new Vector2(-240, -240)),
                new(NPCID.GreekSkeleton, new Vector2(-240, 240)),
                new(NPCID.GreekSkeleton, new Vector2(320, 320)),
                new(NPCID.GreekSkeleton, new Vector2(320, -320)),
                new(NPCID.GreekSkeleton, new Vector2(-320, -320)),
                new(NPCID.GreekSkeleton, new Vector2(-320, 320)),
                new(NPCID.GreekSkeleton, new Vector2(320, 0)),
                new(NPCID.GreekSkeleton, new Vector2(240, 0)),
                new(NPCID.GreekSkeleton, new Vector2(-240, 0)),
                new(NPCID.GreekSkeleton, new Vector2(-320, 0)),], "MarbleRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Watch, new SubworldInfor([
                new(NPCID.GraniteFlyer, new Vector2(240, 240)),
                new(NPCID.GraniteFlyer, new Vector2(240, -240)),
                new(NPCID.GraniteFlyer, new Vector2(-240, -240)),
                new(NPCID.GraniteFlyer, new Vector2(-240, 240)),
                new(NPCID.GraniteFlyer, new Vector2(320, 320)),
                new(NPCID.GraniteFlyer, new Vector2(320, -320)),
                new(NPCID.GraniteFlyer, new Vector2(-320, -320)),
                new(NPCID.GraniteFlyer, new Vector2(-320, 320)),
                new(NPCID.GraniteGolem, new Vector2(320, 0)),
                new(NPCID.GraniteGolem, new Vector2(240, 0)),
                new(NPCID.GraniteGolem, new Vector2(-240, 0)),
                new(NPCID.GraniteGolem, new Vector2(-320, 0)),], "GraniteRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.DeadWaters, new SubworldInfor([
                new(NPCID.Raven, new Vector2(240, 240)),
                new(NPCID.Raven, new Vector2(240, -240)),
                new(NPCID.Raven, new Vector2(-240, -240)),
                new(NPCID.Raven, new Vector2(-240, 240)),
                new(NPCID.Ghost, new Vector2(320, 320)),
                new(NPCID.Raven, new Vector2(320, -320)),
                new(NPCID.Ghost, new Vector2(-320, -320)),
                new(NPCID.Raven, new Vector2(-320, 320)),
                new(NPCID.Ghost, new Vector2(320, 0)),
                new(NPCID.Ghost, new Vector2(240, 0)),
                new(NPCID.Ghost, new Vector2(-240, 0)),
                new(NPCID.Ghost, new Vector2(-320, 0)),], "GraveyardRuin", new Point16(-65, -33), true, 8000)
            },
            {RelicSet.Dot, new SubworldInfor([
                new(NPCID.DarkCaster, new Vector2(240, 240)),
                new(NPCID.DarkCaster, new Vector2(240, -240)),
                new(NPCID.DarkCaster, new Vector2(-240, -240)),
                new(NPCID.DarkCaster, new Vector2(-240, 240)),
                new(NPCID.CursedSkull, new Vector2(320, 320)),
                new(NPCID.CursedSkull, new Vector2(320, -320)),
                new(NPCID.CursedSkull, new Vector2(-320, -320)),
                new(NPCID.CursedSkull, new Vector2(-320, 320)),
                new(NPCID.AngryBonesBigHelmet, new Vector2(320, 0)),
                new(NPCID.AngryBonesBigHelmet, new Vector2(240, 0)),
                new(NPCID.AngryBonesBigHelmet, new Vector2(-240, 0)),
                new(NPCID.AngryBonesBigHelmet, new Vector2(-320, 0)),], "DungeonRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Quantum, new SubworldInfor([
                new(NPCID.SporeBat, new Vector2(240, 240)),
                new(NPCID.SporeBat, new Vector2(240, -240)),
                new(NPCID.SporeBat, new Vector2(-240, -240)),
                new(NPCID.SporeBat, new Vector2(-240, 240)),
                new(NPCID.AnomuraFungus, new Vector2(240, 320)),
                new(NPCID.AnomuraFungus, new Vector2(320, -320)),
                new(NPCID.AnomuraFungus, new Vector2(-320, -320)),
                new(NPCID.AnomuraFungus, new Vector2(-320, 320)),
                new(NPCID.SporeSkeleton, new Vector2(320, 0)),
                new(NPCID.SporeSkeleton, new Vector2(240, 0)),
                new(NPCID.SporeSkeleton, new Vector2(-240, 0)),
                new(NPCID.SporeSkeleton, new Vector2(-320, 0)),], "GlowshroomRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Guard, new SubworldInfor([
                new(NPCID.SpikedIceSlime, new Vector2(240, 240)),
                new(NPCID.SpikedIceSlime, new Vector2(240, -240)),
                new(NPCID.SpikedIceSlime, new Vector2(-240, -240)),
                new(NPCID.SpikedIceSlime, new Vector2(-240, 240)),
                new(NPCID.IceBat, new Vector2(320, 320)),
                new(NPCID.IceBat, new Vector2(320, -320)),
                new(NPCID.IceBat, new Vector2(-320, -320)),
                new(NPCID.IceBat, new Vector2(-320, 320)),
                new(NPCID.UndeadViking, new Vector2(320, 0)),
                new(NPCID.UndeadViking, new Vector2(240, 0)),
                new(NPCID.UndeadViking, new Vector2(-240, 0)),
                new(NPCID.UndeadViking, new Vector2(-320, 0)),], "SnowRuin", new Point16(-65, -33), false, -8000)
            },
            {RelicSet.Ice, new SubworldInfor([
                new(NPCID.IceBat, new Vector2(240, 240)),
                new(NPCID.IceBat, new Vector2(240, -240)),
                new(NPCID.IceBat, new Vector2(-240, -240)),
                new(NPCID.IceBat, new Vector2(-240, 240)),
                new(NPCID.SpikedIceSlime, new Vector2(320, 320)),
                new(NPCID.SpikedIceSlime, new Vector2(320, -320)),
                new(NPCID.SpikedIceSlime, new Vector2(-320, -320)),
                new(NPCID.SpikedIceSlime, new Vector2(-320, 320)),
                new(NPCID.SnowFlinx, new Vector2(320, 0)),
                new(NPCID.SnowFlinx, new Vector2(240, 0)),
                new(NPCID.SnowFlinx, new Vector2(-240, 0)),
                new(NPCID.SnowFlinx, new Vector2(-320, 0)),], "SnowRuin", new Point16(-65, -33), true, 8000)
            },
            {RelicSet.Scholar, new SubworldInfor([
                new(NPCID.PinkJellyfish, new Vector2(-320, -240)),
                new(NPCID.PinkJellyfish, new Vector2(320, -240)),
                new(NPCID.PinkJellyfish, new Vector2(-240, -240)),
                new(NPCID.PinkJellyfish, new Vector2(240, -240)),
                new(NPCID.Shark, new Vector2(-160, -240)),
                new(NPCID.Shark, new Vector2(240, -240)),
                new(NPCID.Shark, new Vector2(-240, -240)),
                new(NPCID.Shark, new Vector2(160, -240)),
                new(NPCID.PinkJellyfish, new Vector2(-160, -120)),
                new(NPCID.PinkJellyfish, new Vector2(240, -120)),
                new(NPCID.PinkJellyfish, new Vector2(-240, -120)),
                new(NPCID.PinkJellyfish, new Vector2(160, -120)),], "BeachRuin", new Point16(-65, -33), false, 8000, 35000)
            },
            {RelicSet.Wind, new SubworldInfor([
                new(NPCID.Shark, new Vector2(-320, -240)),
                new(NPCID.Shark, new Vector2(320, -240)),
                new(NPCID.Shark, new Vector2(-240, -240)),
                new(NPCID.Shark, new Vector2(240, -240)),
                new(NPCID.Shark, new Vector2(240, -120)),
                new(NPCID.Shark, new Vector2(-240, -120)),], "BeachRuin", new Point16(-65, -33), true, 8000, 35000)
            },
            {RelicSet.Imaginary, new SubworldInfor([
                new(NPCID.Vulture, new Vector2(240, 240)),
                new(NPCID.Vulture, new Vector2(240, -240)),
                new(NPCID.Vulture, new Vector2(-240, -240)),
                new(NPCID.Vulture, new Vector2(-240, 240)),
                new(NPCID.Vulture, new Vector2(320, 320)),
                new(NPCID.Vulture, new Vector2(320, -320)),
                new(NPCID.Vulture, new Vector2(-320, -320)),
                new(NPCID.Vulture, new Vector2(-320, 320)),
                new(NPCID.Antlion, new Vector2(320, 0)),
                new(NPCID.Antlion, new Vector2(240, 0)),
                new(NPCID.Antlion, new Vector2(-240, 0)),
                new(NPCID.Antlion, new Vector2(-320, 0)),], "DesertRuin", new Point16(-65, -33), true, 8000)
            },
            {RelicSet.Messenger, new SubworldInfor([
                new(NPCID.BigHornetFatty, new Vector2(240, 240)),
                new(NPCID.BigHornetFatty, new Vector2(240, -240)),
                new(NPCID.BigHornetFatty, new Vector2(-240, -240)),
                new(NPCID.BigHornetFatty, new Vector2(-240, 240)),
                new(NPCID.BigHornetStingy, new Vector2(320, 320)),
                new(NPCID.BigHornetStingy, new Vector2(320, -320)),
                new(NPCID.BigHornetStingy, new Vector2(-320, -320)),
                new(NPCID.BigHornetStingy, new Vector2(-320, 320)),
                new(NPCID.BigHornetFatty, new Vector2(320, 0)),
                new(NPCID.BigHornetFatty, new Vector2(240, 0)),
                new(NPCID.BigHornetFatty, new Vector2(-240, 0)),
                new(NPCID.BigHornetFatty, new Vector2(-320, 0)),], "HiveRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Life, new SubworldInfor([
                new(NPCID.JungleBat, new Vector2(240, 240)),
                new(NPCID.JungleBat, new Vector2(240, 0)),
                new(NPCID.JungleBat, new Vector2(-240, -240)),
                new(NPCID.JungleBat, new Vector2(-240, 300)),
                new(NPCID.JungleBat, new Vector2(120, 120)),
                new(NPCID.JungleBat, new Vector2(120, 0)),
                new(NPCID.JungleBat, new Vector2(-120, -120)),
                new(NPCID.JungleBat, new Vector2(-120, 240)),
                new(NPCID.JungleSlime, new Vector2(120, 0)),
                new(NPCID.JungleSlime, new Vector2(240, 0)),
                new(NPCID.JungleSlime, new Vector2(-240, -120)),
                new(NPCID.JungleSlime, new Vector2(-120, -120)),], "JungleRuin2", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Iron, new SubworldInfor([
                new(NPCID.BigCrimera, new Vector2(320, 320)),
                new(NPCID.BigCrimera, new Vector2(320, -320)),
                new(NPCID.BigCrimera, new Vector2(-320, -320)),
                new(NPCID.BigCrimera, new Vector2(-320, 320)),
                new(NPCID.FaceMonster, new Vector2(320, 0)),
                new(NPCID.FaceMonster, new Vector2(240, 0)),
                new(NPCID.FaceMonster, new Vector2(-240, 0)),
                new(NPCID.FaceMonster, new Vector2(-320, 0)),], "CrimsonRuin", new Point16(-65, -33), false, 8000)
            },
            {RelicSet.Healing, new SubworldInfor([
                new(NPCID.BigCrimera, new Vector2(320, 320)),
                new(NPCID.BigCrimera, new Vector2(320, -320)),
                new(NPCID.BigCrimera, new Vector2(-320, -320)),
                new(NPCID.BigCrimera, new Vector2(-320, 320)),
                new(NPCID.BloodCrawlerWall, new Vector2(320, 0)),
                new(NPCID.BloodCrawlerWall, new Vector2(240, 0)),
                new(NPCID.BloodCrawlerWall, new Vector2(-240, 0)),
                new(NPCID.BloodCrawlerWall, new Vector2(-320, 0)),], "CrimsonRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Knight, new SubworldInfor([
                new(NPCID.BigEater, new Vector2(240, 240)),
                new(NPCID.BigEater, new Vector2(240, -240)),
                new(NPCID.BigEater, new Vector2(-240, -240)),
                new(NPCID.BigEater, new Vector2(-240, 240)),
                new(NPCID.BigEater, new Vector2(320, 320)),
                new(NPCID.BigEater, new Vector2(320, -320)),
                new(NPCID.BigEater, new Vector2(-320, -320)),
                new(NPCID.BigEater, new Vector2(-320, 320))], "CorruptedRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Physics, new SubworldInfor([
                new(NPCID.CaveBat, new Vector2(240, 240)),
                new(NPCID.CaveBat, new Vector2(240, -240)),
                new(NPCID.CaveBat, new Vector2(-240, -240)),
                new(NPCID.CaveBat, new Vector2(-240, 240)),
                new(NPCID.Tim, new Vector2(320, 320)),
                new(NPCID.Tim, new Vector2(-320, -320)),
                new(NPCID.UndeadMiner, new Vector2(320, 0)),
                new(NPCID.BigPantlessSkeleton, new Vector2(240, 0)),
                new(NPCID.BigPantlessSkeleton, new Vector2(-240, 0)),
                new(NPCID.UndeadMiner, new Vector2(-320, 0)),], "RockRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Duke, new SubworldInfor([
                new(NPCID.Demon, new Vector2(240, 240)),
                new(NPCID.Demon, new Vector2(240, -240)),
                new(NPCID.Demon, new Vector2(-240, -240)),
                new(NPCID.Demon, new Vector2(-240, 240)),
                new(NPCID.LavaSlime, new Vector2(320, 0)),
                new(NPCID.LavaSlime, new Vector2(240, 0)),
                new(NPCID.LavaSlime, new Vector2(-240, 0)),
                new(NPCID.LavaSlime, new Vector2(-320, 0)),], "UnderworldRuin", new Point16(-65, -33), false, -14000)
            },
            {RelicSet.Fire, new SubworldInfor([
                new(NPCID.FireImp, new Vector2(240, 240)),
                new(NPCID.FireImp, new Vector2(240, -240)),
                new(NPCID.FireImp, new Vector2(-240, -240)),
                new(NPCID.FireImp, new Vector2(-240, 240)),
                new(NPCID.Hellbat, new Vector2(320, 0)),
                new(NPCID.Hellbat, new Vector2(240, 0)),
                new(NPCID.Hellbat, new Vector2(-240, 0)),
                new(NPCID.Hellbat, new Vector2(-320, 0)),], "UnderworldRuin", new Point16(-65, -33), true, -14000)
            },
            {RelicSet.Lightning, new SubworldInfor([
                new(NPCID.BlueSlime, new Vector2(320, 320)),
                new(NPCID.BlueSlime, new Vector2(320, 320)),
                new(NPCID.RedSlime, new Vector2(-320, 320)),
                new(NPCID.RedSlime, new Vector2(-320, 320)),
                new(NPCID.BlueSlime, new Vector2(240, 240)),
                new(NPCID.BlueSlime, new Vector2(240, -240)),
                new(NPCID.RedSlime, new Vector2(-240, 240)),
                new(NPCID.RedSlime, new Vector2(-240, 240)),
                new(NPCID.YellowSlime, new Vector2(320, 0)),
                new(NPCID.YellowSlime, new Vector2(240, 0)),
                new(NPCID.YellowSlime, new Vector2(320, 0)),
                new(NPCID.YellowSlime, new Vector2(240, 0)),], "DirtRuin2", new Point16(-65, -33), false, -2000)
            },

            {RelicSet.Banditry, new SubworldInfor([
                new(NPCID.WanderingEye, new Vector2(320, 320)),
                new(NPCID.WanderingEye, new Vector2(320, -320)),
                new(NPCID.WanderingEye, new Vector2(-320, -320)),
                new(NPCID.WanderingEye, new Vector2(-320, 320)),
                new(NPCID.PossessedArmor, new Vector2(320, 0)),
                new(NPCID.PossessedArmor, new Vector2(240, 0)),
                new(NPCID.PossessedArmor, new Vector2(-240, 0)),
                new(NPCID.PossessedArmor, new Vector2(-320, 0)),], "NormalRuin", new Point16(-65, -33), false, 8000)
            },
            {RelicSet.Xianzhou, new SubworldInfor([
                new(NPCID.AngryNimbus, new Vector2(240, 240)),
                new(NPCID.AngryNimbus, new Vector2(240, -240)),
                new(NPCID.AngryNimbus, new Vector2(-240, -240)),
                new(NPCID.AngryNimbus, new Vector2(-240, 240)),
                new(NPCID.AngryNimbus, new Vector2(320, 320)),
                new(NPCID.AngryNimbus, new Vector2(320, -320)),
                new(NPCID.AngryNimbus, new Vector2(-320, -320)),
                new(NPCID.AngryNimbus, new Vector2(-320, 320)),], "NormalRuin", new Point16(-65, -33), true, 8000)
            },
            {RelicSet.Salsotto, new SubworldInfor([
                new(NPCID.Lavabat, new Vector2(240, 240)),
                new(NPCID.Lavabat, new Vector2(240, -240)),
                new(NPCID.Lavabat, new Vector2(-240, -240)),
                new(NPCID.Lavabat, new Vector2(-240, 240)),
                new(NPCID.Lavabat, new Vector2(280, 280)),
                new(NPCID.Lavabat, new Vector2(280, -280)),
                new(NPCID.Lavabat, new Vector2(-280, -280)),
                new(NPCID.Lavabat, new Vector2(-280, 280)),
                new(NPCID.Lavabat, new Vector2(320, 320)),
                new(NPCID.Lavabat, new Vector2(320, -320)),
                new(NPCID.Lavabat, new Vector2(-320, -320)),
                new(NPCID.Lavabat, new Vector2(-320, 320)),], "UnderworldRuin", new Point16(-65, -33), false, -14000)
            },
            {RelicSet.Differentiator, new SubworldInfor([
                new(NPCID.WyvernHead, new Vector2(240, -240)),
                new(NPCID.WyvernHead, new Vector2(-240, -240)),], "SkyRuin", new Point16(-65, -33), false, 15000)
            },
            {RelicSet.Space, new SubworldInfor([
                new(NPCID.WyvernHead, new Vector2(240, -240)),
                new(NPCID.WyvernHead, new Vector2(-240, -240)),], "SkyRuin", new Point16(-65, -33), true, 15000)
            },
            {RelicSet.Enterprise, new SubworldInfor([
                new(NPCID.Medusa, new Vector2(240, 240)),
                new(NPCID.Medusa, new Vector2(240, -240)),
                new(NPCID.Medusa, new Vector2(-240, -240)),
                new(NPCID.Medusa, new Vector2(-240, 240)),
                new(NPCID.Medusa, new Vector2(320, 0)),
                new(NPCID.Medusa, new Vector2(-320, 0)),], "MarbleRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Izumo, new SubworldInfor([
                new(NPCID.HoppinJack, new Vector2(240, 240)),
                new(NPCID.HoppinJack, new Vector2(240, -240)),
                new(NPCID.HoppinJack, new Vector2(-240, -240)),
                new(NPCID.HoppinJack, new Vector2(-240, 240)),
                new(NPCID.HoppinJack, new Vector2(320, 320)),
                new(NPCID.HoppinJack, new Vector2(320, -320)),
                new(NPCID.HoppinJack, new Vector2(-320, -320)),
                new(NPCID.HoppinJack, new Vector2(-320, 320)),
                new(NPCID.HoppinJack, new Vector2(320, 0)),
                new(NPCID.HoppinJack, new Vector2(240, 0)),
                new(NPCID.HoppinJack, new Vector2(-240, 0)),
                new(NPCID.HoppinJack, new Vector2(-320, 0)),], "GraveyardRuin", new Point16(-65, -33), true, 8000)
            },
            {RelicSet.Sigonia, new SubworldInfor([
                new(NPCID.SkeletonCommando, new Vector2(240, 240)),
                new(NPCID.Paladin, new Vector2(-240, -240)),
                new(NPCID.GiantCursedSkull, new Vector2(320, 320)),
                new(NPCID.SkeletonSniper, new Vector2(320, -320)),
                new(NPCID.HellArmoredBonesSword, new Vector2(320, 0)),
                new(NPCID.BoneLee, new Vector2(240, 0)),
                new(NPCID.HellArmoredBonesSword, new Vector2(-320, 0)),], "DungeonRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Penacony, new SubworldInfor([
                new(NPCID.MushiLadybug, new Vector2(240, 240)),
                new(NPCID.MushiLadybug, new Vector2(240, -240)),
                new(NPCID.MushiLadybug, new Vector2(-240, -240)),
                new(NPCID.MushiLadybug, new Vector2(-240, 240)),
                new(NPCID.MushiLadybug, new Vector2(240, 320)),
                new(NPCID.MushiLadybug, new Vector2(320, -320)),
                new(NPCID.MushiLadybug, new Vector2(-320, -320)),
                new(NPCID.MushiLadybug, new Vector2(-320, 320)),], "GlowshroomRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Belobog, new SubworldInfor([
                new(NPCID.IceElemental, new Vector2(240, 240)),
                new(NPCID.IceElemental, new Vector2(240, -240)),
                new(NPCID.IceElemental, new Vector2(-240, -240)),
                new(NPCID.IceElemental, new Vector2(-240, 240)),
                new(NPCID.IceElemental, new Vector2(320, 320)),
                new(NPCID.IceElemental, new Vector2(320, -320)),
                new(NPCID.IceElemental, new Vector2(-320, -320)),
                new(NPCID.IceElemental, new Vector2(-320, 320)),
                new(NPCID.Wolf, new Vector2(320, 0)),
                new(NPCID.Wolf, new Vector2(240, 0)),
                new(NPCID.Wolf, new Vector2(-240, 0)),
                new(NPCID.Wolf, new Vector2(-320, 0)),], "SnowRuin", new Point16(-65, -33), false, 8000)
            },
            {RelicSet.Sunken, new SubworldInfor([
                new(NPCID.PigronHallow, new Vector2(-320, -240)),
                new(NPCID.PigronHallow, new Vector2(320, -240)),
                new(NPCID.PigronHallow, new Vector2(-240, -240)),
                new(NPCID.PigronHallow, new Vector2(240, -240)),
                new(NPCID.PigronHallow, new Vector2(-160, -240)),
                new(NPCID.PigronHallow, new Vector2(240, -240)),
                new(NPCID.PigronHallow, new Vector2(-240, -240)),
                new(NPCID.PigronHallow, new Vector2(160, -240)),], "BeachRuin", new Point16(-65, -33), false, 8000, 35000)
            },
            {RelicSet.Wolves, new SubworldInfor([
                new(NPCID.DarkMummy, new Vector2(240, 240)),
                new(NPCID.LightMummy, new Vector2(240, -240)),
                new(NPCID.LightMummy, new Vector2(-240, -240)),
                new(NPCID.BloodMummy, new Vector2(-240, 240)),
                new(NPCID.BloodMummy, new Vector2(320, 320)),
                new(NPCID.LightMummy, new Vector2(320, -320)),
                new(NPCID.LightMummy, new Vector2(-320, -320)),
                new(NPCID.DarkMummy, new Vector2(-320, 320)),
                new(NPCID.Mummy, new Vector2(320, 0)),
                new(NPCID.Mummy, new Vector2(240, 0)),
                new(NPCID.Mummy, new Vector2(-240, 0)),
                new(NPCID.Mummy, new Vector2(-320, 0)),], "DesertRuin", new Point16(-65, -33), true, 8000)
            },
            {RelicSet.Banana, new SubworldInfor([
                new(NPCID.GiantMossHornet, new Vector2(240, -240)),
                new(NPCID.GiantMossHornet, new Vector2(-240, 240)),
                new(NPCID.GiantMossHornet, new Vector2(320, 320)),
                new(NPCID.GiantMossHornet, new Vector2(320, -320)),
                new(NPCID.GiantMossHornet, new Vector2(-320, -320)),
                new(NPCID.GiantMossHornet, new Vector2(-320, 320)),], "HiveRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Keel, new SubworldInfor([
                new(NPCID.Derpling, new Vector2(240, 240)),
                new(NPCID.Derpling, new Vector2(240, 0)),
                new(NPCID.Derpling, new Vector2(-240, -240)),
                new(NPCID.Derpling, new Vector2(-240, 300)),
                new(NPCID.GiantTortoise, new Vector2(120, 0)),
                new(NPCID.GiantTortoise, new Vector2(-120, -120)),], "JungleRuin2", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Glamoth, new SubworldInfor([
                new(NPCID.IchorSticker, new Vector2(320, 320)),
                new(NPCID.IchorSticker, new Vector2(-320, -320)),
                new(NPCID.CrimsonAxe, new Vector2(240, -240)),
                new(NPCID.CrimsonAxe, new Vector2(-240, 240)),
                new(NPCID.Herpling, new Vector2(320, 0)),
                new(NPCID.Herpling, new Vector2(-320, 0)),], "CrimsonRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Vonwacq, new SubworldInfor([
                new(NPCID.CursedHammer, new Vector2(320, -320)),
                new(NPCID.CursedHammer, new Vector2(-320, 320)),
                new(NPCID.CursedHammer, new Vector2(240, -240)),
                new(NPCID.CursedHammer, new Vector2(-240, 240)),
                new(NPCID.Corruptor, new Vector2(320, 0)),
                new(NPCID.Corruptor, new Vector2(240, 0)),
                new(NPCID.Corruptor, new Vector2(-240, 0)),
                new(NPCID.Corruptor, new Vector2(-320, 0)),], "CorruptedRuin", new Point16(-65, -33), true, -8000)
            },
            {RelicSet.Kalpagni, new SubworldInfor([
                new(NPCID.RedDevil, new Vector2(240, 240)),
                new(NPCID.RedDevil, new Vector2(240, -240)),
                new(NPCID.RedDevil, new Vector2(-240, -240)),
                new(NPCID.RedDevil, new Vector2(-240, 240)),], "UnderworldRuin", new Point16(-65, -33), true, -14000)
            },
            {RelicSet.Rutilant, new SubworldInfor([
                new(NPCID.SkeletonArcher, new Vector2(-240, -240)),
                new(NPCID.SkeletonArcher, new Vector2(-240, 240)),
                new(NPCID.RuneWizard, new Vector2(320, 320)),
                new(NPCID.RuneWizard, new Vector2(320, -320)),
                new(NPCID.RockGolem, new Vector2(240, 0)),
                new(NPCID.RockGolem, new Vector2(-240, 0)),], "RockRuin", new Point16(-65, -33), true, -8000)
            },
        };

        public override void UpdateEquips()
        {
            if (InSubworld)
            {
                Player.noBuilding = true;
                Player.AddBuff(BuffID.NoBuilding, 3);
            }
        }

        public override void PostUpdate()
        {
            UpdateSubworld();
        }

        public override void OnEnterWorld()
        {
            if (InSubworld)
            {
                InitializeWorld();
            }

            if (!InSubworld)
            {
                if (downedAllNPC)
                {
                    int[] relicTypes = Player.GetModPlayer<EnvironmentPlayer>().isOutRelic
                        ? ([.. ModRelic.ModOutRelicLists[(RelicSet)Player.GetModPlayer<EnvironmentPlayer>().ObtainableRelicsKeyOut].Values])
                        : ([.. ModRelic.ModInRelicLists[(RelicSet)Player.GetModPlayer<EnvironmentPlayer>().ObtainableRelicsKeyIn].Values]);
                    
                    int relicNum = MainConfigs.Instance.SimplifiedMode ? 4 : 2;
                    
                    for (int i = 0; i < relicNum; i++)
                    {
                        int type = relicTypes[Main.rand.Next(relicTypes.Length)];
                        NewItemSycn(new EntitySource_Gift(Player), oldPosition, type);
                    }
                }

                if (oldPosition != Vector2.Zero)
                {
                    Player.position = oldPosition;
                }
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (InSubworld)
            {
                SubworldSystem.Exit();
            }
        }

        private void UpdateSubworld()
        {
            if (InSubworld)
            {
                CheckNPCsAndExit();

                if (NPCToSummonLists.TryGetValue(GetObtainableRelicsKey(), out SubworldInfor worldInfo))
                {
                    Main.GlobalTimerPaused = false;
                    if (worldInfo.dayTime)
                    {
                        Main.dayTime = true;
                        Main.time = 27000;
                    }
                    else
                    {
                        Main.dayTime = false;
                        Main.time = 16200;
                    }
                }
            }
        }

        private void CheckNPCsAndExit()
        {
            downedAllNPC = summonedNPCs.All(npc => !npc.active || npc.type == NPCID.None);

            if (downedAllNPC)
            {
                exitCheckTimer++;
                if (exitCheckTimer >= 240)
                {
                    exitCheckTimer = 0;
                    SubworldSystem.Exit();
                }
                else
                {
                    ShowExitText();
                }
            }
        }

        private void ShowExitText()
        {
            if (exitCheckTimer == 180)
            {
                Main.NewText(GetTextValue("Mods.StarRailRelic.SubworldExit1"));
            }
            else if (exitCheckTimer == 120)
            {
                Main.NewText(GetTextValue("Mods.StarRailRelic.SubworldExit2"));
            }
            else if (exitCheckTimer == 60)
            {
                Main.NewText(GetTextValue("Mods.StarRailRelic.SubworldExit3"));
            }
            else if (exitCheckTimer == 30)
            {
                Main.NewText(GetTextValue("Mods.StarRailRelic.SubworldExit4"));
            }
        }

        private void InitializeWorld()
        {
            if (NPCToSummonLists.TryGetValue(GetObtainableRelicsKey(), out SubworldInfor worldInfo))
            {
                Player.Center = new(Player.Center.X - worldInfo.playerWidth, Player.Center.Y - worldInfo.playerHeight);

                summonedNPCs.Clear();
                for (int i = 0; i < worldInfo.summonNPCInfors.Count; i++)
                {
                    SummonNPCInfor summonNPCInfor = worldInfo.summonNPCInfors[i];
                    Vector2 summonPosition = summonNPCInfor.npcPosition + Player.Center;

                    NPC npc = NewNPCSycn(Player.GetSource_FromThis(), summonPosition, summonNPCInfor.npcType);
                    summonedNPCs.Add(npc);
                }

                if (worldInfo.structureKey != "")
                {
                    string structureKey = worldInfo.structureKey;
                    if (MainConfigs.Instance.CompatibilityMode)
                    {
                        structureKey = "NormalRuin";
                    }
                    Generator.GenerateStructure($"Assets/Structures/{structureKey}", worldInfo.structurePosition + new Point16((int)(Player.Center.X / 16), (int)(Player.Center.Y / 16)), Mod);
                }
            }
        }

        private RelicSet GetObtainableRelicsKey()
        {
            return Player.GetModPlayer<EnvironmentPlayer>().isOutRelic ?
                (RelicSet)Player.GetModPlayer<EnvironmentPlayer>().ObtainableRelicsKeyOut :
                (RelicSet)Player.GetModPlayer<EnvironmentPlayer>().ObtainableRelicsKeyIn;
        }
    }

    public struct SubworldInfor(List<SummonNPCInfor> summonNPCInfors)
    {
        public List<SummonNPCInfor> summonNPCInfors = summonNPCInfors;
        public string structureKey = "NormalRuin";
        public Point16 structurePosition = new(-65, -33);
        public bool dayTime = true;
        public int playerHeight = 0;
        public int playerWidth = 0;

        public SubworldInfor(List<SummonNPCInfor> summonNPCInfors, string structureKey, Point16 structurePosition, bool dayTime = true, int playerHeight = 0, int playerWidth = 0) : this(summonNPCInfors)
        {
            this.structureKey = structureKey;
            this.structurePosition = structurePosition;
            this.dayTime = dayTime;
            this.playerHeight = playerHeight;
            this.playerWidth = playerWidth;
        }
    }

    public struct SummonNPCInfor(int npcType, Vector2 npcPosition)
    {
        public int npcType = npcType;
        public Vector2 npcPosition = npcPosition;
    }
}
