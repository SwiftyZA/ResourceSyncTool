using System;
using Common.Enums;

namespace ResourceSyncTool.Extenders
{
    internal static class EnumExtentions
    {
        internal static string ToUserFriendlyString(this State state)
        {
            switch (state)
            {
                case State.NoChange:
                    return "No Change";
                case State.Updated:
                    return "Updated";
                case State.New:
                    return "New";
                case State.GoogleTranslated:
                    return "Translated by Google";
                case State.MasterChanged:
                    return "Master File Changed";
                case State.Faulted:
                    return "Faulted";
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
