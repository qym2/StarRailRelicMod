using UIHoverImage = StarRailRelic.Common.UI.UIHoverImage;

namespace StarRailRelic.Utils
{
    public static class UITools
    {
        #region UI扩展方法
        /// <summary>
        /// 初始化UI
        /// </summary>
        /// <param name="uiElement">UI元素</param>
        /// <param name="left">距离屏幕左边的距离</param>
        /// <param name="top">距离屏幕上边的距离</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public static void SetRectangle(this UIElement uiElement, float left, float top, float width, float height)
        {
            uiElement.Left.Set(left, 0f);
            uiElement.Top.Set(top, 0f);
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }

        /// <summary>
        /// 初始化UI，并根据UI的宽和高使UI居中
        /// </summary>
        /// <param name="uiElement">UI元素</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="topOffset">顶部的偏移量</param>
        public static void SetRectangle(this UIElement uiElement, float width, float height, float topOffset = 0)
        {
            uiElement.Left.Set((Main.screenWidth - width) / 2, 0f);
            uiElement.Top.Set((Main.screenHeight - height) / 2 + topOffset, 0f);
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }

        /// <summary>
        /// 获取文字的右上角坐标
        /// </summary>
        /// <param name="uiText">目标UIText</param>
        /// <returns>文字的右上角坐标</returns>
        public static Vector2 GetTextRightPosition(this UIText uiText)
        {
            float lastCharacterRightX = uiText.GetDimensions().X + FontAssets.MouseText.Value.MeasureString(uiText.Text).X;
            float lastCharacterRightY = uiText.GetDimensions().Y + FontAssets.MouseText.Value.MeasureString(uiText.Text).Y;
            Vector2 position = new(lastCharacterRightX, lastCharacterRightY);
            return position;
        }

        /// <summary>
        /// 获取文字的左上角坐标
        /// </summary>
        /// <param name="uiText">目标UIText</param>
        /// <returns>文字的左上角坐标</returns>
        public static Vector2 GetTextLeftPosition(this UIText uiText)
        {
            if (uiText.Parent != null)
            {
                return new Vector2(uiText.Left.Pixels + uiText.Parent.Left.Pixels, uiText.Top.Pixels + uiText.Parent.Top.Pixels);
            }
            else
            {
                return new Vector2(uiText.Left.Pixels, uiText.Top.Pixels);
            }
        }

        /// <summary>
        /// 获取文字的宽度和高度
        /// </summary>
        /// <param name="uiText">目标UIText</param>
        /// <returns>文字的宽度和高度</returns>
        public static Vector2 GetTextWidthAndHeight(this UIText uiText)
        {
            return uiText.GetTextRightPosition() - uiText.GetTextLeftPosition();
        }

        /// <summary>
        /// 检查鼠标是否在目标UI元素范围内，若是则设置mouseInterface为true
        /// </summary>
        /// <param name="uiElement">目标UI元素</param>
        public static void CheckMouseInUI(this UIElement uiElement)
        {
            // 避免空引用异常
            if (uiElement == null)
            {
                return;
            }

            // 关键：获取元素在屏幕上的绝对坐标（考虑所有父级偏移）
            CalculatedStyle dims = uiElement.GetDimensions();
            float uiLeft = dims.X;       // 屏幕绝对X坐标
            float uiTop = dims.Y;       // 屏幕绝对Y坐标
            float uiWidth = dims.Width;       // 元素宽度
            float uiHeight = dims.Height;     // 元素高度

            // 检查鼠标屏幕坐标是否在UI元素范围内
            bool isMouseInUI = Main.MouseScreen.X > uiLeft
                            && Main.MouseScreen.Y > uiTop
                            && Main.MouseScreen.X < uiLeft + uiWidth
                            && Main.MouseScreen.Y < uiTop + uiHeight;

            // 若在范围内，设置mouseInterface为true（优先UI交互）
            if (isMouseInUI)
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }

