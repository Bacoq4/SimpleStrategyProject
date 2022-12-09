using Project.Sender;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Building
{
    public class BuildingSpawner : MonoBehaviour
    {
        [field: SerializeField ,FoldoutGroup("Debugging"), ReadOnly] public BuildingBase m_BuildingToPlace { get; set; }

        [FoldoutGroup("Array"), SerializeField] public BuildingBase[] m_Buildings;
    
        private void OnEnable()
        {
            PlaceButtonMessageSender.OnButtonPressed += CreateBuilding;
        }
        private void OnDisable()
        {
            PlaceButtonMessageSender.OnButtonPressed -= CreateBuilding;
        }
    
        private void CreateBuilding(BuildingType _BuildingType)
        {
            switch (_BuildingType)
            {
                case BuildingType.Barrack:
                    m_BuildingToPlace = Instantiate(m_Buildings[0]);
                    break;
                case BuildingType.PowerPlant:
                    m_BuildingToPlace = Instantiate(m_Buildings[1]);
                    break;
            }
        }
    }
}
