using FreeScape.Engine.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FreeScape.Engine.Physics.Movements;

namespace FreeScape.GameObjects
{
    public class PlayerController : PlayerActions
    {
        public PlayerController(SoundProvider soundProvider, FrameTimeProvider frameTimeProvider,
                                AnimationProvider animationProvider, MapProvider mapProvider) : base(soundProvider, frameTimeProvider, animationProvider, mapProvider)
        {

        }

        public void ControllerTick(HeadingVector headingVector, bool roll, bool attack, bool block)
        {
            ActionTick(headingVector, roll, attack, block);
        }
    }
}
