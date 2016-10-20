using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * CollisionMap Class toevoegen om meerdere collisionmaps toe te voegen. Dit zou handig kunnen zijn wanneer men een wereld 2d wereld wil bouwen met een gelaagde hoogte opbouw
 * elke hoogtelaag heeft dan een eigen collisionmap. Dat was voor deze game niet nodig dus heb ik dit weggelaten, maar zou gemakkelijk toegevoegd kunnen worden.
 */

namespace Tetris
{
    public class TetrisGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GameMode gameMode;
        public static Random random = new Random();

        public TetrisGame()
        {
            graphics = new GraphicsDeviceManager(this);

            //set screen height and width equal to to monitors dimensions
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            //set Content folder
            Content.RootDirectory = "Content";
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

            /* 
             * Set the gameMode, in which Actors using content is creadted
             * Init is called in this func at last, which adds all actors to actorList that should be shown when playing
             */
            gameMode = GameMode.setGameMode(GameMode.gameModes.PLAYING);
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