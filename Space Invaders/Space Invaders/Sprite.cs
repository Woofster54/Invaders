using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class Sprite
    {
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Pos.X, (int)Pos.Y, (int)(Image.Width * Scale.X), (int)(Image.Height * Scale.Y));
            }
            

        }
        public Sprite(Vector2 pos, Vector2 scale, Texture2D image, Color color)
        {
            Pos = pos;
            Scale = scale;
            Image = image;
            Color = color;
        }
        
        public Vector2 Pos { get; set; }
        public Vector2 Scale { get; set; }
        public Texture2D Image { get; set; }
        public Color Color { get; set; }
        public void Draw(SpriteBatch sprite)
        {
            
            sprite.Draw(Image, Hitbox, Color);
        }
        
    }
}
