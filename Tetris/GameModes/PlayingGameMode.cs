using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Tetris.GameModes
{
    public class PlayingGameMode : GameMode
    {
        private static int worldWidth = 12;
        private static int worldHeight = 20;
        private static int worldSize = worldWidth * worldHeight;
        bool test = true;

        //init actors
        public Actor grid;
        private Actor currentBlock;

        public PlayingGameMode() : base()
        {
            //set CollisionMap properties
            CollisionComponent.collisionMapHeight = worldHeight;
            CollisionComponent.collisionMap = Actor.GenerateStructure(worldWidth, worldHeight, false);
            CollisionComponent.collisionMapPosition = new Vector2(300, 600);

            //create grid actor
            grid = new Actor(worldWidth, worldHeight,
                new TextureComponent(ContentLibrary.gridBlock, Actor.GenerateStructure(worldWidth, worldHeight, true), Vector2.Zero), 
                new CollisionComponent(Actor.GenerateStructure(worldWidth, worldHeight, false), 0 , worldHeight));

            //set tilesize
            tileSize = 32;
        }

        protected override void init()
        {
            actorList.Add(grid);
            base.init();
        }

        public override void Draw(SpriteBatch spritebatch)
        {         
            base.Draw(spritebatch);
        }

        public override void Update(GameTime gameTime)
        {
            if(test == true)
            {
                currentBlock = Block.GenerateBlock(5, worldWidth);
                actorList.Add(currentBlock);
                test = false;
            }

            base.Update(gameTime);
        }

        protected override void HandleInput()
        {
            KeyboardState keyboardinput = Keyboard.GetState();


            if (keyboardinput.IsKeyDown(Keys.S))
            {
                currentBlock.Move(Actor.direction.FORWARD);
            }
            base.HandleInput();
        }
    }
}