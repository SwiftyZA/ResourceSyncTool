using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Common.POCOS;

namespace ResourceSyncTool.Extenders
{
    internal static class DataContractExtenders
    {
        internal static void AddCultures(this List<CultureContainer> culture, IEnumerable<CultureInfo> cultures)
        {
            culture.AddRange(cultures.Select(x => new CultureContainer()
            {
                Culture = x,
                Existing = false,
            }));
        }
    }
}
