using System;
using System.Numerics;
using FreeScape.Engine.Input;
using SFML.Graphics;

namespace FreeScape.Engine.Core.GameObjects.UI
{
    public class Button : IButton
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale => Vector2.One;
        public Vector2 Size { get; }
        public Texture ButtonTexture { get; set; }
        public Action OnClickAction { get; }
        public bool Hovered { get; set; }

        private float _wiggleSpeed = 1f;
        private double _wiggleDuration;
        
        private const float _maxWiggleAngle = 3f;

        private readonly ActionProvider _actionProvider;
        private readonly FrameTimeProvider _frameTimeProvider;
        private readonly Color _defaultColor = Color.White;
        private readonly Color _hoverColor = Color.Green;
        private readonly Sprite _buttonSprite;
        private readonly bool _wigglable;


        public Button(ButtonInfo info, Texture defaultTexture, ActionProvider actionProvider, FrameTimeProvider frameTimeProvider)
        {
            _actionProvider = actionProvider;
            _frameTimeProvider = frameTimeProvider;
            OnClickAction = info.OnClickAction;
            Size = info.Size;
            Position = info.Position;
            ButtonTexture = defaultTexture;
            defaultTexture.Smooth = true;
            _buttonSprite = new Sprite(defaultTexture);
            _buttonSprite.Scale = Size / new Vector2(600, 200);
            _buttonSprite.Position = Position;
            _buttonSprite.Origin = new Vector2(300, 100);
            _wigglable = info.Wigglable;
        }
        
        public void Init() { }

        public void Render(RenderTarget target)
        {
            if (Hovered)
            {
                _buttonSprite.Color = _hoverColor;
            }
            else
            {
                _buttonSprite.Color = _defaultColor;
            }

            target.Draw(_buttonSprite);
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
        private void StartWiggle()
        {
            _wiggleDuration = 100;
            _wiggleSpeed = 1.5f;
        }
        private void Wiggle()
        {
            _wiggleDuration -= _frameTimeProvider.DeltaTimeMilliSeconds;
            if (_wiggleDuration <= 0)
            {
                _buttonSprite.Rotation = 0;
                return;
            }
            _buttonSprite.Rotation = _buttonSprite.Rotation + _wiggleSpeed;
            if (Math.Abs(_buttonSprite.Rotation) > _maxWiggleAngle) 
                _wiggleSpeed = -_wiggleSpeed;
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
            Wiggle();
            _buttonSprite.Position = Position;
        }

        public void OnClick()
        {
            OnClickAction();
            Hovered = false;
        }
    }
}