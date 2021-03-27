using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class Projectile : Sprite
    {
        
        public Projectile(Vector2 pos, Vector2 scale, int speed, Texture2D image, Color color) : base(pos, scale, image, color)
        {
            Speed = speed;
        }

        public int Speed { get; }

        public void Update()
        {
            Pos = new Vector2(Pos.X, Pos.Y + Speed);
        }


    }
}
