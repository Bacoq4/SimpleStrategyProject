using Project.Grid;
using Project.Unit;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class SoldierBase : ObjectBase, IPlaceableOnGrid, IMovable
{
    [field: SerializeField, FoldoutGroup("Debugging"), ReadOnly] public GridObject m_BelongedGO { get; set; }
 
    public void SetSpawnPoint(GridObject _GO)
    {
        // There is no spawn point, ISpawnable interface can be created
    }

    public void SendMessageToUIManager()
    {
        // will be filled
    }

    public void Move(GridObject _StartGO, GridObject _FinishGO)
    {
        StartCoroutine(GridPathfinding.Instance.Move( _StartGO,  _FinishGO, transform, 0.5f));
        _FinishGO.SetObjectBase(this);
        _StartGO.SetObjectBase(null);
    }

}

