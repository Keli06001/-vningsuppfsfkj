using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;

namespace _vningsuppfsfkj
{
    public class Player : BaseClass
    {
        private MouseState oldState;
        public Player(Texture2D texture)
            :base(texture, new Microsoft.Xna.Framework.Vector2(350, 190))
        {
            color = Microsoft.Xna.Framework.Color.Blue;
        }


        public override void Update(){
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();
            Vector2 direction = new Vector2(0,0);

            int speed = 5;


            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W)) 
            {
                position.Y -= speed;
            }
            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S)) 
            {
                position.Y += speed;
            }
            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) 
            {
                position.X -= speed;
            }
            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) 
            {
                position.X += speed;
            }
            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.R))
            {
                position.X = 350;
                position.Y = 190;
            }
            if(direction != Vector2.Zero){
                direction.Normalize();
            }
            position+=direction * speed;

             if(mState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released) 
            {
                Vector2 bulletDirection = mState.Position.ToVector2() - position;
                bulletDirection.Normalize();

                BulletSystem.Instance.SummonBullet(position, bulletDirection);
            }
                
            
            oldState = mState;
        }

        public Vector2 GetPosition(){
            return position;
        }
    }
}