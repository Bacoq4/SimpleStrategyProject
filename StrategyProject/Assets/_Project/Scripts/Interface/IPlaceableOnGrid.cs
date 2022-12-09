using Project.Grid;
public interface IPlaceableOnGrid : IObject
{
    void SetSpawnPoint(GridObject _GO);
    void SendMessageToUIManager();
}
