using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class Laser : Sprite
    {
        TimeSpan current = TimeSpan.Zero;
        TimeSpan delay;
       
        public Laser(Vector2 pos, Vector2 scale, Texture2D image, Color color, int laserdelay) : base(pos, scale, image, color)//take in the amount to change scale by
        {
            delay = TimeSpan.FromMilliseconds(laserdelay);
        }
        //update function that calls the increase size every time the time is reached
        public void Update(GameTime gametime)
        {
            current += gametime.ElapsedGameTime;
            if (current >= delay)
            {
                current = TimeSpan.Zero;
                IncreaseSize();
            }

        }
        public void IncreaseSize()
        {
            Scale = new Vector2(Scale.X, Scale.Y + 2.4f);
        }

        //reset scale function
    }
}
