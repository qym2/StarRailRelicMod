namespace StarRailRelic.Common.Systems
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind RelicButtonKeybind { get; private set; }

        public override void Load()
        {
            RelicButtonKeybind = KeybindLoader.RegisterKeybind(Mod, "RelicButton", "R");
        }
        
        public override void Unload()
        {
            RelicButtonKeybind = null;
        }
    }
}
