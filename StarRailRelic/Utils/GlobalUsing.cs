global using System;
global using System.IO;
global using System.Linq;
global using System.Text;
global using System.Threading;
global using System.Reflection;
global using System.ComponentModel;
global using System.Threading.Tasks;
global using System.Collections.Generic;

global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;

global using static Microsoft.Xna.Framework.MathHelper;

global using Terraria;
global using Terraria.ID;
global using Terraria.UI;
global using Terraria.IO;
global using Terraria.Chat;
global using Terraria.Enums;
global using Terraria.Audio;
global using Terraria.ModLoader;
global using Terraria.Utilities;
global using Terraria.GameInput;
global using Terraria.ObjectData;
global using Terraria.GameContent;
global using Terraria.Localization;
global using Terraria.DataStructures;
global using Terraria.ModLoader.IO;
global using Terraria.ModLoader.Config;
global using Terraria.GameContent.Bestiary;
global using Terraria.GameContent.Events;
global using Terraria.GameContent.Personalities;
global using Terraria.ModLoader.UI;
global using Terraria.GameContent.UI.Elements;
global using Terraria.GameContent.ItemDropRules;
global using Terraria.Graphics.CameraModifiers;
global using Terraria.GameContent.ObjectInteractions;

global using ReLogic.Content;

//global using SubworldLibrary;
global using StructureHelper.API;
global using InnoVault;
global using InnoVault.UIHandles;

global using static Terraria.Localization.Language;
global using static Terraria.ModLoader.ModContent;
global using NPCSets = Terraria.ID.NPCID.Sets;
global using BestiaryBiomes = Terraria.GameContent.Bestiary.BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes;

global using static StarRailRelic.Utils.MyTools;
global using static StarRailRelic.Utils.TextureTool;
global using static StarRailRelic.Utils.LocalizationTool.UI;
global using static StarRailRelic.Utils.LocalizationTool.Terraria;

global using StarRailRelic.Utils;
global using StarRailRelic.Common.UI;
global using StarRailRelic.Common.NPCs;
global using StarRailRelic.Content.NPCs;
global using StarRailRelic.Content.Items;
global using StarRailRelic.Content.Tiles;
global using StarRailRelic.Content.Buffs;
global using StarRailRelic.Common.Players;
global using StarRailRelic.Common.Systems;
global using StarRailRelic.Common.Configs;
global using StarRailRelic.Content.Rarities;
global using StarRailRelic.Common.UI.Relic;
global using StarRailRelic.Common.UI.SU;
global using StarRailRelic.Content.NPCs.Boss;
global using StarRailRelic.Content.Items.Relic;
global using StarRailRelic.Content.Projectiles.Hostile;
global using StarRailRelic.Content.Items.Miscellaneous;
global using StarRailRelic.Content.Items.Consumables.Bossbag;
global using StarRailRelic.Content.Items.Consumables.BossSummonItem;