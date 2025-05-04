using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _vningsuppfsfkj
{
    public class Player : BaseClass
    {
        private MouseState oldState;
        private float timeSinceLastClick = 0f;
        private float clickCooldown = 1.2f; 
        private float timeSinceLastDeath = 0f; 
        private float spawnCooldown = 1f; 
        private float shootCooldown = 0.5f; 
        private float shootTimer = 0f;
        private bool dead = false; 
        public int hp = 3; 
        private float baseSpeed = 5f;
        private float currentSpeed;  
        public static bool IsPlayerRespawning = false;

        public Player(Texture2D texture)
            : base(texture, new Vector2(350, 190))
        {
            position = new Vector2(350, 190);
        }


        public override void Update(GameTime gameTime)
        {
            shootTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (dead)
            {
                timeSinceLastDeath += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceLastDeath >= spawnCooldown)
                {

                    Respawn();
                }
                return; 
            }


            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();
            Vector2 direction = new Vector2(0, 0);
            timeSinceLastClick += (float)gameTime.ElapsedGameTime.TotalSeconds;

            HandleMovement(kState, ref direction);
            position += direction * currentSpeed;

            CheckCollisionWithEnemies();


            if (mState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released && shootTimer <= 0f) 
            {
                Vector2 bulletDirection = mState.Position.ToVector2() - position; 
                bulletDirection.Normalize(); 
                BulletSystem.Instance.SummonBullet(position + new Vector2(50, 50), bulletDirection); 
                shootTimer = shootCooldown;
            }

            oldState = mState;
        }

        private void HandleMovement(KeyboardState kState, ref Vector2 direction)
        {
            currentSpeed = baseSpeed; 
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && timeSinceLastClick >= clickCooldown)
            {
                currentSpeed = baseSpeed * 20;
                timeSinceLastClick = 0f;
            }
            if (kState.IsKeyDown(Keys.W)) direction.Y -= 1;
            if (kState.IsKeyDown(Keys.S)) direction.Y += 1;
            if (kState.IsKeyDown(Keys.A)) direction.X -= 1;
            if (kState.IsKeyDown(Keys.D)) direction.X += 1;

            if (direction != Vector2.Zero) 
            {
                direction.Normalize();
            }
        }

        private void CheckCollisionWithEnemies()
        {
            foreach (BaseClass entity in Game1.Game.entities)
            {
                if (entity is Enemy enemy && this.Rectangle.Intersects(enemy.Rectangle))
                {
                    hp--; 
                    foreach(BaseClass bce in Game1.Game.entities)
                    {
                        if(bce is Enemy e)
                        {
                            TeleportEnemy(e);
                            IsPlayerRespawning = true;
                        }
                    }
                    dead = true; 
                    timeSinceLastDeath = 0f; 
                    position = new Vector2(350, 190); 
                    Console.WriteLine("Player has died!");
                    break; 
                }
            }
        }

        private void Respawn()
        {
            if (timeSinceLastDeath >= spawnCooldown) 
            {
                if (hp > 0) 
                {
                    dead = false; 
                    IsPlayerRespawning = false;
                    timeSinceLastDeath = 0f; 
                    Console.WriteLine("Player respawned!");
                    
                    if (hp <= 0)
                    {
                        Console.WriteLine("Game Over! Please restart.");
                    }
                }
            }
        }
        private void TeleportEnemy(Enemy enemy)
        {
            Random rand = new Random();
            
            Vector2[] possiblePositions = {
                new Vector2(0, 0),      
                new Vector2(700, 0),     
                new Vector2(0, 400),   
                new Vector2(700, 400)   
            };

            Vector2 selectedPosition = possiblePositions[rand.Next(possiblePositions.Length)];

            enemy.Teleport(selectedPosition);
        }
        public void UpgradeSpeed(float amount)
        {
            baseSpeed += amount;
        }
        public void AddHP(int amount)
        {
            hp += amount;
        }
        public void UpgradeFireRate()
        {
            shootCooldown = Math.Max(0.05f, shootCooldown - 0.05f); // Stoppar vid 0.05s
        }
        public float GetShootCooldown()
        {
            return shootCooldown;
        }
        public Vector2 GetPosition() => position; 
        public float GetSpeed() => baseSpeed;
    }
}