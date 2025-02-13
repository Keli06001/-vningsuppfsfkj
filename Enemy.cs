using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _vningsuppfsfkj
{
    public class Enemy : BaseClass
    {
        public Enemy(Texture2D texture, Vector2 position)
            :base(texture, position)
        {
            color = Color.Red;
        }
        public override void Update()
        {
            
        }

    }
}