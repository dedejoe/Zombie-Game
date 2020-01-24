using Microsoft.Xna.Framework;

namespace Space_Game
{
    class ParticleEffect
    {
        //Ints
        public int frame = 0;
        public int textureID;
        public int lifetime;
        public int size;
        //Floats
        public double rotation;
        //Vector2s
        public Vector2 position;
        public Vector2 velocity;
        private Player myPlayer;
        public Color myColor;
        public Zombie myZombie;

        public ParticleEffect(int newTexture, Vector2 newPosition, int newLifetime, Vector2 newVelocity, int newSize, int newFrame, Player myPlr, Zombie myZombi, Color newColor)
        {
            textureID = newTexture;
            position = newPosition;
            lifetime = newLifetime;
            velocity = newVelocity;
            size = newSize;
            frame = newFrame;
            myPlayer = myPlr;
            myZombie = myZombi;
            myColor = newColor;
        }

        public bool Increment(int numFrames)
        {
            if (frame == lifetime)
                return true;
            else
            {
                for (int i = 0; i < numFrames; i++)
                {
                    position.X += velocity.X;
                    position.Y += velocity.Y;
                    frame += 1;
                }
                return false;
            }
        }
    }
}
