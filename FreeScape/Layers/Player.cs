using System.Numerics;
using FreeScape.Engine;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;
using FreeScape.Engine.Physics;
using SFML.Graphics;
using SFML.System;
using FreeScape.Engine.GameObjects.UI;
using FreeScape.Engine.Managers;
using FreeScape.Scenes;

namespace FreeScape.Layers
{
    public class Player: IMovable
    {
        private readonly ActionProvider _actionProvider;
        private readonly DisplayManager _displayManager;
        public int ZIndex { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Weight { get; set; }
        public Vector2 Size { get; set; }
        public bool Collidable { get; set; } = false;
        public float Speed { get; set; }

        private Vector2 _velocity = new (0, 0);
        private Vector2 _position = new (0, 0);

        public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
        public Vector2 Position { get { return _position; } set { _position = value; } }

        
        public Player(ActionProvider actionProvider, SoundProvider soundProvider, TextureProvider textureProvider, SceneManager sceneManager, DisplayManager displayManager)
        {
            _actionProvider = actionProvider;
            _displayManager = displayManager;


            actionProvider.SubscribeOnPressed(a =>
            {
                if(a == "Punch")
                    soundProvider.PlaySound("punch");
            });
            //_position = new Vector2(600, 600);
            
        }

        public void Init()
        {

            ZIndex = 999;
            Velocity = new Vector2(0, 0);
            Speed = 5.0f;
            Size = new Vector2(3.0f, 3.0f);
            _position = new Vector2(500, 500);

            _displayManager.CurrentPerspective.View.Center = Position;
        }

        public void Tick()
        {
            SetVelocity();
        }

        public void ActionPressed()
        {

        }
        public void ActionReleased()
        {

        }

        private void SetVelocity()
        {
            var xMovement = GetXMovement();
            var yMovement = GetYMovement();

            _velocity = (xMovement, yMovement) switch
            {
                (0, 0) => Vector2.Zero,
                (-1 or 1, -1 or 1) => new Vector2(Speed * xMovement, Speed * yMovement) /1.5f,
                (_, _) => new Vector2(Speed * xMovement, Speed * yMovement)
            };
        }

        private int GetXMovement()
        {
            var left = _actionProvider.IsActionActivated("MoveLeft");
            var right = _actionProvider.IsActionActivated("MoveRight");
            if (left == right)
                return 0;
            if (left)
                return -1;
            return 1;
        }

        private int GetYMovement()
        {
            var up = _actionProvider.IsActionActivated("MoveUp");
            var down = _actionProvider.IsActionActivated("MoveDown");
            if (up == down)
                return 0;
            if (up)
                return -1;
            return 1;
        }


        public void Render(RenderTarget target)
        {
            var player = new CircleShape(Size.X);
            player.FillColor = Color.Red;
            player.Position = new Vector2(_position.X - (Size.X), _position.Y - (Size.Y));
                
            target.Draw(player);
        }
    }
}