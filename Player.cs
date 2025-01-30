using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _vningsuppfsfkj
{
    public class Player : BaseClass
    {
        public Player(Texture2D texture, Vector2 position, Color color, Point size)
        {
            this.texture = texture;
            this.color = color;
            this.position = position;
            hitbox = new Rectangle(position.ToPoint(),size);
        }
        Vector2 position = new Vector2(0, 0);

        public void Update(){
            position.X += 10;
        }
    }
}