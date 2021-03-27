using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class Player : Sprite
    {
        public Player(Vector2 pos, Vector2 scale, Texture2D image, Color color) : base(pos, scale, image, color)
        {
        }
        public void MoveLeft()
        {
            if (Pos.X > 0)
            {
                Pos = new Vector2(Pos.X - 5, Pos.Y);
            }
        }
        public void MoveRight(GraphicsDevice gfx)
        {
            if (Pos.X + Image.Width < gfx.Viewport.Width)
            {
                Pos = new Vector2(Pos.X + 5, Pos.Y);
            }
        }
    }
}
