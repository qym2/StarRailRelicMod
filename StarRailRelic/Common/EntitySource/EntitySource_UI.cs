namespace StarRailRelic.Common.EntitySource
{
    public class EntitySource_UI : IEntitySource
    {
        public UIState UIState { get; }

#nullable enable
        public string? Context { get; }

        public EntitySource_UI(UIState uiState, string? context = null)
        {
            UIState = uiState;
            Context = context;
        }
    }
}
