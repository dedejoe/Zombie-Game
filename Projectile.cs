using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Drawing;

namespace Space_Game
{
    class Projectile
    {
        //Ints
        public int frame = 0;
        public int lifetime;
        public int hits = 0;
        public int numHits;

        public Weapon myWeapon;

        public float distanceTraveled = 0f;

        //Vector2s
        public Vector2 position;
        public Vector2 velocity;


        public RectangleF BoundingBox;
        public ParticleEffect myParticleEffect;


        public Projectile(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
        }

        public Projectile(ParticleEffect newParticleEffect, Vector2 newPosition, Vector2 newVelocity, int newLifeTime, int newHits, RectangleF newBoundingBox, Weapon newWeapon)
        {
            myParticleEffect = newParticleEffect;
            BoundingBox = newBoundingBox;
            position = newPosition;
            velocity = newVelocity;
            lifetime = newLifeTime;
            numHits = newHits;
            myWeapon = newWeapon;
        }

        public bool Increment(int numFrames)
        {
            if (frame == lifetime) BoundingBox = new RectangleF(0, 0, 0, 0);
            if (frame == lifetime && myParticleEffect == null)
                return true;
            else
            {
                for (int i = 0; i < numFrames; i++)
                {
                    if (myParticleEffect != null && myParticleEffect.Increment(1))
                    {
                        myParticleEffect = null;
                    }
                    if (frame != lifetime)
                    {
                        position.X += velocity.X;
                        BoundingBox.X += velocity.X;
                        position.Y += velocity.Y;
                        BoundingBox.Y += velocity.Y;
                        distanceTraveled += (velocity.X + velocity.Y);
                        frame += 1;
                    }
                }
                return false;
            }
        }
    }
}
