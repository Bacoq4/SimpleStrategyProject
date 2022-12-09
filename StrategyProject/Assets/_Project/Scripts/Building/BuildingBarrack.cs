using Project.Grid;
using Project.Sender;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Building
{
    public class BuildingBarrack : BuildingBase
    {
        [FoldoutGroup("Debugging"), SerializeField, ReadOnly] private GridObject m_SpawnPoint;
        [FoldoutGroup("References"), SerializeField] protected SoldierBase m_Soldier;

        private void OnEnable()
        {
            ProduceButtonMessageSender.OnButtonPressed += ProduceSoldier;
        }

        private void OnDisable()
        {
            ProduceButtonMessageSender.OnButtonPressed -= ProduceSoldier;
        }

        public override void SetSpawnPoint(GridObject _GO)
        {
            m_SpawnPoint = _GO;
        }

        private void ProduceSoldier()
        {
            if (m_SpawnPoint.IsPlaceable())
            {
                SoldierBase _Soldier = SpawnSoldier();
                _Soldier.transform.position = m_SpawnPoint.transform.position;
                m_SpawnPoint.SetObjectBase(_Soldier);
            }
            else
            {
                Debug.Log("Spawn point needs to be changed until dynamic spawn system is implemented");
            }
        }

        private SoldierBase SpawnSoldier()
        {
            return Instantiate(m_Soldier);
        }

        public override void SendMessageToUIManager()
        {
            OnChosenByPlayer?.Invoke(m_BuildingType);
        }
    }

}
