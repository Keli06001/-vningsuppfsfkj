using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _vningsuppfsfkj;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    List<BaseClass> entities = new List<BaseClass>();
    private BulletSystem bulletSystem;
    private Player player;
    Texture2D bullet;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2D playerTexture = Content.Load<Texture2D>("player"); 

        player = new Player(playerTexture);

        Texture2D pixel;
        Texture2D bullet;

        pixel = new Texture2D(GraphicsDevice,1,1);
        pixel.SetData(new Color[]{Color.White});

        bullet = Content.Load<Texture2D>("bullet");
        BulletSystem.CreateInstance(bullet);

        entities.Add(player);
        entities.Add(new Enemy(pixel, new Vector2(400,380), entities));
    }
    private void SpawnNewEnemy(List<BaseClass> entities)
    {
        Random random = new Random();
        Vector2 newEnemyPosition = new Vector2(random.Next(100, 700), random.Next(100, 500)); // Adjust range as needed
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
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
