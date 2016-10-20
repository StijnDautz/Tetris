using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;
using System.Collections.Generic;
using System;


namespace Tetris
{
    public class TetrisGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GameMode gameMode;
        public static Random random = new Random();

        private const int gridHeight = 20;
        private const int gridWidth = 12;
        
        public TetrisGame()
        {
            graphics = new GraphicsDeviceManager(this);

            //set Content folder
            Content.RootDirectory = "Content";

            //set the gameMode to start with
            gameMode = GameMode.setGameMode(GameMode.gameModes.PLAYING);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load all content in the contentlibrary class
            ContentLibrary contentlibrary = new ContentLibrary(Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //Update according to gameMode
            gameMode.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            //Draw the textures in the gameMode TextureList
            gameMode.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}