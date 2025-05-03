using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _vningsuppfsfkj
{
    public class Enemy : BaseClass
    {
        int speed = 1;
        Player p;
        public Enemy(Texture2D texture, Vector2 position, List<BaseClass> entities)
            :base(texture, position)
        {
            foreach (var entity in entities){
                if (entity is Player){
                    p = (Player)entity;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 direction = p.GetPosition();
            direction = direction - position;
            if( direction != Vector2.Zero){
                direction.Normalize();
            }
            position+=direction * speed;
            
        }
        public void Teleport(Vector2 newPosition)
        {
            position = newPosition; 
        }

    }
}