using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;

namespace Space_Game
{
    class Player
    {
        //Ints
        public int lockedframe = 0;
        public int direction = 1;
        public int frame = 3;
        public int maxHealth;
        public int currentHealth;
        public int currentWeapon = 0;

        public Timer moveTimer;
        public Timer weaponSwitchTimer = new Timer();

        //Floats
        public float speed;
        public double rotation;
        public double tmpRotation;

        //Booleans
        public bool tookDamage = false;
        public bool jumped = false;
        public bool canMove = true;
        public bool isShooting;
        public bool isAlive;

        //Texture2Ds
        public Texture2D texture;

        public Texture2D[][] shooting = new Texture2D[][]{
            new Texture2D[18],
            new Texture2D[13]
        };

        public Texture2D[,] running = new Texture2D[2,18];
        public Texture2D[] jumping = new Texture2D[21];

        public Weapon[] weapons = new Weapon[2];

        //Vector2s
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 AimDirection;

        //Rectangles
        public RectangleF BoundingBox
        {
            get
            {
                Vector2 tempPos = new Vector2((Position.X - 32) - 32 * (float)Math.Cos(rotation + (Math.PI / 2)), (Position.Y - 32) - 32 * (float)Math.Sin(rotation + (Math.PI / 2)));
                return new RectangleF(
                   tempPos.X,
                    tempPos.Y,
                    64,
                    64);

            }
        }

        public Player(Texture2D texture, Vector2 position, int myHealth)
        {
            maxHealth = myHealth;
            currentHealth = maxHealth;
            this.texture = texture;
            Position = position;
            weapons[0] = new Weapon(true, 128, 12, 80, 1.5f, 0.95f, 16f, 1f);
            weapons[1] = new Weapon(true, 16, 24, 34, 0.75f, 0f, 32f, 2f);
        }

        public Vector2 Movement(KeyboardState keyState, Zombie[] zombies)
        {
            if (!canMove)
            {
                if (moveTimer.on == true)
                {
                    moveTimer.Tick(1);
                    Vector2 difference = new Vector2(((float)Math.Cos(tmpRotation + (Math.PI / 2)) * (moveTimer.lifetime - moveTimer.index)), ((float)Math.Sin(tmpRotation + (Math.PI / 2))) * (moveTimer.lifetime - moveTimer.index));
                    Position += difference;
                    return difference;
                }
                else
                {
                    canMove = true;
                }
            }
            //keyboard input for debugging
            Vector2 rpp = new Vector2(0, 0);
            if (keyState.IsKeyDown(Keys.Right) == true)
            {
                Position.X += 1 * speed;
                rpp.X += 1 * speed;
            }
            else if (keyState.IsKeyDown(Keys.Left) == true)
            {
                Position.X -= 1 * speed;
                rpp.X -= 1 * speed;
            }
            if (keyState.IsKeyDown(Keys.Up) == true)
            {
                Position.Y -= 1 * speed;
                rpp.Y -= 1 * speed;
            }
            else if (keyState.IsKeyDown(Keys.Down) == true)
            {
                Position.Y += 1 * speed;
                rpp.Y += 1 * speed;
            }

            float turningSpeed = 0.1f;
            Velocity = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
            //turning Lag for X
            if (AimDirection.X < GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X)
            {
                if (AimDirection.X - GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X < -turningSpeed)
                {
                    AimDirection.X += turningSpeed;
                }
                else
                {
                    AimDirection.X = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                }
            }
            else
            {
                if (AimDirection.X - GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > turningSpeed)
                {
                    AimDirection.X -= turningSpeed;
                }
                else
                {
                    AimDirection.X = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                }
            }
            //Turning lag for Y
            if (AimDirection.Y < GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y)
            {
                if (AimDirection.Y - GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < -turningSpeed)
                {
                    AimDirection.Y += turningSpeed;
                }
                else
                {
                    AimDirection.Y = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;
                }
            }
            else
            {
                if (AimDirection.Y - GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > turningSpeed)
                {
                    AimDirection.Y -= turningSpeed;
                }
                else
                {
                    AimDirection.Y = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;
                }
            }

            float tmpfloat = 1.0f;

            if (!double.IsNaN(-Math.PI / 2 + -Math.Asin(AimDirection.Y / (Math.Sqrt((AimDirection.X * AimDirection.X) + (AimDirection.Y * AimDirection.Y))))) && isAlive == true)
            {
                if (AimDirection.X < 0)
                    rotation = -(-Math.PI / 2 + -Math.Asin(AimDirection.Y / (Math.Sqrt((AimDirection.X * AimDirection.X) + (AimDirection.Y * AimDirection.Y)))));
                else
                    rotation = -Math.PI / 2 + -Math.Asin(AimDirection.Y / (Math.Sqrt((AimDirection.X * AimDirection.X) + (AimDirection.Y * AimDirection.Y))));
                tmpfloat = 0.5F;
            }
            else if (!double.IsNaN(-Math.PI / 2 + -Math.Asin(Velocity.Y / (Math.Sqrt((Velocity.X * Velocity.X) + (Velocity.Y * Velocity.Y))))) && isAlive == true)
            {
                if (Velocity.X < 0)
                    rotation = -(-Math.PI / 2 + -Math.Asin(Velocity.Y / (Math.Sqrt((Velocity.X * Velocity.X) + (Velocity.Y * Velocity.Y)))));
                else
                    rotation = -Math.PI / 2 + -Math.Asin(Velocity.Y / (Math.Sqrt((Velocity.X * Velocity.X) + (Velocity.Y * Velocity.Y))));
            }

            Vector2 tmpPosition = Position;
            Vector2 tmprrp = rpp;
            //Vector2 diff = new Vector2(0, 0); diff = Vector2.Subtract(Velocity, AimDirection);
            //float diffDouble = 1 - ((float)Math.Round(Math.Sqrt((diff.X * diff.X) + (diff.Y * diff.Y)),4)/2);

            Position.X += ((Velocity.X * speed) * tmpfloat);
            Position.Y -= ((Velocity.Y * speed) * tmpfloat);
            rpp.X += ((Velocity.X * speed) * tmpfloat);
            rpp.Y -= ((Velocity.Y * speed) * tmpfloat);

            if (CheckCollision(zombies))
            {
                Position = tmpPosition;
                rpp = tmprrp;
            }

            //if (double.IsNaN(rotation)) rotation = 0;

            return rpp;
        }

