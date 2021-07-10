using System.Numerics;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.GameObjects.Entities;
using FreeScape.Engine.Providers;
using SFML.Graphics;
using SFML.System;

namespace FreeScape.Engine.Config.Map
{
    public class Tile : IGameObject, ICollider
    {

        public float Size { get; set; }
        public bool Collidable { get; set; }
        public Vector2 Position { get; set; }

        public Vector2 ColliderSize { get; set; }

        public RectangleShape TileRectangleShape;
        public Tile(Vector2 position, bool collidable, float size, Texture texture)
        {
            Size = size;
            Collidable = collidable;
            Position = position;
            ColliderSize = new Vector2(Size, Size);
            RectangleShape rectangleShape = new RectangleShape(new Vector2(Size, Size));

            rectangleShape.Texture = texture;
            rectangleShape.Position = new Vector2(Position.X, Position.Y);
            if(rectangleShape.Texture == null)
            {
                rectangleShape.FillColor = Color.Yellow;
                rectangleShape.OutlineColor = Color.Black;
                rectangleShape.OutlineThickness = 1;
            }
            TileRectangleShape = rectangleShape;
        }
        public void Render(RenderTarget target)
        {
            target.Draw(TileRectangleShape);
        }

        public void Tick()
        {
        }
    }
}