using Microsoft.Xna.Framework.Input;

namespace _vningsuppfsfkj
{
    public class Shop
    {
        Player player;
        public string Message { get; private set; } = "";

        public Shop(Player player)
        {
            this.player = player;
        }

        public void Update()
        {
            KeyboardState kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.U))
            {
                if (PointSystem.Instance.SpendXP(100)) 
                {
                    player.UpgradeSpeed(1); 
                    Message = "Speed upgraded!";
                }
                else
                {
                    Message = "Not enough XP!";
                }
            }
        }
    }
}
