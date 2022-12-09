using System.Collections;
using System.Collections.Generic;
using Project.General;
using UnityEngine;

namespace Project.Grid
{
    // Obstacles in pathfinding is not implemented
    public class GridPathfinding : SingletonComponent<GridPathfinding>
    {
        [SerializeField] private GridManager m_GridManager;

        private const int MoveCost = 10;
        
        private List<GridObject> m_OpenList;
        private List<GridObject> m_ClosedList;

        public IEnumerator Move(GridObject _StartGO, GridObject _FinishGO, Transform _MovedTr, float _SecToMoveOneTile)
        {
            List<Vector3> _PosList = new List<Vector3>();
            List<GridObject> _GridObjects = FindPath(_StartGO, _FinishGO);

            for (int i = 0; i < _GridObjects.Count; i++)
            {
                _PosList.Add(m_GridManager.GetGridPos(_GridObjects[i].transform.position));
            }
            
            for (int i = 0; i < _PosList.Count; i++)
            {
                _MovedTr.transform.position = _PosList[i];
                yield return new WaitForSeconds(_SecToMoveOneTile);
            }
        }

        private List<GridObject> FindPath(GridObject _StartGO, GridObject _EndGO)
        {
            m_OpenList = new List<GridObject>{ _StartGO };
            m_ClosedList = new List<GridObject>();

            for (int i = 0; i < m_GridManager.m_GridObjects.GetLength(0); i++)
            {
                for (int j = 0; j < m_GridManager.m_GridObjects.GetLength(1); j++)
                {
                    GridObject _GO = m_GridManager.m_GridObjects[i, j];
                    _GO.m_gCost = int.MaxValue;
                    _GO.CalculateFCost();
                    _GO.m_PreviousGO = null;
                }
            }

            _StartGO.m_gCost = 0;
            _StartGO.m_hCost = CalculateDistanceCost(_StartGO, _EndGO);
            _StartGO.CalculateFCost();

            while (m_OpenList.Count > 0)
            {
                GridObject _CurrentGO = GetLowestFCostGO(m_OpenList);
                if (_CurrentGO == _EndGO)
                {
                    // reached
                    return CalculatePath(_EndGO);
                }

                m_OpenList.Remove(_CurrentGO);
                m_ClosedList.Add(_CurrentGO);

                foreach (GridObject _NeighbourGO in GetNeighbourList(_CurrentGO))
                {
                    if(m_ClosedList.Contains(_NeighbourGO)) continue;

                    int tentativeGCost = _CurrentGO.m_gCost + CalculateDistanceCost(_CurrentGO, _NeighbourGO);
                    if (tentativeGCost < _NeighbourGO.m_gCost)
                    {
                        _NeighbourGO.m_PreviousGO = _CurrentGO;
                        _NeighbourGO.m_gCost = tentativeGCost;
                        _NeighbourGO.m_hCost = CalculateDistanceCost(_NeighbourGO, _EndGO);
                        _NeighbourGO.CalculateFCost();

                        if (!m_OpenList.Contains(_NeighbourGO))
                        {
                            m_OpenList.Add(_NeighbourGO);
                        }
                    }
                }
            }
            
            return null;
        }

        private List<GridObject> GetNeighbourList(GridObject _GO)
        {
            List<GridObject> _NeighbourList = new List<GridObject>();

            var position = _GO.transform.position;
            _NeighbourList.Add( m_GridManager.GetGridObjectFromPosition(position + Vector3.right));
            _NeighbourList.Add( m_GridManager.GetGridObjectFromPosition(position + Vector3.up));
            _NeighbourList.Add( m_GridManager.GetGridObjectFromPosition(position + Vector3.left));
            _NeighbourList.Add( m_GridManager.GetGridObjectFromPosition(position + Vector3.down));

            return _NeighbourList;
        }

        private List<GridObject> CalculatePath(GridObject _EndGO)
        {
            List<GridObject> _Path = new List<GridObject>();
            _Path.Add(_EndGO);
            GridObject _CurrentGO = _EndGO;
            while (_CurrentGO.m_PreviousGO != null)
            {
                _Path.Add(_CurrentGO.m_PreviousGO);
                _CurrentGO = _CurrentGO.m_PreviousGO;
            }

            _Path.Reverse();
            return _Path;
        }

        private int CalculateDistanceCost(GridObject _StartGO, GridObject _EndGO)
        {
            var position = _StartGO.transform.position;
            var position1 = _EndGO.transform.position;
            int _xDist = Mathf.Abs((int)position.x - (int)position1.x);
            int _yDist = Mathf.Abs((int)position.y - (int)position1.y);

            return MoveCost * _xDist + MoveCost * _yDist;
        }

        private GridObject GetLowestFCostGO(List<GridObject> _GridObjects)
        {
            GridObject _LowestFCostGO = _GridObjects[0];
            for (int i = 0; i < _GridObjects.Count; i++)
            {
                if (_GridObjects[i].m_fCost < _LowestFCostGO.m_fCost)
                {
                    _LowestFCostGO = _GridObjects[i];
                }
            }

            return _LowestFCostGO;
        }
    }

}