        /// <summary>
        /// 强制触发UI元素的鼠标交互事件（用于解决父容器裁剪导致的交互失效问题）
        /// </summary>
        /// <param name="targetElement">需要检测交互的目标UI元素（如按钮、文本）</param>
        /// <param name="parentContainer">目标元素所在的父容器（用于限制交互范围在父容器内）</param>
        /// <remarks>
        /// 适用于特殊场景：当目标元素因父容器裁剪（如滚动容器、面板）导致交互失效时，
        /// 手动检测鼠标是否同时位于目标元素和父容器范围内，并强制触发鼠标事件（悬停、点击、离开）。
        /// 注意：此方法为特殊场景下的补充方案，优先通过调整UI层级和裁剪规则解决问题。
        /// </remarks>
        public static void EnforceUIElementInteraction(this UIElement targetElement, UIElement parentContainer)
        {
            // 避免空引用异常：目标元素或父容器为空时直接返回
            if (targetElement == null || parentContainer == null)
            {
                return;
            }

            // 获取目标元素在屏幕上的绝对坐标和尺寸（考虑所有父级偏移）
            CalculatedStyle targetDims = targetElement.GetDimensions();
            float targetLeft = targetDims.X;
            float targetTop = targetDims.Y;
            float targetWidth = targetDims.Width;
            float targetHeight = targetDims.Height;

            // 获取父容器在屏幕上的绝对坐标和尺寸（限制交互在父容器范围内）
            CalculatedStyle parentDims = parentContainer.GetDimensions();
            float parentLeft = parentDims.X;
            float parentTop = parentDims.Y;
            float parentWidth = parentDims.Width;
            float parentHeight = parentDims.Height;

            // 检查鼠标是否同时位于目标元素和父容器的范围内
            bool isInTarget = Main.MouseScreen.X > targetLeft
                           && Main.MouseScreen.X < targetLeft + targetWidth
                           && Main.MouseScreen.Y > targetTop
                           && Main.MouseScreen.Y < targetTop + targetHeight;

            bool isInParent = Main.MouseScreen.X > parentLeft
                           && Main.MouseScreen.X < parentLeft + parentWidth
                           && Main.MouseScreen.Y > parentTop
                           && Main.MouseScreen.Y < parentTop + parentHeight;

            bool isMouseInRange = isInTarget && isInParent;

            // 根据鼠标状态触发对应事件
            if (isMouseInRange)
            {
                // 触发鼠标悬停事件
                targetElement.MouseOver(new UIMouseEvent(targetElement, Main.MouseScreen));

                // 触发左键点击事件（检测按下状态）
                if (Main.mouseLeft)
                {
                    targetElement.LeftClick(new UIMouseEvent(targetElement, Main.MouseScreen));
                }
            }
            else
            {
                // 触发鼠标离开事件
                targetElement.MouseOut(new UIMouseEvent(targetElement, Main.MouseScreen));
            }
        }

        public static void ScaleNPCImage(this UIHoverImage image, int npcID, int imageSize = 32)
        {
            Texture2D frameTexture = GetNPCDisplayTexture(npcID);

            float scaleX = (float)imageSize / frameTexture.Width;
            float scaleY = (float)imageSize / frameTexture.Height;
            float scale = Math.Min(scaleX, scaleY);

            image.Set(frameTexture, Lang.GetNPCNameValue(npcID));
            image.ImageScale = scale;
            image.Width = StyleDimension.FromPixels(imageSize);
            image.Height = StyleDimension.FromPixels(imageSize);
        }

        public static void ScaleItemImage(this UIHoverImage image, int itemID, int imageSize = 32)
        {
            Texture2D texture = TextureAssets.Item[itemID].Value;

            float scaleX = (float)imageSize / texture.Width;
            float scaleY = (float)imageSize / texture.Height;
            float scale = Math.Min(scaleX, scaleY);

            image.Set(texture, Lang.GetItemNameValue(itemID));
            image.ImageScale = scale;
            image.Width = StyleDimension.FromPixels(imageSize);
            image.Height = StyleDimension.FromPixels(imageSize);
        }
        #endregion
    }
}