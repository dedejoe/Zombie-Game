using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Game
{
    class StaticEffect
    {
        //Ints
        public int frame = 0;
        public int lifetime;
        public Texture2D myTexture;
        public Rectangle rec;
        public Color myColor;
        public int type;

        public StaticEffect(int lifespan, int myType, Texture2D newTexture, Rectangle newRec, Color newColor)
        {
            lifetime = lifespan;
            myTexture = newTexture;
            rec = newRec;
            type = myType;
            if (type == 0) frame = lifetime;
            myColor = newColor;
        }

        public bool Increment(int numFrames)
        {
            if (type == 0) {
                if (frame == 0)
                    return true;
                else
                {
                    frame--;
                    return false;
                }
            }
            else if (type == 1)
            {
                if (frame == lifetime)
                    return true;
                else
                {
                    frame++;
                    return false;
                }
            }
            return false;
        }
    }
}
