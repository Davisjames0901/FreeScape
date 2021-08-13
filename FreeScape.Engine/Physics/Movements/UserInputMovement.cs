using FreeScape.Engine.Providers;

namespace FreeScape.Engine.Physics.Movements
{
    public class UserInputMovement : IMovement
    {
        private readonly KeyboardMovement _keyboardMovement;

        public UserInputMovement(KeyboardMovement keyboardMovement)
        {
            _keyboardMovement = keyboardMovement;
        }

        public ActionProvider CurrentActionProvider => _keyboardMovement.ActionProvider;
        
        public HeadingVector HeadingVector { get; private set; }
        public void Tick()
        {
            _keyboardMovement.Tick();
            HeadingVector = _keyboardMovement.HeadingVector;
        }
    }
}