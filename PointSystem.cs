using System;
namespace _vningsuppfsfkj
{
    public class PointSystem
    {
        public static PointSystem Instance { get; private set; } = new PointSystem();

        private int xp;
        public int XP => xp;

        private PointSystem()
        {
            xp = 0;
        }

        public void AddXP(int amount)
        {
            xp += amount;
        }

        public bool SpendXP(int amount)
        {
            if (xp >= amount)
            {
                xp -= amount;
                return true;
            }
            return false;
        }
    }
}