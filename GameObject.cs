using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Game
{
    class GameObject
    {
            //Ints
            public int lockedframe = 0;
            public int direction = 1;
            public int frame = 0;
            //Floats
            public float speed;
            public double rotation;
            //Booleans
            public bool jumped = false;
            public bool[] moving = new bool[4];
            public bool isShooting;
            public bool isAlive;
            //Texture2Ds
            public Texture2D texture;
            public Texture2D[] running = new Texture2D[80];
            public Texture2D[] jumping = new Texture2D[21];
            public Texture2D[] shooting = new Texture2D[18];
            //Vector2s
            public Vector2 Position;
            public Vector2 Velocity;
            //Rectangles
            public Rectangle bb;
            public Rectangle BoundingBox
            {
                get
                {
                    if (this.texture.Bounds.Width > 128)
                    {
                        return new Rectangle(
                            (int)Position.X + bb.X,
                            (int)Position.Y + bb.Y,
                            128 - bb.Width,
                            128 - bb.Height);
                    }
                    else
                    {
                        return new Rectangle(
                            (int)Position.X + bb.X,
                            (int)Position.Y + bb.Y,
                            texture.Width - bb.Width,
                            texture.Height - bb.Height);
                    }
                }
            }

            public GameObject(Texture2D texture, Vector2 position)
            {
                this.texture = texture;
                this.Position = position;
            }

            public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
            {
                this.texture = texture;
                this.Position = position;
                this.Velocity = velocity;
            }

            public void Draw(SpriteBatch spriteBatch, float spx, float spy)
            {
                Vector2 newp;
                newp.X = Position.X - spx;
                newp.Y = Position.Y - spy;
                Rectangle rec = new Rectangle((int)newp.X, (int)newp.Y, 128, 128);
                spriteBatch.Draw(texture, rec, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
            }
        }
}
