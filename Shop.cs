using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _vningsuppfsfkj
{
    public class Shop
    {
        Player player;
        public string Message { get; private set; } = "";
        private KeyboardState oldKeyState;
        private float messageTimer = 0f; 
        private const float MessageDisplayTime = 2f;  

        public Shop(Player player)
        {
            this.player = player;
        }

        public void Update(GameTime gameTime)
        {
            messageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (messageTimer <= 0)
            {
                Message = ""; 
            }

            KeyboardState newKeyState = Keyboard.GetState();

            if (newKeyState.IsKeyDown(Keys.U) && oldKeyState.IsKeyUp(Keys.U))
            {
                if (PointSystem.Instance.SpendXP(100)) 
                {
                    player.UpgradeSpeed(1);
                    Message = "Speed upgraded!";
                    messageTimer = MessageDisplayTime;  
                }
                else
                {
                    Message = "Not enough XP!";
                    messageTimer = MessageDisplayTime;  
                }
            }

            oldKeyState = newKeyState;
        }
    }
}