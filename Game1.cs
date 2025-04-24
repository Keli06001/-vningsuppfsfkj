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
    List<BaseClass> entities = new List<BaseClass>();
    private BulletSystem bulletSystem;
    private Player player;
    Texture2D bullet;
    SpriteFont font;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Game = this;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        font = Content.Load<SpriteFont>("File");

        Texture2D playerTexture = Content.Load<Texture2D>("player"); 
        player = new Player(playerTexture);

        Texture2D bullet;

        bullet = Content.Load<Texture2D>("bullet");
        BulletSystem.CreateInstance(bullet);

        entities.Add(player);
        entities.Add(new Enemy(Content.Load<Texture2D>("enemy"), new Vector2(400,380), entities));
    }
    public void SpawnNewEnemy(List<BaseClass> entities)
    {
        Random random = new Random();
        Vector2 newEnemyPosition = new Vector2(random.Next(100, 700), random.Next(100, 500));
        entities.Add(new Enemy(Content.Load<Texture2D>("enemy"), newEnemyPosition, entities));
    }

    protected override void Update(GameTime gameTime)
    {   
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var BaseClass in entities){
            BaseClass.Update();
        }
        BulletSystem.Instance.Update(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, entities);
        player.Update(gameTime);
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
        _spriteBatch.DrawString(font, BulletSystem.killCount.ToString(), new Vector2(10, 10), Color.Black);
        Console.WriteLine(BulletSystem.killCount);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
