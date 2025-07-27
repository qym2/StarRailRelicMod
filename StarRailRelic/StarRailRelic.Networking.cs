namespace StarRailRelic
{
    public partial class StarRailRelic
    {
        public enum MessageType : byte
        {
            RelicPlayer
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType messageType = (MessageType)reader.ReadByte();

            switch (messageType)
            {
                case MessageType.RelicPlayer:
                    byte playerNumber = reader.ReadByte();
                    RelicPlayer relicPlayer = Main.player[playerNumber].GetModPlayer<RelicPlayer>();
                    relicPlayer.ReceivePlayerSync(reader);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        relicPlayer.SyncPlayer(-1, whoAmI, false);
                    }
                    break;
                default:
                    Logger.WarnFormat("StarRailRelic: Unknown Message type: {0}", messageType);
                    break;
            }
        }
    }
}
