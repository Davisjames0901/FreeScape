namespace FreeScape.Engine.Physics.Movements
{
    public class GamePadMovement : IMovement
    {
        public GamePadMovement()
        {
            HeadingVector = new HeadingVector();
        }
        public HeadingVector HeadingVector { get; }
        
        public void Tick()
        {
                
        }
    }
}