namespace StarRailRelic.Common.Systems
{
    public class DownedBossSystem : ModSystem
    {
        public static bool downedSwarmTrueSting;
        public static bool downedIceOutOfSpace;
        public static bool downedBlazeOutOfSpace;

        public override void ClearWorld()
        {
            downedSwarmTrueSting = false;
            downedIceOutOfSpace = false;
            downedBlazeOutOfSpace = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedSwarmTrueSting)
            {
                tag["downedSwarmTrueSting"] = true;
            }
            if (downedIceOutOfSpace)
            {
                tag["downedIceOutOfSpace"] = true;
            }
            if (downedBlazeOutOfSpace)
            {
                tag["downedBlazeOutOfSpace"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedSwarmTrueSting = tag.ContainsKey("downedSwarmTrueSting");
            downedIceOutOfSpace = tag.ContainsKey("downedIceOutOfSpace");
            downedBlazeOutOfSpace = tag.ContainsKey("downedBlazeOutOfSpace");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new();

            flags[0] = downedSwarmTrueSting;
            flags[1] = downedIceOutOfSpace;
            flags[2] = downedBlazeOutOfSpace;

            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();

            downedSwarmTrueSting = flags[0];
            downedIceOutOfSpace = flags[1];
            downedBlazeOutOfSpace = flags[2];
        }
    }
}
