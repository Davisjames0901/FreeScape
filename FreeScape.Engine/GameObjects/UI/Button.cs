using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using SFML.Graphics;
using System;
using System.Numerics;

namespace FreeScape.Engine.GameObjects.UI
{
    public class Button : IRenderable, IButton
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Texture ButtonTextureDefault { get; set; }
        public Texture ButtonTextureHover { get; set; }

        public Action OnClickAction;

        public bool Hovered { get; set; } = false;

        private RectangleShape button;

        private readonly ActionProvider _actionProvider;


        public Button(Vector2 position, Vector2 size, Texture textureDefault, Texture textureHover, Action onClickAction, ActionProvider actionProvider)
        {
            _actionProvider = actionProvider;
            OnClickAction = onClickAction;
            Size = size;
            Position = position;
            ButtonTextureDefault = textureDefault;
            ButtonTextureHover = textureHover;
            button = new RectangleShape(Size);
            button.Texture = textureDefault;
            button.Position = Position;
        }


        public void Render(RenderTarget target)
        {
            if (Hovered)
            {
                button.Texture = ButtonTextureHover;
            }
            else
            {
                button.Texture = ButtonTextureDefault;
            }
            target.Draw(button);
        }

        public void OnHover()
        {
            Hovered = true;
        }
        public void OnHoverEnd()
        {
            Hovered = false;
        }
        
        public void Tick()
        {
            var mouseCoords = _actionProvider.GetMouseWorldCoods();
            if (mouseCoords.X > Position.X && mouseCoords.Y > Position.Y && mouseCoords.X < Position.X + Size.X && mouseCoords.Y < Position.Y + Size.Y)
            {
                OnHover();
            }
            else
            {
                OnHoverEnd();
            }
        }

        public void OnClick()
        {
            OnClickAction();
        }
    }
}