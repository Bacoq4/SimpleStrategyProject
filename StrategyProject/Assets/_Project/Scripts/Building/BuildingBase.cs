using Project.Data;
using Project.Grid;
using Project.Unit;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Building
{
    public abstract class BuildingBase : ObjectBase , IPlaceableOnGrid
    {
        [field: SerializeField ,FoldoutGroup("Scriptable Object")] public ScBuildingData m_BuildingData { get; set; }
        [field: SerializeField ,FoldoutGroup("References")] public SpriteRenderer m_SpriteRenderer { get; set; }

        [SerializeField, FoldoutGroup("Settings")] protected BuildingType m_BuildingType;

        public static UnityAction<BuildingType> OnChosenByPlayer { get; set; }

        public abstract void SetSpawnPoint(GridObject _GO);

        public abstract void SendMessageToUIManager();
        
        public void ChangeColor(Color _Color)
        {
            m_SpriteRenderer.color = _Color;
        }
    }

}