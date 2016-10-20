using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tetris
{
    public class ContentLibrary
    {
        public static Texture2D block1;
        public static Texture2D block2;
        public static Texture2D block3;
        public static Texture2D block4;
        public static Texture2D gridBlock;

        public ContentLibrary(ContentManager content)
        {
            block1 = content.Load<Texture2D>("Block1");
            block2 = content.Load<Texture2D>("Block2");
            block3 = content.Load<Texture2D>("Block3");
            block4 = content.Load<Texture2D>("Block4");
            gridBlock = content.Load<Texture2D>("GridBlock");
        }
    }
}
