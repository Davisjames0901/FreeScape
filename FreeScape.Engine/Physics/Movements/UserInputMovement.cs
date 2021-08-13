namespace FreeScape.Engine.Physics.Movements
{
    public class UserInputMovement : IMovement
    {
        private readonly KeyboardMovement _keyboardMovement;

        public UserInputMovement(KeyboardMovement keyboardMovement)
        {
            _keyboardMovement = keyboardMovement;
        }
         
        public HeadingVector HeadingVector { get; }
        public void Tick()
        {
            _keyboardMovement.Tick();
            throw new System.NotImplementedException();
        }
    }
}