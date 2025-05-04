using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _vningsuppfsfkj
{
    public class Shop
    {
        Player player;
        private KeyboardState oldKeyState;
        public string Message { get; private set; } = "";
        private float messageTimer = 0f; 
        private const float MessageDisplayTime = 2f;  

        public Shop(Player player)
        {
            this.player = player;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newKeyState = Keyboard.GetState();
            messageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (messageTimer <= 0)
            {
                Message = ""; 
            }

            if (newKeyState.IsKeyDown(Keys.O) && oldKeyState.IsKeyUp(Keys.O))
            {
                if (PointSystem.Instance.SpendXP(200))
                {
                    player.UpgradeFireRate();
                    Message = "Fire rate upgraded!";
                    messageTimer = MessageDisplayTime;
                }
                else
                {
                    Message = "Not enough XP!";
                    messageTimer = MessageDisplayTime;
                }
            }

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

            if (newKeyState.IsKeyDown(Keys.I)&& oldKeyState.IsKeyUp(Keys.I))
            {
                if (PointSystem.Instance.SpendXP(1000)) 
                {
                    player.AddHP(1);
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
        public void SetMessage(string message)
        {
            Message = message;
            messageTimer = MessageDisplayTime;
        }
    }
}