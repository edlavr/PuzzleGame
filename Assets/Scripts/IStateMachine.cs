public interface IStateMachine<in T>
{
    void EnterState(T obj);
    void UpdateState(T obj);
    void ExitState(T obj);
}
