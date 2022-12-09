using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Grid
{
    [RequireComponent(typeof(LineRenderer))]
    public class GridObject : MonoBehaviour
    {
        [FoldoutGroup("Debugging"), SerializeField, ReadOnly] private ObjectBase m_ObjectBase;

        public int m_gCost { get; set; }
        public int m_hCost { get; set; }
        public int m_fCost { get; set; }

        public GridObject m_PreviousGO { get; set; }
        public bool IsPlaceable()
        {
            return m_ObjectBase == null;
        }
        public void SetObjectBase(ObjectBase _ObjectBase)
        {
            m_ObjectBase = _ObjectBase;
        }

        public ObjectBase GetObjectBase()
        {
            return m_ObjectBase;
        }

        public void CalculateFCost()
        {
            m_fCost = m_gCost + m_hCost;
        }
    }
}


