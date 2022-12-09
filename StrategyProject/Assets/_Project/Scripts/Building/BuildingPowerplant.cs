using Project.Grid;

namespace Project.Building
{
    public class BuildingPowerplant : BuildingBase
    {
        public override void SetSpawnPoint(GridObject _GO)
        {
            // There is no spawn point
        }

        public override void SendMessageToUIManager()
        {
            OnChosenByPlayer?.Invoke(m_BuildingType);
        }
    }

}

