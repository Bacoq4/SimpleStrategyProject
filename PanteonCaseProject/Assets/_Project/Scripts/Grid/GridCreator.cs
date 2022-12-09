using Project.Data;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Grid
{
    public class GridCreator : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField] private GridObject m_GridObject;
        [field: SerializeField ,FoldoutGroup("Scriptable Objects")] public ScGridCreatorData m_GridCreatorData { get; set; }

        [Button, GUIColor(0.3f,0.5f,0.7f,0.8f)]
        public GridObject[] CreateGrid()
        {
            ScGridCreatorData _Data = m_GridCreatorData;

            GridObject[] _GridObjects = new GridObject[_Data.m_Height * _Data.m_Height];
                
            for (int i = 0; i < _GridObjects.Length; i++)
            {
                _GridObjects[i] = SpawnGridObject(new Vector3(i%_Data.m_Width,(int)(i/_Data.m_Width),0));
                _GridObjects[i].transform.name = i.ToString();
            }

            return _GridObjects;
        }
        
        public GridObject[,] Create2DArrayGrid()
        {
            ScGridCreatorData _Data = m_GridCreatorData;

            GridObject[,] _GridObjects = new GridObject[_Data.m_Height, _Data.m_Width];
                
            for (int i = 0; i < _GridObjects.Length; i++)
            {
                GridObject _GO = SpawnGridObject(new Vector3(i%_Data.m_Width,(int)(i/_Data.m_Width),0));
                _GridObjects[i % _Data.m_Width, i / _Data.m_Width] = _GO; 
                _GO.transform.name = i.ToString();
            }

            return _GridObjects;
        }

        private GridObject SpawnGridObject(Vector3 _SpawnPos)
        {
            GridObject _GO = Instantiate(m_GridObject, _SpawnPos, quaternion.identity);
            _GO.transform.SetParent(transform);

            return _GO;
        }
    }
    
}
