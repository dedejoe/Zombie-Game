namespace Space_Game
{
    class Timer
    {

        public int lifetime;
        public int index;
        public bool on;

        public Timer() { }

        public void Tick(int howManyTicks)
        {
            index += howManyTicks;
            if (index >= lifetime)
            {
                on = false;
            }
        }

        public void ResetTimer()
        {
            index = 0;
            on = false;
        }

        public int getIndex()
        {
            return index;
        }

        public void StartTimer(int when, int howLong)
        {
            index = when;
            lifetime = howLong;
            on = true;
        }

        public void StopTimer(int when = 0)
        {
            if(when <= index)
            {
                on = false;
            }
            else
            {
                lifetime = when;
            }
        }

    }
}
