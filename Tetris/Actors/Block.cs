using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Tetris
{
    public class Block : Actor
    {
        private static Texture2D[] _textures = { ContentLibrary.block1, ContentLibrary.block2, ContentLibrary.block3, ContentLibrary.block4 };
        private int _structureSize;
        private int _size;

        public Block(int width, int height, TextureComponent texturecomponent, CollisionComponent collisioncomponent) : base(width, height, texturecomponent, collisioncomponent)
        {
            if(width != height)
            {
                throw new Exception("Height of Block has to be equal to its width");
            }
            _size = width;
            _structureSize = _size * _size;
        }

        /*
        * Generates a blockStructure and returns a block
        */
        public static Block GenerateBlock(int size, int gridSize)
        {
            int structureSize = size * size;
            //Create a BitArray of size blockStructure and set every bit equal to 0
            BitArray blockStructure = GenerateStructure(size, size, false);

            /* Create a list of points that will be set to true inside the blockstructure
             * All points that are true will be pieces of the block 
             * at those who are false no block piece will exist*/
            List<int> blockPoints = new List<int>();

            /*Create a list of possible points to be set to true
             * Everytime a blockPoints.Count++ more possible points to be set to true will exist
             * When a new point of possiblePoints is added to blockPoints this possiblePoint will be removed from possiblePoints to make sure a point is not set twice to true*/
            List<int> possiblePoints = new List<int>();

            //Create a random starting point inside the blockStructure
            int start = size * size / 2;

            //Add the starting point to blockPoints
            blockPoints.Add(start);

            /*Check around the newest point added to blockPoints if:
             *  - the possible blockPoint is within the blockStructures boundaries
             *  - the possible blockPoint is next to the blockPoint (it might be at the top when the blockPoint is at the bottom and vise versa)
             *  - the possible blockPoint is false
             *If these are all true add the blockPoint to possiblePoints*/
            for (int i = 0; i < size; i++)
            {
                //set the newest point added  to blockPoints to 1
                blockStructure[blockPoints[i]] = true;                
                
                //vertical
                if (blockPoints[i] - 1 >= 0 && blockPoints[i] % size != 0 && blockStructure[blockPoints[i] - 1] != true)
                {
                    possiblePoints.Add(blockPoints[i] - 1);
                }
                if(blockPoints[i] + 1 < structureSize && (blockPoints[i] + 1) % size != 0 && blockStructure[blockPoints[i] + 1] != true)
                {
                        possiblePoints.Add(blockPoints[i] + 1);
                }       

                //horizontal
                if(blockPoints[i] - size >= 0 && blockStructure[blockPoints[i] - size] != true)
                {
                        possiblePoints.Add(blockPoints[i] - size);           
                }
                if(blockPoints[i] + size < structureSize && blockStructure[blockPoints[i] + size] != true)
                {
                        possiblePoints.Add(blockPoints[i] + size);
                }

                blockPoints.Add(possiblePoints[TetrisGame.random.Next(0, possiblePoints.Count - 1)]);

                //Remove the blockPoint added to blockPoints from possiblePoints, as we don't to set a blockPoint twice to true(we want a block of blockSize, not smaller)
                possiblePoints.Remove(blockPoints[i + 1]);
            }

            //Get a textureName from Block.textureNames to set for the Block
            Texture2D texture = _textures[TetrisGame.random.Next(0, _textures.Length)];

            //Create the block with the generated blockStructure and spawn it at in the center of the top of the grid
            Block block = new Block(size, size,
                new TextureComponent(texture, blockStructure, Vector2.Zero), 
                new CollisionComponent(blockStructure, 0, size));

            //return the block
            return block;
        }
    }
}
