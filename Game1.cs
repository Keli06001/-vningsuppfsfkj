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
    Texture2D pixel;
    Vector2 v1 = new Vector2(0,0);
    Vector2 v2 = new Vector2(0,0);

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
        Texture2D pixel;
        pixel = new Texture2D(GraphicsDevice,1,1);
        pixel.SetData(new Color[]{Color.White});
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        entities.Add(new Player(pixel));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
        || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (BaseClass e in entities){
            e.Update();
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

         _spriteBatch.Begin();

        foreach(var entity in entities){
            entity.Draw(_spriteBatch);
        }

        base.Draw(gameTime);
    }
}
