using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
//using FreeScape.Engine.Render.Utilities;
using System;
using System.Numerics;
using FreeScape.Engine.Config.UI;
using SFML.Graphics;
using FreeScape.Engine.Render.Utilities;
using System.Diagnostics;
using FreeScape.Engine.Managers;

namespace FreeScape.Engine.GameObjects.UI
{
    public class Button : IRenderable, IButton
    {
        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Texture ButtonTexture { get; set; }

        public Color DefaultColor = Color.White;
        public Color HoverColor = Color.Green;
        public Sprite ButtonSprite { get; set; }

        public Action OnClickAction;

        public bool Hovered { get; set; } = false;

        private bool _wigglable = false;
        private bool _wiggling = false;
        private float _wiggleSpeed = 0.05f;
        private float _maxWiggleAngle = 3f;

        private Stopwatch _wiggleTimer;

        private int _wiggleDuration = 150;

        private readonly ActionProvider _actionProvider;
        private readonly DisplayManager _displayManager;

        public ButtonInfo Info;


        public Button(ButtonInfo info, Texture defaultTexture, ActionProvider actionProvider, DisplayManager displayManager)
        {
            Info = info;
            _actionProvider = actionProvider;
            _displayManager = displayManager;
            OnClickAction = Info.OnClickAction;
            Size = Info.Size;
            Position = Info.Position;
            ButtonTexture = defaultTexture;
            var scale = Size / new Vector2(600, 200);
            ButtonSprite = new Sprite(defaultTexture);
            ButtonSprite.Scale = scale;
            ButtonSprite.Position = Position;
            ButtonSprite.Origin = new Vector2(300, 100);
            _wiggleTimer = new Stopwatch();
            _wigglable = info.Wigglable;
        }
        public void Init()
        {

        }

        public void Render(RenderTarget target)
        {
            if (Hovered)
            {
                ButtonSprite.Color = HoverColor;
            }
            else
            {
                ButtonSprite.Color = DefaultColor;
            }

            target.Draw(ButtonSprite);
        }

        public void OnHover()
        {
            Hovered = true;
            if(_wigglable)
                StartWiggle();
        }
        public void OnHoverEnd()
        {
            Hovered = false;
        }
        public void StartWiggle()
        {
            _wiggling = true;
            _wiggleTimer.Restart();
        }
        private void wiggle()
        {
            if(_wiggleTimer.ElapsedMilliseconds < _wiggleDuration)
            {
                ButtonSprite.Rotation = ButtonSprite.Rotation + _wiggleSpeed;
                if (ButtonSprite.Rotation > _maxWiggleAngle || ButtonSprite.Rotation < -_maxWiggleAngle) _wiggleSpeed = -_wiggleSpeed;
            }
            else
            {
                _wiggling = false;
                ButtonSprite.Rotation = 0;
                _wiggleTimer.Stop();
            }
        }
        public void Tick()
        {
            var mouseCoords = _actionProvider.GetMouseWorldCoods();
            var hovering = mouseCoords.X > Position.X - Size.X / 2 && mouseCoords.Y  > Position.Y - Size.Y / 2 && mouseCoords.X < Position.X + Size.X / 2 && mouseCoords.Y < Position.Y + Size.Y / 2;
            if (!Hovered && hovering)
            {
                OnHover();
            }
            else if(!hovering)
            {
                OnHoverEnd();
            }
            if (_wiggling)
                wiggle();
            ButtonSprite.Position = Position;
        }

        public void OnClick()
        {
            OnClickAction();
            Hovered = false;
        }
    }
}