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

        public Rectangle Rectangle{
            get{return Rectangle;}
        }

        public BaseClass(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            color = Color.White;
        }

        public virtual void Update(){

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y,100,100);
            
            spriteBatch.Draw(texture, hitbox, color);
        }
    }
}