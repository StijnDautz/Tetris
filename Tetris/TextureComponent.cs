using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public class TextureComponent
    {

        public Texture2D texture;
        public BitArray structure;
        public Vector2 position;

        public TextureComponent(Texture2D tex, BitArray s, Vector2 screenpos)
        {
            texture = tex;
            structure = s;
            position = screenpos;
        }
    }
}
