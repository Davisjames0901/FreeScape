using FreeScape.Engine.Physics.Movement;

namespace FreeScape.Engine.Input.Controllers
{
    public class UserInputController : IController
    {
        private readonly KeyboardController _keyboardController;

        public UserInputController(KeyboardController keyboardController)
        {
            _keyboardController = keyboardController;
        }

        public ActionProvider CurrentActionProvider => _keyboardController.ActionProvider;

        public HeadingVector HeadingVector => _keyboardController.HeadingVector;
        public void Tick()
        {
            _keyboardController.Tick();
            //HeadingVector = _keyboardMovement.HeadingVector;
        }
    }
}