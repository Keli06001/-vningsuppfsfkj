using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _vningsuppfsfkj
{
    public class Bullet : BaseClass
    {
        private int size;
        Texture2D bulletTexture;
        private float speed = 1;

        private double angle;

        public Bullet(Texture2D texture, Vector2 position, double angle)
            :base(texture, new Microsoft.Xna.Framework.Vector2(position.X, position.Y))
        {
            this.bulletTexture = texture;
            this.angle = angle;
            this.position = position;
        }

        public override void Update(){
            position.X += (float) (Math.Cos(angle)) * speed;
            position.Y += (float) (Math.Sin(angle)) * speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, bulletTexture.Width, bulletTexture.Height);
            Vector2   scale;
            scale.X = 0.01f;
            scale.Y = 0.01f;

            spriteBatch.Draw(bulletTexture, position, sourceRectangle,Color.White, (float)angle, position, scale,SpriteEffects.None,1);
        }
    }
}