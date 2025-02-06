using System.Drawing;
using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace _vningsuppfsfkj
{
    public class Enemy : BaseClass
    {
        public Enemy(Texture2D texture, Vector2 position)
            :base(texture, position)
        {
            color = Color.Black;
        }
        
    }
}