using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace _vningsuppfsfkj
{
    public class Player : BaseClass
    {
        public Player(Texture2D texture)
            :base(texture, new Microsoft.Xna.Framework.Vector2(350, 190))
        {
            color = Microsoft.Xna.Framework.Color.Green;
        }


        public override void Update(){
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

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

            if(mState.LeftButton==Microsoft.Xna.Framework.Input.ButtonState.Pressed) 
            {
                Vector2 vector;
                vector.Y = Button.MousePosition.Y - position.Y;
                vector.X = Button.MousePosition.X - position.X;
                double angle = Math.Atan2(vector.Y, vector.X);
                
            }
            
        }
    }
}