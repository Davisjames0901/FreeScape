using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using System.Numerics;
using FreeScape.Engine.Physics.Movements;

namespace FreeScape.GameObjects
{
    public class PlayerActions : Animated
    {
        private string _defaultPlayerActionName = "idle";
        private string _defaultPlayerDirection = "down";
        public string PlayerActionName = "none";
        public string PlayerActionDirection = "none";
        private bool _performingPlayerAction = false;

        public float PlayerActionSpeedModifier = 1.0f;
        public PlayerActions(SoundProvider soundProvider, FrameTimeProvider frameTimeProvider, 
                                AnimationProvider animationProvider, MapProvider mapProvider) : base(animationProvider, mapProvider)
        {
            PlayerActionName = _defaultPlayerActionName;
            PlayerActionDirection = _defaultPlayerDirection;
            AnimationName = _defaultPlayerActionName + ":" + _defaultPlayerDirection;
        }
        
        public void ActionTick(HeadingVector headingVector, bool roll, bool attack, bool block)
        {
            PlayerActionDirection = GetAnimationDirection(headingVector);
            
            if(PlayerActionName == "block")
            {
                if (!block)
                {
                    StopAction();
                }
            }
            if (_performingPlayerAction)
            {
                ContinueAction();
            }
            else
            {
                if (roll)
                {
                    StartAction("roll");
                }
                else if (block)
                {
                    StartAction("block");
                }
                else if (attack)
                {
                    StartAction("attack");
                }
                else if (headingVector.Direction != Direction.None)
                {
                    StartAction("walk");
                }
                else StartDefault();
            }

        }
        public string GetAnimationDirection(HeadingVector headingVector)
        {
            if (headingVector.Direction.HasFlag(Direction.Left)) return "left";
            if (headingVector.Direction.HasFlag(Direction.Right)) return "right";
            if (headingVector.Direction.HasFlag(Direction.Up)) return "up";
            if (headingVector.Direction.HasFlag(Direction.Down)) return "down";
            return PlayerActionDirection;
        }

        public void StartAction(string action)
        {
            switch (action)
            {
                case "walk":
                    if (CanWalk())
                    {
                        StartWalk();
                    }
                    break;
                case "roll":
                    if (CanRoll())
                    {
                        StartRoll();
                    }
                    break;
                case "attack":
                    if (CanAttack())
                    {
                        StartAttack();
                    }
                    break;
                case "block":
                    if (CanBlock())
                    {
                        StartBlock();
                    }
                    break;
                default:
                    StartDefault();
                    break;
            }
        }
        public void ContinueAction()
        {
            switch (PlayerActionName)
            {
                case "walk":
                        Walk();
                    break;
                case "roll":
                    Roll();
                    break;
                case "attack":
                    Attack();
                    break;
                case "block":
                    Block();
                    break;
                default:
                    StartDefault();
                    break;
            }
        }
        public void StartDefault()
        {
            PlayerActionName = _defaultPlayerActionName;
            AnimationName = _defaultPlayerActionName + ":" + PlayerActionDirection;
            PlayerActionSpeedModifier = 1.0f;
        }
        private bool CanWalk()
        {

            return true;
        }
        private void StartWalk()
        {
            PlayerActionName = "walk";
            AnimationName = "walk:" + PlayerActionDirection;
        }
        private void Block()
        {

            //AnimationName = "block:" + PlayerActionDirection;
            PlayerActionSpeedModifier = 0.05f;
            _performingPlayerAction = true;
        }
        private bool CanBlock()
        {

            return true;
        }
        private void StartBlock()
        {
            PlayerActionName = "block";
            AnimationName = "block:" + PlayerActionDirection;
            _performingPlayerAction = true;
        }
        private void Walk()
        {

        }
        private bool CanRoll()
        {


            return true;
        }
        private void StartRoll()
        {
            PlayerActionName = "roll";
            AnimationName = "roll:" + PlayerActionDirection;
            _performingPlayerAction = true;
            PlayerActionSpeedModifier = 2.0f;
        }
        private void Roll()
        {

            if (AnimationIterations > 0)
            {
                StopAction();
            }
            else
                AnimationName = "roll:" + PlayerActionDirection;
        }

        private bool CanAttack()
        {


            return true;
        }
        private void StartAttack()
        {
            PlayerActionName = "attack";
            AnimationName = "attack:" + PlayerActionDirection;
            _performingPlayerAction = true;
            PlayerActionSpeedModifier = 0.2f;
        }
        private void Attack()
        {
            if (AnimationIterations > 0)
            {
                StopAction();
            }

        }
        private void StopAction()
        {
            _performingPlayerAction = false;
            StartDefault();
        }
    }
}
