using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;

namespace Space_Game
{
    class Zombie
    {
        //Ints
        public int lockedframe = 0;
        public int direction = 1;
        public int frame = 0;
        public Timer zombieTimer = new Timer();
        public int health;

        //Floats
        public float speed;
        public double rotation = 0f;

        public double tmpRotation;
        public float slowDown = 1f;

        //Booleans
        public bool jumped = false;
        public bool canMove = true;
        public bool isShooting;
        public bool isAlive = true;

        //Texture2Ds
        public Texture2D texture;
        public Texture2D[] running;
        public Texture2D[] dying;
        //Vector2s
        public Vector2 Position;
        public float z = 32;
        public Vector2 Velocity;
        //Rectangles
        public RectangleF bb;
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

        public Zombie(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            Position = position;
        }

        public Zombie(Texture2D texture, Vector2 position, Vector2 velocity, int runningFrames, int dyingFrames, int health)
        {
            this.texture = texture;
            running = new Texture2D[runningFrames];
            dying = new Texture2D[dyingFrames];
            Position = position;
            Velocity = velocity;
            this.health = health;
        }

        public bool CheckCollision(Player myPlayer, Zombie[] allTheZombies)
        {
            if (BoundingBox.IntersectsWith(myPlayer.BoundingBox)  && myPlayer.isAlive == true) {
                if (!zombieTimer.on)
                {
                    myPlayer.TakeDamage(25,this);
                    myPlayer.tmpRotation = (Math.Atan2(myPlayer.Position.Y - Position.Y, myPlayer.Position.X - Position.X) - (Math.PI / 2));
                    zombieTimer.StartTimer(0, 60);

                }
                return true;
            }
            for (int i = 0; i < allTheZombies.Length; i++)
            { 
                if(allTheZombies[i] != null && allTheZombies[i] != this && BoundingBox.IntersectsWith(allTheZombies[i].BoundingBox) && allTheZombies[i].isAlive)
                {
                    tmpRotation = (-Math.Atan2(allTheZombies[i].Position.Y - Position.Y, allTheZombies[i].Position.X - Position.X) - (Math.PI / 2));
                    return true;
                }
            }
            return false;
        }

        public void Move(Player myPlayer, Zombie[] allTheZombies)
        {
            if (isAlive)
            {
                if (zombieTimer != null && zombieTimer.on) zombieTimer.Tick(1);
                Vector2 tmpPosition = Position;
                double newRotation = rotation;
                texture = running[frame];

                rotation = (Math.Atan2(myPlayer.Position.Y - Position.Y, myPlayer.Position.X - Position.X) - (Math.PI / 2));
                Position.X += (float)Math.Cos(rotation + (Math.PI / 2)) * Velocity.X * slowDown;
                Position.Y += (float)Math.Sin(rotation + (Math.PI / 2)) * Velocity.Y * slowDown;
                if (slowDown < 1f) slowDown += 0.02f;

                if (CheckCollision(myPlayer, allTheZombies))
                {
                    Position = tmpPosition;
                    rotation = newRotation;
                    if (!BoundingBox.IntersectsWith(myPlayer.BoundingBox))
                    {
                        Position.X += (float)Math.Cos(tmpRotation + (Math.PI / 2)) * Velocity.X;
                        Position.Y += (float)Math.Sin(tmpRotation + (Math.PI / 2)) * Velocity.Y;
                    }

                }
                if (frame < 72)
                    frame += 1;
                else
                    frame = 1;
            }
            else
            {

                Position += Velocity;
                Velocity *= 0.85f;
                if (frame < 20)
                {
                    texture = dying[frame];
                    z -= 1.5f;
                    frame += 1;
                }
                else Velocity *= 0f;
            }
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
