using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Space_Invaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D Enemy1;
        Texture2D Enemy2;
        Texture2D Enemy3;
        List<Enemy> enemies = new List<Enemy>();
        int DeadCount;
        int ChosenShot;

        Texture2D player;
        Player playership;
        int Lives = 3;
        bool PlayerHit = false;

        Texture2D PlayerShot;
        Texture2D EnemyShot;
        Projectile shot;
        Projectile EShot;
        Projectile BossShot;

        Texture2D BossBullet;


        SoundEffect Shot;
        SoundEffect Explosion;

        Enemy Boss;
        Texture2D BigBoy;
        int bosshp = 50;
        bool activatelaser = false;

        bool ShootBossBullet = false;

        Texture2D laserimage;
        SoundEffect EnemySound;

        SpriteFont LivesFont;
        SpriteFont font;
        SpriteFont WinFont;

        Laser laser;

        Random rnd = new Random();

        TimeSpan delay1;
        TimeSpan BulletDelay;


        TimeSpan delay = TimeSpan.FromMilliseconds(5000);
        TimeSpan current = TimeSpan.Zero;
        TimeSpan BulletCurrent = TimeSpan.Zero;
        TimeSpan LaserCurrent = TimeSpan.Zero;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1100;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Enemy1 = Content.Load<Texture2D>("Enemy1");
            Enemy2 = Content.Load<Texture2D>("Enemy2");
            Enemy3 = Content.Load<Texture2D>("Enemy3");

            player = Content.Load<Texture2D>("PlayerShip");

            PlayerShot = Content.Load<Texture2D>("PlayerShot");
            EnemyShot = Content.Load<Texture2D>("projectile-green");
            BossBullet = Content.Load<Texture2D>("BossProjectile");

            font = Content.Load<SpriteFont>("Font");
            LivesFont = Content.Load<SpriteFont>("LifeFont");
            WinFont = Content.Load<SpriteFont>("WIN");

            laserimage = Content.Load<Texture2D>("LASER");

            BigBoy = Content.Load<Texture2D>("BOSS2.0");

            Explosion = Content.Load<SoundEffect>("Explosion");
            Shot = Content.Load<SoundEffect>("Blaster");
            EnemySound = Content.Load<SoundEffect>("EnemyShotSound");


            playership = new Player(new Vector2(550, GraphicsDevice.Viewport.Height - 50), Vector2.One, player, Color.LightSteelBlue);
            Boss = new Enemy(new Vector2(350, 0), new Vector2(3.7f, 3.7f), BigBoy, Color.DarkRed);
            laser = new Laser(new Vector2(Boss.Pos.X + 70, Boss.Pos.Y + 300), Vector2.One, laserimage, Color.White, 200);

            delay = TimeSpan.FromSeconds(5);
            BulletDelay = TimeSpan.FromSeconds(10);
            delay1 = TimeSpan.FromSeconds(4);

            int startx = 25;
            int starty = 35;
            int gap = 15;
            int ygap = 65;
            int count = 0;
            for (int i = 0; i < 30; i++)
            {

                if (i != 0 && i % 10 == 0)
                {
                    starty += ygap;
                    startx = 25;
                    count++;

                }
                if (count == 0)
                {
                    enemies.Add(new Enemy(new Vector2(startx, starty), Vector2.One, Enemy1, Color.Lime));
                }
                else if (count == 1)
                {
                    enemies.Add(new Enemy(new Vector2(startx, starty), Vector2.One, Enemy2, Color.DarkGreen));
                }
                else
                {
                    enemies.Add(new Enemy(new Vector2(startx, starty), Vector2.One, Enemy3, Color.LawnGreen));
                }

                startx += gap + Enemy1.Width;
            }

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        void ResetGame()
        {

            enemies.Clear();
            DeadCount = 0;
            bosshp = 50;
            int startx = 25;
            int starty = 35;
            int gap = 15;
            int ygap = 65;
            int count = 0;
            for (int i = 0; i < 30; i++)
            {
                if (i != 0 && i % 10 == 0)
                {
                    starty += ygap;
                    startx = 25;
                    count++;

                }
                if (count == 0)
                {
                    enemies.Add(new Enemy(new Vector2(startx, starty), Vector2.One, Enemy1, Color.Lime));
                }
                else if (count == 1)
                {
                    enemies.Add(new Enemy(new Vector2(startx, starty), Vector2.One, Enemy2, Color.DarkGreen));
                }
                else
                {
                    enemies.Add(new Enemy(new Vector2(startx, starty), Vector2.One, Enemy3, Color.LawnGreen));
                }

                startx += gap + Enemy1.Width;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            bool EndOfRow = false;


            if (!PlayerHit)
            {
                //restart game
                if (Keyboard.GetState().IsKeyDown(Keys.Tab))
                {
                    ResetGame();
                }
                //skip to boss cheat
                if (Keyboard.GetState().IsKeyDown(Keys.N))
                {
                    foreach (var enemy in enemies)
                    {
                        enemy.IsHit = true;
                        DeadCount = 30;
                    }
                }
                //move left and right
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    playership.MoveLeft();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    playership.MoveRight(GraphicsDevice);
                }
                //move enemies as long as there are enemies
                if (DeadCount < 30)
                {
                    //check if enemy reaches end of row
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].IsMovingRight == true && enemies[i].Pos.X + enemies[i].Image.Width >= GraphicsDevice.Viewport.Width && enemies[i].IsHit == false)
                        {
                            EndOfRow = true;

                        }
                        else if (enemies[i].IsMovingRight == false && enemies[i].Pos.X - 5 <= 0)
                        {
                            EndOfRow = true;
                        }

                    }
                    //move enemies down
                    foreach (var enemy in enemies)
                    {
                        if (EndOfRow == true)
                        {
                            enemy.GoDown = true;

                        }
                        enemy.Update(gameTime, GraphicsDevice);
                    }


                    //enemy shooting
                    current += gameTime.ElapsedGameTime;
                    if (current >= delay)
                    {
                        ChosenShot = rnd.Next(enemies.Count);

                        while (enemies[ChosenShot].IsHit)
                        {
                            ChosenShot = rnd.Next(enemies.Count);
                        }
                        EnemySound.Play();
                        EShot = new Projectile(new Vector2(enemies[ChosenShot].Pos.X, enemies[ChosenShot].Pos.Y), Vector2.One, 8, EnemyShot, Color.White);
                        current = TimeSpan.Zero;


                    }
                }
                //If no bullet shoot
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && shot == null)
                {
                    Shot.Play();
                    shot = new Projectile(new Vector2(playership.Pos.X, playership.Pos.Y), Vector2.One, -8, PlayerShot, Color.White);
                }
                //if bullets collide, get rid of both bulllets
                if (shot != null && EShot != null && shot.Hitbox.Intersects(EShot.Hitbox) == true)
                {
                    shot = null;
                    EShot = null;
                }
                //if bullet reaches top of screen, get rid of bullet
                if (shot != null && (shot.Pos.Y <= 0))
                {
                    shot = null;
                }
                //if enemy is hit, then remove enemy
                for (int i = 0; i < enemies.Count; i++)
                {


                    if (shot != null && shot.Hitbox.Intersects(enemies[i].Hitbox) && enemies[i].IsHit == false)
                    {

                        shot = null;
                        enemies[i].IsHit = true;
                        DeadCount++;
                        Explosion.Play();
                    }
                }
                //if enemy shot hits player, remove life
                if (EShot != null && EShot.Hitbox.Intersects(playership.Hitbox))
                {
                    EShot = null;
                    Lives--;
                    PlayerHit = true;

                }
                //if shot is active, update it
                if (shot != null)
                {
                    shot.Update();
                }
                //if enemy shot is active, update it
                if (EShot != null)
                {
                    EShot.Update();
                }



                if (DeadCount == 30 && shot != null && shot.Hitbox.Intersects(Boss.Hitbox))
                {
                    shot = null;
                    bosshp--;
                }
                if (DeadCount == 30 && bosshp > 0)
                {
                    BulletCurrent += gameTime.ElapsedGameTime;
                    if (BulletCurrent >= BulletDelay)
                    {
                        ShootBossBullet = true;
                        BulletCurrent = TimeSpan.Zero;
                    }
                    if (ShootBossBullet == true)
                    {
                        BossShot = new Projectile(new Vector2(Boss.Pos.X - 15, Boss.Pos.Y), Vector2.One, 3, BossBullet, Color.White);
                    }

                    if (Boss.Pos.X + Boss.Hitbox.Width >= GraphicsDevice.Viewport.Width)
                    {
                        Boss.IsMovingRight = false;
                    }
                    else if (Boss.Pos.X <= 0)
                    {
                        Boss.IsMovingRight = true;
                    }

                    current += gameTime.ElapsedGameTime;
                    if (current >= delay)
                    {
                        activatelaser = true;
                        current = TimeSpan.Zero;
                    }
                    if (activatelaser == true)
                    {
                        laser.Pos = new Vector2(Boss.Pos.X + 65, Boss.Pos.Y + 300);
                        laser.Update(gameTime);
                      
                        LaserCurrent += gameTime.ElapsedGameTime;
                        if (LaserCurrent >= delay1 || laser.Scale.Y >= 20)
                        {
                            laser.ResetScale();
                            LaserCurrent = TimeSpan.Zero;
                            activatelaser = false;

                        }  
                        else if (playership.Hitbox.Intersects(laser.Hitbox))
                        {
                            PlayerHit = true;
                            Lives--;
                            LaserCurrent = TimeSpan.Zero;
                            activatelaser = false;
                            laser.ResetScale();
                        }
                    }



                    Boss.Update(gameTime, GraphicsDevice);
                }
            }
            if (Lives != 0 && PlayerHit == true && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                PlayerHit = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            GraphicsDevice.Clear(Color.Black);

            foreach (var enemy in enemies)
            {
                if (!enemy.IsHit)
                {
                    enemy.Draw(spriteBatch);
                }
            }
            // TODO: Add your drawing code here
            if (shot != null) shot.Draw(spriteBatch);
            spriteBatch.DrawString(LivesFont, $"Lives: {Lives}", new Vector2(10, 11), Color.White);


            if (!PlayerHit)
            {

                if (EShot != null) EShot.Draw(spriteBatch);

            }
            else if (PlayerHit && Lives != 0)
            {
                spriteBatch.DrawString(font, "Press Enter To Continue", new Vector2(425, 11), Color.Red);
            }
            else if (PlayerHit && Lives == 0)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(font, "Game Over", new Vector2(425, 11), Color.Red);
            }
            if (DeadCount == 30)
            {
                delay = TimeSpan.FromSeconds(5);
                
                if (ShootBossBullet == true)
                {
                    BossShot.Draw(spriteBatch);
                }
                if (bosshp > 0)
                {
                    Boss.Draw(spriteBatch);

                    spriteBatch.DrawString(font, $"Boss Hit Points:{bosshp}", new Vector2(25, GraphicsDevice.Viewport.Height - 30), Color.DarkRed);
                }
                else
                {
                    spriteBatch.DrawString(WinFont, "YOU WIN!", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
                }
                if (activatelaser == true)
                {
                    laser.Draw(spriteBatch);
                }
            }
            spriteBatch.DrawString(font, $" current: {current.Seconds}, laser current: {LaserCurrent.Seconds}\n scale: {laser.Scale}", new Vector2(50, 0), Color.Chartreuse);
            playership.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
