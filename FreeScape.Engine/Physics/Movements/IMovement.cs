namespace FreeScape.Engine.Physics.Movements
{
    public interface IMovement
    {
        HeadingVector HeadingVector { get; }
        void Tick();
    }
}