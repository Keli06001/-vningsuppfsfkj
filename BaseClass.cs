using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _vningsuppfsfkj
{
    public class BaseClass
    {
        protected Vector2 position;
        protected Texture2D texture;
        protected Color color;
        protected Rectangle hitbox;

        public BaseClass(Texture2D texture, Vector2 position, Color color, Point size)
        {
            this.texture = texture;
            this.color = color;
            this.position = position;
            hitbox = new Rectangle(position.ToPoint(),size);
        }

        public virtual void Update(){

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y,100,100);
            
            spriteBatch.Draw(texture, position, color);
        }
    }
}