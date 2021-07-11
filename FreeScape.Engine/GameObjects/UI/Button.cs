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

        private RectangleShape rectangleShape;

        private readonly ActionProvider _actionProvider;

        public ButtonInfo Info;


        public Button(ButtonInfo info, Texture defaultTexture, Texture hoverTexture, ActionProvider actionProvider)
        {
            Info = info;
            _actionProvider = actionProvider;
            OnClickAction = Info.OnClickAction;
            Size = Info.Size;
            Position = Info.Position;
            ButtonTextureDefault = defaultTexture;
            ButtonTextureHover = hoverTexture;
            rectangleShape = new RectangleShape(Size);
            rectangleShape.Texture = defaultTexture;
            rectangleShape.Position = Position;
        }


        public void Render(RenderTarget target)
        {
            if (Hovered)
            {
                rectangleShape.Texture = ButtonTextureHover;
            }
            else
            {
                rectangleShape.Texture = ButtonTextureDefault;
            }
            target.Draw(rectangleShape);
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
            rectangleShape.Position = Position;
        }

        public void OnClick()
        {
            OnClickAction();
            Hovered = false;
        }
    }
}