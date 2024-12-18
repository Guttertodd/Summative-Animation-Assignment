using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Summative_Animation_Assignment
{
    enum Screen
    {
        Intro,
        RaceTrack, 
        End
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        SpriteFont textFont;

        Texture2D introTexture, midTexture, endTexture, runnerOneTexture, runnerTwoTexture;

        Rectangle runnerOneRect, runnerTwoRect;

        Vector2 runnerOneSpeed, runnerTwoSpeed;

        Screen screen;

        int finishLineX = 650;

        MouseState mouseState, previousMouseState;

      

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            runnerOneRect = new Rectangle(50,50,100,100);
            runnerOneSpeed = new Vector2(2,2);

            runnerTwoRect = new Rectangle(75, 200, 150, 150);
            runnerTwoSpeed = new Vector2(2, 2);


            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();


            screen = Screen.Intro;

            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            introTexture = Content.Load<Texture2D>("Intro");
            textFont = Content.Load<SpriteFont>("File");
            midTexture = Content.Load<Texture2D>("track");
            endTexture = Content.Load<Texture2D>("endscreen");
            runnerOneTexture = Content.Load<Texture2D>("running");
            runnerTwoTexture = Content.Load<Texture2D>("running2");
        }

        protected override void Update(GameTime gameTime)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)

                    screen = Screen.RaceTrack;
            }
            else if (screen == Screen.RaceTrack)
            {
                if (mouseState.RightButton == ButtonState.Pressed)
                    screen = Screen.End;


                runnerOneRect.X += (int)runnerOneSpeed.X;
                if (runnerOneRect.Right > window.Width || runnerOneRect.Left < 0)
                {
                    runnerOneSpeed.X *= 1;
                }

                runnerTwoRect.X += (int)runnerTwoSpeed.X;
                if (runnerTwoRect.Right > window.Width || runnerTwoRect.Left < 0)
                {
                    runnerTwoSpeed.X *= 1;
                }
            }
            if (screen == Screen.End)
            {
                if (mouseState.RightButton == ButtonState.Pressed)
                    screen = Screen.End;
            }
            
            if (runnerOneRect.X >= finishLineX ||runnerTwoRect.X >= finishLineX)
            {
                screen = Screen.End;
            }
            

           
        }
           


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introTexture, window, Color.White);
                _spriteBatch.DrawString(textFont, "Left Click to Continue to the Game!", new Vector2(165, 25), Color.Black);

            }

            else if (screen == Screen.RaceTrack)
            {
                _spriteBatch.Draw(midTexture, window, Color.White);

                _spriteBatch.Draw(runnerOneTexture, runnerOneRect, Color.White);
                _spriteBatch.Draw(runnerTwoTexture, runnerTwoRect, Color.White);

            }

            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(endTexture, window, Color.White);
                _spriteBatch.DrawString(textFont, "Congratulations! You Won The Race!", new Vector2(145, 55), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
