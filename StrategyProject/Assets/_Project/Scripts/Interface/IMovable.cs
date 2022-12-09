using Project.Grid;

public interface IMovable : IObject
{
    void Move(GridObject _StartGO, GridObject _FinishGO);
    public GridObject m_BelongedGO { get; set; }
}
