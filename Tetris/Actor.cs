using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public class Actor
    {
        public CollisionComponent collisionComponent;
        public TextureComponent textureComponent;
        public int height;
        public int width;

        public enum direction
        { LEFT, RIGHT, FORWARD, BACKWARDS};

        public Actor( int w, int h, TextureComponent texturecomp, CollisionComponent collisioncomp)
        {
            textureComponent = texturecomp; 
            collisionComponent = collisioncomp;
            width = w;
            height = h;
        }

        public static BitArray GenerateStructure(int width, int height, bool emptyorfilled)
        {
            BitArray s = new BitArray(width * height);
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = emptyorfilled;
            }
            return s;
        }

        /*************************************************************
         * MOVEMENT
         ************************************************************/

        public int Move(direction d)
        {
            int positionChange = 0;

            //determine position change
            switch (d)
            {
                case direction.LEFT: positionChange = -CollisionComponent.collisionMapHeight;
                    break;
                case direction.RIGHT: positionChange = CollisionComponent.collisionMapHeight;
                    break;
                case direction.FORWARD: positionChange = 1;
                    break;
                case direction.BACKWARDS: positionChange = -1;
                    break;
            }

            /*
             * Handles collision and gives feedback on what happened
             * [0] everything went right
             * [1] positionchange isn't possible
             * [2] the new CollisionComponent collides with other actors on the collisionMap
             */
            int result = collisionComponent.MovementHandler(positionChange);

            return result;
        }

        public bool Rotate(direction d)
        {
            //create newStructure
            BitArray newStructure = new BitArray(collisionComponent.height * collisionComponent.height);

            //Change structure based on rotation direction
            if (d == direction.LEFT)
            {
                int startingPoint = collisionComponent.height - 1;
                while(startingPoint > -1)
                    { 
                    for (int i = 0; i < collisionComponent.height; i++)
                    {
                        newStructure[i] = collisionComponent.structure[i * collisionComponent.height + startingPoint];
                    }
                    startingPoint--;
                }
            }
            else if(d == direction.RIGHT)
            {
                int startingPoint = collisionComponent.height  * collisionComponent.height - 1;
                while (startingPoint < collisionComponent.structure.Length)
                {
                    for (int i = 0; i < collisionComponent.height; i++)
                    {
                        newStructure[i] = collisionComponent.structure[startingPoint - i * collisionComponent.height];
                    }
                    startingPoint++;
                }
            }
            //return true if succesfull else false 
            //this depends on CollisionHandler.CollidesWith
            return true;
        }
    }
}