using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public class GameMode
    { 
        protected SpriteFont font;
        public List<Actor> actorList = new List<Actor>();
        protected GameTime gameTime = new GameTime();
        
        public enum gameModes { MAINMENU, PLAYING, QUIT };
        protected int tileSize;

        protected GameMode()
        {
            //set default variables
            tileSize = 32;            
        }

        protected virtual void init()
        {
            
        }

        /*
         * Handles the GameMode's logic
         */
        public virtual void Update(GameTime gameTime)
        {
            HandleInput();
        }
 
        /*
         * Draws all textures in textureList on the screen
         */
        public virtual void Draw(SpriteBatch spritebatch)
        {
            List<Vector2> positionTestList = new List<Vector2>();

            spritebatch.Begin();
            //Draw all textures in objectList on the screen
            foreach (Actor obj in actorList)
            {
                Vector2 pos = obj.textureComponent.position;

                //check if the texture should be drawed on every true point in obj.structure
                if (obj.textureComponent.structure != null)
                {
                    if(obj.collisionComponent != null)
                    {
                        for (int i = 0; i < obj.textureComponent.structure.Length; i++)
                        {
                            //check if point at structure i == true
                            if (obj.textureComponent.structure[i] == true)
                            {
                                /* if so draw the texture on the calculated position
                                 * position.X++ when point is in column next to the starter column
                                 * position.Y++ point/structure.getHeight() when point/structure.getHeight() == 1
                                 */
                                Vector2 drawPosition = obj.collisionComponent.getCollisionMapVectorPosition(i) * tileSize + CollisionComponent.collisionMapPosition;
                                spritebatch.Draw(obj.textureComponent.texture, drawPosition, Color.White);
                            }
                        }
                    }
                    else
                    {

                    }

                }
                else
                {
                    spritebatch.Draw(obj.textureComponent.texture, pos, Color.White);
                }
            }
            spritebatch.End();
        }
    
        protected virtual void HandleInput()
        {
            
        }

         /*
         * Performs events based on playerinput
         */      
        protected void DrawText(string text, Vector2 location, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawString(font, text, location, color);
        }

        public static GameMode setGameMode(gameModes gamemode)
        {
            GameMode gameMode;
            switch (gamemode)
            {
                case gameModes.PLAYING: gameMode = new GameModes.PlayingGameMode();
                    break;
                default: throw new Exception("gameMode does not exist");
            }

            //Initialize the new GameMode
            gameMode.init();
            return gameMode;
        }

        /*
         * Adds actor to actorList if possible and returns a boolean to determine what happened in this function
         */
        protected bool AddActor(Actor actor)
        {
            if(!actor.collisionComponent.CollidesWithMap())
            {
                actorList.Add(actor);
                return true;
            }
            else { return true; }
        }
    }
}