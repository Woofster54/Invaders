using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class Enemy : Sprite
    {
        TimeSpan delay = TimeSpan.FromMilliseconds(500);
        TimeSpan current = TimeSpan.Zero;
        public bool IsMovingRight = true;
        public bool GoDown = false;
        public bool IsHit = false;
        public Enemy(Vector2 pos, Vector2 scale, Texture2D image, Color color) : base(pos, scale, image, color)
        {
        }
        public void Update(GameTime gametime, GraphicsDevice gfx)
        {
           
            current += gametime.ElapsedGameTime;
            if (current >= delay)
            {
                current = TimeSpan.Zero;
                //if (Pos.X + Scale >= gfx.Viewport.Width)
                //{
                //    IsMovingRight = false;
                //}
                //else if (Pos.X <= 0)
                //{
                //    IsMovingRight = true;
                //}
                if (GoDown == true)
                {
                    Pos = new Vector2(Pos.X, Pos.Y + 25);
                    if (IsMovingRight == false)
                    {
                        IsMovingRight = true;
                    }
                    else
                    {
                        IsMovingRight = false;
                    }
                    GoDown = false;
                }
                else if (IsMovingRight == true)
                {
                    Pos = new Vector2(Pos.X + 15, Pos.Y);
                }
                else if (IsMovingRight == false && !IsHit)
                {
                    Pos = new Vector2(Pos.X - 15, Pos.Y);
                }
            }
           
        }
    }
}
