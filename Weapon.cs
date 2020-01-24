using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Game
{
    class Weapon
    {
        //Floats
        public int bulletLifetime;
        public float bulletWeight;
        public int bulletDamage;
        public float damageFallOff;
        public float velocity;
        public int bulletSize;
        public float particleDisplacement;
        public bool aimEnabled;

        public Weapon(bool aim, int bulletsiz, int lifeTime, int damage, float weight, float dmgFO, float newVelocity, float particleDisp)
        {
            aimEnabled = aim;
            bulletLifetime = lifeTime;
            bulletWeight = weight;
            bulletDamage = damage;
            damageFallOff = dmgFO;
            bulletSize = bulletsiz;
            velocity = newVelocity;
            particleDisplacement = particleDisp;
        }

        public float maxRange ()
        {
            if (damageFallOff > 0f)
            {
                return 10;
            }
            else
            {
                return bulletLifetime/2;
            }
        }

    }
}
