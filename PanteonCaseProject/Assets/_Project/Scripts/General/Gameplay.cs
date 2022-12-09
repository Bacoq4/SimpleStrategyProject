using Project.Building;
using Project.Grid;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Project.General
{
        
    public class Gameplay : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField] private MouseManager m_MouseManager;
        [FoldoutGroup("References"), SerializeField] private GridManager m_GridManager;
        [FoldoutGroup("References"), SerializeField] private BuildingSpawner m_BuildingSpawner;
        
        [FoldoutGroup("Color"), SerializeField] private Color m_InvalidColor;
        [FoldoutGroup("Color"), SerializeField] private Color m_PlaceReadyColor;
        [FoldoutGroup("Color"), SerializeField] private Color m_PlaceColor;
      
        private IMovable m_ChosenMovable;
        private void Update()
        {
            // Get Grid pos from mouse pos
            Vector3 _MousePos = MouseManager.GetMousePositionIn2DWorld();
            Vector3 _GridPos = m_GridManager.GetGridPos(_MousePos);
            
            if (Input.GetMouseButtonDown(0))
            {
                GridObject _GO = m_GridManager.GetGridObjectFromPosition(_GridPos);
                ObjectBase _ObjectBase = _GO.GetObjectBase();
                
                if (m_ChosenMovable != null && _GO.IsPlaceable())
                {
                    m_ChosenMovable.Move(m_ChosenMovable.m_BelongedGO,_GO );
                    m_ChosenMovable = null;
                }
                if (_ObjectBase != null)
                {
                    IPlaceableOnGrid _Placeable = _ObjectBase.GetComponent<IPlaceableOnGrid>();
                    IMovable _Movable = _ObjectBase.GetComponent<IMovable>();

                    _Placeable.SendMessageToUIManager();
                    
                    if (_Movable != null)
                    {
                        m_ChosenMovable = _Movable;
                        m_ChosenMovable.m_BelongedGO = _GO;
                    }
                }
            }
            
            // if there is not object on grid
            /////////////////////////////////////////////////////////////////////////////////////////////////
            if (!m_BuildingSpawner.m_BuildingToPlace) return;

            // drag building until place it
            m_BuildingSpawner.m_BuildingToPlace.transform.position = _GridPos;
            
            // get building data's for grid system to work properly  
            int _Height = m_BuildingSpawner.m_BuildingToPlace.m_BuildingData.m_Height;
            int _Width = m_BuildingSpawner.m_BuildingToPlace.m_BuildingData.m_Width;

            bool _IsGridsPlaceable = m_GridManager.IsGridsPlaceable(_GridPos, _Height, _Width);

            // if it is invalid to place building
            m_BuildingSpawner.m_BuildingToPlace.ChangeColor(m_InvalidColor);
            
            //////////////////////////////////////////////////////////////////////////////////////////////////
            if (!_IsGridsPlaceable) return;
            
            // if building is placeable
            m_BuildingSpawner.m_BuildingToPlace.ChangeColor(m_PlaceReadyColor);
            
            if (Input.GetMouseButtonDown(0))
            {
                // Set grid objects
                ObjectBase _ObjectBase = m_BuildingSpawner.m_BuildingToPlace;
                IPlaceableOnGrid _Placeable = _ObjectBase.GetComponent<IPlaceableOnGrid>(); 
                _Placeable.SetSpawnPoint(m_GridManager.GetDefaultSpawnPoint(_GridPos, _Width));
                m_GridManager.SetGridsObjectBases(_GridPos,  _Height, _Width, _ObjectBase);
                m_BuildingSpawner.m_BuildingToPlace.ChangeColor(m_PlaceColor);
                m_BuildingSpawner.m_BuildingToPlace = null;
            }
        }

        
    }

}