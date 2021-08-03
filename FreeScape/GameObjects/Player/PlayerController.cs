using FreeScape.Engine.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.GameObjects
{
    public class PlayerController : PlayerActions
    {
        public PlayerController(ActionProvider actionProvider, SoundProvider soundProvider, FrameTimeProvider frameTimeProvider,
                                AnimationProvider animationProvider, MapProvider mapProvider) : base(actionProvider, soundProvider, frameTimeProvider, animationProvider, mapProvider)
        {

        }

        public void ControllerTick(Vector2 HeadingVector, bool roll, bool attack, bool block)
        {
            ActionTick(HeadingVector, roll, attack, block);
        }
    }
}
