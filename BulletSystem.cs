using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _vningsuppfsfkj
{
    public class BulletSystem
    {
        public static BulletSystem Instance{
            get{
                return Instance;
            }
        }
        private static BulletSystem instance;
        List<Bullet> bullets = new List<Bullet>();
        Texture2D bulletTexture;

        public static void CreateInstance(Texture2D bulletTexture){
            instance = new BulletSystem(bulletTexture);
        }

        private BulletSystem(Texture2D bulletTexture){
            this.bulletTexture = bulletTexture;
        }

        public void SummonBullet(Vector2 position, double angle){
            bullets.Add(new Bullet(bulletTexture, position, angle));
        }

        private void RemoveBullet(){

        }

        public virtual void Update(){
            foreach(var Bullet in bullets){
                Bullet.Update();
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(var Bullet in bullets){
                Bullet.Draw(spriteBatch);
            }
        }
    }
}