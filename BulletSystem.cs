using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _vningsuppfsfkj
{
    public class BulletSystem
    {
        public static BulletSystem Instance{
            get{
                return instance;
            }
        }
        private static BulletSystem instance;
        List<Bullet> bullets = new List<Bullet>();
        Texture2D bulletTexture;
        public static int killCount= 0;

        public static void CreateInstance(Texture2D bulletTexture){
            instance = new BulletSystem(bulletTexture);
        }

        private BulletSystem(Texture2D bulletTexture){
            this.bulletTexture = bulletTexture;
        }

        public void SummonBullet(Vector2 position, Vector2 direction){
            bullets.Add(new Bullet(bulletTexture, position, direction));
        }
        public virtual void Update(int viewportWidth, int viewportHeight, List<BaseClass> entities, GameTime gameTime)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Update(gameTime);

                // Remove bullets that go off-screen
                if (bullets[i].IsOffScreen(viewportWidth, viewportHeight))
                {
                    bullets.RemoveAt(i);
                    continue;
                }

                // Check for collision with enemies
                for (int j = entities.Count - 1; j >= 0; j--)
                {
                    if (entities[j] is Enemy enemy && bullets[i].Rectangle.Intersects(enemy.Rectangle))
                    {
                        entities.RemoveAt(j); 
                        bullets.RemoveAt(i); 
                        killCount++;
                        Game1.Game.SpawnNewEnemy(entities);
                        break;
                    }
                }
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