namespace StarRailRelic.Common.Commands
{
	public class SummonCommand : ModCommand
	{
		// CommandType.World means that command can be used in Chat in SP and MP, but executes on the Server in MP
		public override CommandType Type => CommandType.World;

		// The desired text to trigger this command
		public override string Command => "qianyimuSummon";

		public override void Action(CommandCaller caller, string input, string[] args) {
			// Checking input Arguments
			if (args.Length == 0)
            {
                return;
            }
			if (!int.TryParse(args[0], out int type))
            {
                return;
            }

			// Default values for spawn
			// Position - Player.Bottom, number of NPC - 1 
			int xSpawnPosition = (int)caller.Player.Bottom.X;
			int ySpawnPosition = (int)caller.Player.Bottom.Y;
			int numToSpawn = 1;
			bool relativeX = false;
			bool relativeY = false;

			// If command has X position argument
			if (args.Length > 1)
            {
                // X relative check
                if (args[1][0] == '~')
                {
                    relativeX = true;
					args[1] = args[1][1..];
				}
				// Parsing X position
				if (!int.TryParse(args[1], out xSpawnPosition))
                {
                    return;
                }
			}

			// If command has Y position argument
			if (args.Length > 2)
            {
                // Y relative check
                if (args[2][0] == '~') {
					relativeY = true;
					args[2] = args[2][1..];
				}
				// Parsing Y position
				if (!int.TryParse(args[2], out ySpawnPosition))
                {
                    return;
                }
			}

			// Adjusting the positions if they are relative
			if (relativeX)
            {
                xSpawnPosition += (int)caller.Player.Bottom.X;
			}
			if (relativeY)
            {
                ySpawnPosition += (int)caller.Player.Bottom.Y;
			}

			// If command has number argument
			if (args.Length > 3)
            {
                if (!int.TryParse(args[3], out numToSpawn))
                {
                    return;
                }
			}

			// Spawning numToSpawn NPCs with a given position and type
			for (int k = 0; k < numToSpawn; k++)
            {
                // NPC.NewNPC return 200 (Main.maxNPCs) if there are not enough NPC slots to spawn
                int slot = NPC.NewNPC(new EntitySource_DebugCommand($"{nameof(StarRailRelic)}_{nameof(SummonCommand)}"), xSpawnPosition, ySpawnPosition, type);

				// Sync of NPCs on the server in MP
				if (Main.netMode == NetmodeID.Server && slot < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, slot);
				}
			}
		}
	}
}
