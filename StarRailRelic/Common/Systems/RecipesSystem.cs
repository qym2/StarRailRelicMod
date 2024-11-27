using StarRailRelic.Content.Items.Relic.Out.One.Fire;
using StarRailRelic.Content.Items.Relic.Out.Two.Watch;
using Terraria.ModLoader;

namespace StarRailRelic.Common.Systems
{
    public class RecipesSystem : ModSystem
    {
        public override void AddRecipeGroups()
        {
            AddRecipeGroupsEasily(nameof(ItemID.GoldCrown), ItemID.GoldCrown, ItemID.PlatinumCrown);
            AddRecipeGroupsEasily(nameof(ItemID.SilverBar), ItemID.SilverBar, ItemID.TungstenBar);
            AddRecipeGroupsEasily(nameof(ItemID.GoldBar), ItemID.GoldBar, ItemID.PlatinumBar);
            AddRecipeGroupsEasily(nameof(ItemID.CopperBar), ItemID.CopperBar, ItemID.TinBar);
        }

        private static void AddRecipeGroupsEasily(string name, params int[] vaildItems)
        {
            RecipeGroup.RegisterGroup(name, new(() => $"{Any} {Lang.GetItemNameValue(vaildItems[0])}", vaildItems));
        }

        public override void AddRecipes()
        {
            LoadAllRelicRecipes();

            Recipe.Create(ItemID.GPS)
              .AddIngredient<WatchHands>()
              .AddIngredient(ItemID.DepthMeter)
              .AddIngredient(ItemID.Compass)
              .DisableDecraft()
              .Register();
        }

        public static void LoadAllRelicRecipes()
        {
            // 获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 找到所有的 ModRelic 子类，排除抽象类
            IEnumerable<Type> relicTypes = assembly.GetTypes()
                                      .Where(t => t.IsSubclassOf(typeof(ModRelic)) && !t.IsAbstract);

            foreach (Type relicType in relicTypes)
            {
                object instance = typeof(ContentInstance<>).MakeGenericType(relicType).GetProperty("Instance").GetValue(null);
                if (instance is ModItem modItem)
                {
                    Recipe.Create(ItemType<LostCrystal>(), 150)
                      .AddIngredient(modItem.Type)
                      .DisableDecraft()
                      .Register();
                }
            }
        }
    }
}
