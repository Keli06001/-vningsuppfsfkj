using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace _vningsuppfsfkj
{
    public class Bullet : BaseClass
    {
        private int size;
        Texture2D bulletTexture;
        private float speed = 5;

        private Vector2 direction;

        public Bullet(Texture2D texture, Vector2 position, Vector2 direction)
            :base(texture, new Microsoft.Xna.Framework.Vector2(position.X, position.Y))
        {
            this.bulletTexture = texture;
            this.direction = direction;
            this.position = position;
        }

        public override void Update(){
            position += direction * speed;
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width / 4, texture.Height / 4);
        }
        public bool IsOffScreen(int viewportWidth, int viewportHeight)
        {
            return position.X < 0 || position.Y < 0 || position.X > viewportWidth || position.Y > viewportHeight;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, bulletTexture.Width, bulletTexture.Height);
            Vector2   scale;
            scale.X = 0.04f;
            scale.Y = 0.04f;

         spriteBatch.Draw(bulletTexture, position, sourceRectangle,Color.White, (float)((Math.Atan2(direction.Y,direction.X))), position/2, scale,SpriteEffects.None,1);
        }
        
    }
}