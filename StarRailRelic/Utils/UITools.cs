using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent;
using Terraria.UI;
using Microsoft.Xna.Framework;

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
            //uiElement.Left.Pixels = (1744 - width) / 2;
            uiElement.Left.Set((1744 - width) / 2, 0f);
            uiElement.Top.Set((980 - height) / 2 + topOffset, 0f);
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
        #endregion
    }
}
