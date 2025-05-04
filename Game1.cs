using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _vningsuppfsfkj
{
    public enum GameState
    {
        Menu,
        Playing,
        GameOver
    }
    public class Game1 : Game
    {
        private GameState currentState = GameState.Menu;
        public static Game1 Game{get;set;}
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public List<BaseClass> entities{get; set;} = new List<BaseClass>();
        private BulletSystem bulletSystem;
        private Player player;
        Texture2D bullet;
        SpriteFont font;
        private Shop shop;
        private int wave = 0;
        private int killsThisWave = 0;
        private bool inBreak = false;
        private float breakTimer = 0f;
        private const float breakDuration = 15f; 
        private int killsPerWave = 30; 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Game = this;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");

            Texture2D playerTexture = Content.Load<Texture2D>("player"); 
            player = new Player(playerTexture);
            shop = new Shop(player);

            Texture2D bullet;

            bullet = Content.Load<Texture2D>("bullet");
            BulletSystem.CreateInstance(bullet);

            entities.Add(player);
            StartNextWave();
        }
        public void SpawnNewEnemy(List<BaseClass> entities)
        {
            if(!inBreak)
            {
                Random random = new Random();
                Vector2 newEnemyPosition;
                float minDistance = 250f;
                do
                {
                    newEnemyPosition = new Vector2(
                        random.Next(100, 700),
                        random.Next(100, 500)
                    );
                }
                while (Vector2.Distance(player.GetPosition(), newEnemyPosition) < minDistance);

                entities.Add(new Enemy(Content.Load<Texture2D>("enemy"), newEnemyPosition, entities));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (inBreak)
            {
                breakTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (breakTimer <= 0)
                {
                    StartNextWave();
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kState.IsKeyDown(Keys.Escape))
                Exit();

            switch (currentState)
            {
                case GameState.Menu:
                    if (kState.IsKeyDown(Keys.Enter)) 
                    {
                        currentState = GameState.Playing;
                    }
                    break;

                case GameState.Playing:
                    shop.Update(gameTime);
                    foreach (var entity in entities)
                        entity.Update(gameTime);

                    BulletSystem.Instance.Update(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, entities, gameTime);

                    if (player.hp <= 0)
                    {
                        currentState = GameState.Menu;
                        wave=1;
                        ResetGame();
                    }

                    break;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, $"Wave: {wave}", new Vector2(5, 110), Color.Black);

            if (inBreak)
            {
                _spriteBatch.DrawString(font, $"Next wave in: {Math.Ceiling(breakTimer)}s", new Vector2(5, 150), Color.Red);
            }

            if (currentState == GameState.Menu)
            {
                _spriteBatch.DrawString(font, "Välkommen till spelet!", new Vector2(200, 100), Color.Black);
                _spriteBatch.DrawString(font, "Rör dig med WASD, skjut med vänsterklick", new Vector2(200, 130), Color.Black);
                _spriteBatch.DrawString(font, "Tryck på Shift för att rusa", new Vector2(200, 160), Color.Black);
                _spriteBatch.DrawString(font, "Tryck U för att uppgradera speed i shoppen", new Vector2(200, 190), Color.Black);
                _spriteBatch.DrawString(font, "Tryck I för att köpa ett HP", new Vector2(200, 220), Color.Black);
                _spriteBatch.DrawString(font, "Tryck O för att uppgradera skjuthastighet", new Vector2(200, 250), Color.Black);
                _spriteBatch.DrawString(font, "Tryck ENTER för att starta spelet", new Vector2(200, 300), Color.Red);
            }
            else if (currentState == GameState.Playing)
            {
                foreach (var entity in entities)
                    entity.Draw(_spriteBatch);

                BulletSystem.Instance.Draw(_spriteBatch);

                _spriteBatch.DrawString(font, "Kills: " + BulletSystem.killCount.ToString(), new Vector2(5, 10), Color.Black);
                _spriteBatch.DrawString(font, "XP: " + PointSystem.Instance.XP.ToString(), new Vector2(5, 30), Color.Black);
                _spriteBatch.DrawString(font, "HP: " + player.hp.ToString(), new Vector2(5, 50), Color.Black);
                _spriteBatch.DrawString(font, "Speed: " + player.GetSpeed(), new Vector2(5, 70), Color.Black);
                _spriteBatch.DrawString(font, "Cooldown: "+ Math.Round(player.GetShootCooldown(), 2)+ "s", new Vector2(5, 90), Color.Black);
                _spriteBatch.DrawString(font, shop.Message, new Vector2(5, 130), Color.Black);
                _spriteBatch.DrawString(font, "Kills to next wave " + (killsPerWave-killsThisWave), new Vector2(300, 10), Color.Black);
                _spriteBatch.DrawString(font, "SHOP", new Vector2(600, 10), Color.Black);
                _spriteBatch.DrawString(font, "100XP Speed: U", new Vector2(600, 30), Color.Black);
                _spriteBatch.DrawString(font, "1000XP +1HP: I", new Vector2(600, 50), Color.Black);
                _spriteBatch.DrawString(font, "200XP Faster Fire: O", new Vector2(600, 70), Color.Black);

            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void RegisterKill()
        {
            killsThisWave++;

            if (!inBreak && killsThisWave >= killsPerWave)
            {
                inBreak = true;
                breakTimer = breakDuration;
                shop.SetMessage($"Wave {wave} cleared! Break for {breakDuration} seconds!");
            }
        }
        private void StartNextWave()
        {
            wave++;
            killsThisWave = 0;
            killsPerWave += 10*(wave-1);
            inBreak = false;

            for (int i = 0; i < (wave == 1 ? 1 : (wave-1) * 2); i++)
            {
                SpawnNewEnemy(entities);
            }

            shop.SetMessage($"Wave {wave} started!");
        }
        private void ResetGame()
        {
            entities.Clear();

            Texture2D playerTexture = Content.Load<Texture2D>("player");
            player = new Player(playerTexture);
            shop = new Shop(player);

            BulletSystem.killCount = 0;
            PointSystem.Instance.AddXP(-PointSystem.Instance.XP); 

            entities.Add(player);
            entities.Add(new Enemy(Content.Load<Texture2D>("enemy"), new Vector2(400, 380), entities));
        }

    }

}