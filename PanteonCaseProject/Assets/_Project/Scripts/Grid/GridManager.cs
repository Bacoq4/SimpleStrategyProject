using Project.Unit;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Project.Grid
{
        
    public class GridManager : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField] private GridCreator m_GridCreator;
        public GridObject[,] m_GridObjects { get; set; }
        
        
        private void Start()
        {
            m_GridObjects = m_GridCreator.Create2DArrayGrid();
        }
        public GridObject GetGridObjectFromPosition(Vector3 _Pos)
        {
            return IsPosInsideGrid(_Pos) ? m_GridObjects[(int)_Pos.x, (int)_Pos.y] : null;
        }
        public void SetGridObjectBase(GridObject _GO, ObjectBase _ObjectBase)
        {
            _GO.SetObjectBase(_ObjectBase);
        }
        private bool IsPosInsideGrid(Vector3 _Pos)
        {
            return _Pos.x > -1 && _Pos.x < m_GridCreator.m_GridCreatorData.m_Width && _Pos.y > -1 && _Pos.y < m_GridCreator.m_GridCreatorData.m_Height;
        }
        public Vector3 GetGridPos(Vector3 _MousePos)
        {
            return new Vector3(Mathf.FloorToInt(_MousePos.x), Mathf.FloorToInt(_MousePos.y), 0);
        }
        public void SetGridsObjectBases(Vector3 _Vec, int _Height, int _Width, ObjectBase _ObjectBase)
        {
            for (int i = 0; i < _Height; i++)
            {
                for (int j = 0; j < _Width; j++)
                {
                    GridObject _GO =
                        GetGridObjectFromPosition(_Vec + Vector3.right * j + Vector3.up * i);
                    SetGridObjectBase(_GO, _ObjectBase);
                }
            }
        }
        
        public GridObject GetDefaultSpawnPoint(Vector3 _Vec, int _Width)
        {
            return GetGridObjectFromPosition(_Vec + Vector3.right * _Width);
        }

        public bool IsGridsPlaceable(Vector3 _Vec, int _Height, int _Width)
        {
            for (int i = 0; i < _Height; i++)
            {
                for (int j = 0; j < _Width; j++)
                {
                    GridObject _GO =
                        GetGridObjectFromPosition(_Vec + Vector3.right * j + Vector3.up * i);
                    if(!_GO.IsPlaceable())
                        return false;
                }
            }

            return true;
        }
    }

}