using UnityEngine;
using ICities;

namespace TerrainMastery
{
    public class TerrainMasteryLoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
            {
                TerrainMasteryPanel.OnLoad();
            }
        }
    }
}