        public void TakeDamage(int howMuch, Zombie whichZombie)
        {

            currentHealth -= howMuch;
            tookDamage = true;
            canMove = false;
            moveTimer = new Timer();
            moveTimer.StartTimer(0, 20);
            if (currentHealth <= 0)
            {
                isAlive = false;
            }
        }

        public bool CheckCollision(Zombie[] allTheZombies)
        {
            for (int i = 0; i < allTheZombies.Length; i++)
            {
                if (allTheZombies[i] != null && BoundingBox.IntersectsWith(allTheZombies[i].BoundingBox) && allTheZombies[i].isAlive)
                {
                    return true;
                }
            }
            return false;
        }

        public void FireBullet(Projectile[] bullets)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i] == null)
                {
                    bullets[i] = new Projectile(new ParticleEffect(0, new Vector2(Position.X - weapons[currentWeapon].bulletSize / 2 + ((weapons[currentWeapon].bulletSize * 2) / 2 * (float)Math.Cos(rotation + (Math.PI / 2))) * weapons[currentWeapon].particleDisplacement, Position.Y - weapons[currentWeapon].bulletSize / 2 + ((weapons[currentWeapon].bulletSize * 2) / 2 * (float)Math.Sin(rotation + (Math.PI / 2))) * weapons[currentWeapon].particleDisplacement), 24, new Vector2((float)Math.Sin(rotation) * -3, (float)Math.Cos(rotation) * 3), weapons[currentWeapon].bulletSize, 1, this, null, Microsoft.Xna.Framework.Color.White),
                        new Vector2(Position.X - weapons[currentWeapon].bulletSize / 2 + ((weapons[currentWeapon].bulletSize * 2) / 2 * (float)Math.Cos(rotation + (Math.PI / 2))), Position.Y - weapons[currentWeapon].bulletSize / 2 + ((weapons[currentWeapon].bulletSize * 2) / 2 * (float)Math.Sin(rotation + (Math.PI / 2)))),
                        new Vector2(weapons[currentWeapon].velocity * (float)Math.Cos(rotation + (Math.PI / 2)), weapons[currentWeapon].velocity * (float)Math.Sin(rotation + (Math.PI / 2))), weapons[currentWeapon].bulletLifetime, 1,
                        new RectangleF(Position.X - weapons[currentWeapon].bulletSize / 2 + ((weapons[currentWeapon].bulletSize * 2) / 2 * (float)Math.Cos(rotation + (Math.PI / 2))), Position.Y - weapons[currentWeapon].bulletSize / 2 + ((weapons[currentWeapon].bulletSize * 2) / 2 * (float)Math.Sin(rotation + (Math.PI / 2))), weapons[currentWeapon].bulletSize, weapons[currentWeapon].bulletSize), weapons[currentWeapon]);
                    isShooting = true;
                    frame = (3 - currentWeapon);
                    break;
                }
            }
        }

        public void UpdateFrame()
        {
            if (Velocity.X != 0 || Velocity.Y != 0 && frame != running.GetLength(currentWeapon) && canMove == true)
                frame += 1;
            else if (isShooting == true && frame != ((shooting[currentWeapon].Length - 1)*3))
                frame += 1;

            if (isShooting == true)
                texture = shooting[currentWeapon][frame / (3 - currentWeapon)];
            else
                texture = running[currentWeapon, frame / (3 - currentWeapon)];

            if (frame >= (shooting[currentWeapon].Length - 1) * (3 - currentWeapon))
            {
                frame = (3 - currentWeapon);
                if (isShooting == true) isShooting = false;
            }

            weaponSwitchTimer.Tick(1);

        }

        public void Draw(SpriteBatch spriteBatch, float spx, float spy)
        {
            Vector2 newp;
            newp.X = Position.X - spx;
            newp.Y = Position.Y - spy;
            Microsoft.Xna.Framework.Rectangle rec = new Microsoft.Xna.Framework.Rectangle((int)newp.X, (int)newp.Y, 128, 128);
            spriteBatch.Draw(texture, rec, null, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
        }

    }
}
