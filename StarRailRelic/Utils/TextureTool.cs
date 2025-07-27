namespace StarRailRelic.Utils
{
    public static class TextureTool
    {
        /// <summary>
        /// 根据类名获取图片路径
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetTexture<T>() where T : ModType
        {
            return (typeof(T).Namespace + "." + typeof(T).Name).Replace('.', '/');
        }

        /// <summary>
        /// 根据类名获取图片（泛型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Texture2D GetTextureValue<T>() where T : ModType
        {
            return Request<Texture2D>((typeof(T).Namespace + "." + typeof(T).Name).Replace('.', '/')).Value;
        }

        /// <summary>
        /// 根据类名获取图片（实例）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Texture2D GetTextureValue<T>(this T t) where T : ModType
        {
            return Request<Texture2D>((t.GetType().Namespace + "." + t.GetType().Name).Replace('.', '/')).Value;
        }

        /// <summary>
        /// 根据图片路径获取图片
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Texture2D GetTextureValue(string Texture)
        {
            return Request<Texture2D>(Texture).Value;
        }

        /// <summary>
        /// 根据图片路径获取图片
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Asset<Texture2D> GetTexture(string Texture)
        {
            return Request<Texture2D>(Texture);
        }

        /// <summary>
        /// 装备遗器按钮图片
        /// </summary>
        public static Asset<Texture2D> RelicButtonTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/RelicButton");
        /// <summary>
        /// 装备遗器按钮图片（鼠标悬浮）
        /// </summary>
        public static Asset<Texture2D> RelicButton_HoverTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/RelicButton_Hover");
        /// <summary>
        /// 遗器栏位图片
        /// </summary>
        public static Asset<Texture2D> RelicSlotTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/RelicSlot");
        /// <summary>
        /// 遗器升级按钮图片
        /// </summary>
        public static Asset<Texture2D> RelicStrengtheningButtonTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/RelicStrengtheningButton");
        /// <summary>
        /// 遗器升级按钮图片（鼠标悬浮）
        /// </summary>
        public static Asset<Texture2D> RelicStrengtheningButton_HoverTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/RelicStrengtheningButton_Hover");
        /// <summary>
        /// 开拓力图片
        /// </summary>
        public static Asset<Texture2D> TrailblazePowerTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/TrailblazePower");
        /// <summary>
        /// 遗器栏位图片
        /// </summary>
        public static Asset<Texture2D> RelicPanelTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/RelicPanel");
        /// <summary>
        /// 增加按钮图片
        /// </summary>
        public static Asset<Texture2D> AddDifficultyButtonTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/Add");
        /// <summary>
        /// 增加按钮图片（鼠标悬浮）
        /// </summary>
        public static Asset<Texture2D> AddDifficultyButton_HoverTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/Add_Hover");
        /// <summary>
        /// 减少按钮图片
        /// </summary>
        public static Asset<Texture2D> SubtractDifficultyButtonTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/Subtract");
        /// <summary>
        /// 减少按钮图片（鼠标悬浮）
        /// </summary>
        public static Asset<Texture2D> SubtractDifficultyButton_HoverTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/Subtract_Hover");
        /// <summary>
        /// 提示按钮图片
        /// </summary>
        public static Asset<Texture2D> TipsButtonTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/Tips");
        /// <summary>
        /// 提示按钮图片（鼠标悬浮）
        /// </summary>
        public static Asset<Texture2D> TipsButton_HoverTexture => Request<Texture2D>("StarRailRelic/Assets/Textures/Tips_Hover");

        /// <summary>
        /// 空图片路径
        /// </summary>
        public static string NullTexturePath => "StarRailRelic/Assets/Textures/Null";
        /// <summary>
        /// 空图片
        /// </summary>
        public static Asset<Texture2D> NullTexture => Request<Texture2D>(NullTexturePath);
    }
}
