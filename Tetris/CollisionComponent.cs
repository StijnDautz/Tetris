using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public class CollisionComponent
    {
        //collisionmap
        public static int collisionMapHeight;
        public static BitArray collisionMap;
        public static Vector2 collisionMapPosition;

        //collisionStructure
        public BitArray structure;
        public int height;
        public int width;

        //position in GameMode related collisionStructure
        public int position;

        public CollisionComponent(BitArray collisionstructure, int pos, int h)
        {
            structure = collisionstructure;
            position = pos;
            height = h;
            addCollisionCompToMap();
        }

        public int MovementHandler(int positionChange)
        {
            //if everything is succesfull return 0
            int returnValue = 0;

            //check if positionchange is possible
            if(positionChangePossible(positionChange))
            {
                //create new CollisionComponent on new position
                CollisionComponent newcomp = new CollisionComponent(structure, position + positionChange, height);

                //check if newcomp collides with other actors except for newcomp's original CollisionComponent
                for(int i = 0; i < structure.Length; i++)
                {
                    bool collisionMapOldValue = collisionMap[getCollisionMapPosition(i)];
                    bool collisionMapNewValue = collisionMap[newcomp.getCollisionMapPosition(i)];
                    if(collisionMapNewValue == true && collisionMapOldValue == false && newcomp.structure[i] == true)
                    {
                        returnValue = 2;
                        break;
                    }
                }

                //if returnvalue is still 0; if everything went right, remove old CollisionComponent from CollisionMap
                if (returnValue == 0)
                {
                    for (int i = 0; i < structure.Length; i++)
                    {
                        if (collisionMap[getCollisionMapPosition(i)] == true && structure[i] == true)
                        {
                            collisionMap[getCollisionMapPosition(i)] = false;
                        }
                    }
                    //add new CollisionComponent to CollisionMap
                    newcomp.addCollisionCompToMap();
                }

                //change position variable
                position += positionChange;

            }
            else { returnValue = 1; }

            return returnValue;
        }

        public int RotationHandler(BitArray newStructure)
        {

        }

        /*
         * Checks if collisionComponent collides with collisionmap
         */
        public bool CollidesWithMap()
        {
            bool collidesWith = false;

            for(int i = 0; i < structure.Length; i++)
            {
                bool posToCheck = collisionMap[getCollisionMapPosition(i)];
                if (posToCheck == true && structure[i] == true)
                {
                    collidesWith = true;
                    break;
                }
            }
            return collidesWith;
        }

        private int getCollisionMapPosition(int pos)
        {
            return  (int)Math.Floor((double)pos / (double)height) * collisionMapHeight + pos % height + position;
        }

        public Vector2 getCollisionMapVectorPosition(int pos)
        {
            return new Vector2((float)Math.Floor((double)((pos + position) / height)), (float)((pos + position) % height));
        }

        public bool positionChangePossible(int positionChange)
        {
            int pos = position + positionChange;
            if (pos > 0 && pos < structure.Length && (Math.Floor((double)pos / collisionMapHeight) == Math.Floor((double)position / collisionMapHeight)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void addCollisionCompToMap()
        {
            for(int i = 0; i < structure.Length; i++)
            {
                if(structure[i] == true)
                {
                    collisionMap[getCollisionMapPosition(i)] = true;
                }
            }
        }
    }
}