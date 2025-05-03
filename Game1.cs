using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _vningsuppfsfkj;

public class Game1 : Game
{

    public static Game1 Game{get;set;}
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public List<BaseClass> entities{get; set;} = new List<BaseClass>();
    private BulletSystem bulletSystem;
    private Player player;
    Texture2D bullet;
    SpriteFont font;
    private Shop shop;


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
        entities.Add(new Enemy(Content.Load<Texture2D>("enemy"), new Vector2(400,380), entities));
    }
    public void SpawnNewEnemy(List<BaseClass> entities)
    {
        Random random = new Random();
        Vector2 newEnemyPosition;
        float minDistance = 200f;
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

    protected override void Update(GameTime gameTime)
    {   
        shop.Update();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var BaseClass in entities){
            BaseClass.Update(gameTime);
        }
        BulletSystem.Instance.Update(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, entities, gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Aqua);

        _spriteBatch.Begin();

        foreach(var BaseClass in entities){
            BaseClass.Draw(_spriteBatch);
        }
        BulletSystem.Instance.Draw(_spriteBatch); 
        _spriteBatch.DrawString(font, "Kills: ", new Vector2(5, 10), Color.Black);
        _spriteBatch.DrawString(font, BulletSystem.killCount.ToString(), new Vector2(50, 10), Color.Black);

        _spriteBatch.DrawString(font, "HP: ", new Vector2(5, 30), Color.Black);
        _spriteBatch.DrawString(font, player.hp.ToString(), new Vector2(40, 30), Color.Black);

        _spriteBatch.DrawString(font, "XP: " + PointSystem.Instance.XP.ToString(), new Vector2(5, 50), Color.Black);
        _spriteBatch.DrawString(font, shop.Message, new Vector2(5, 90), Color.Black);

        _spriteBatch.DrawString(font, "Speed: " + player.GetSpeed(), new Vector2(5, 70), Color.Black);

        _spriteBatch.DrawString(font, "SHOP" , new Vector2(600, 10), Color.Black);
        _spriteBatch.DrawString(font, "Speed" , new Vector2(600, 30), Color.Black);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
