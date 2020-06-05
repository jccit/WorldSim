using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace WorldSim
{
    class Renderer
    {
        private Bitmap image;
        private int width;
        private int height;

        public Renderer(int width, int height)
        {
            this.width = width;
            this.height = height;

            image = new Bitmap(width, height);
        }

        private void SetPixel(int x, int y, Color colour)
        {
            if (x >= width) x = width - 1;
            if (y >= height) y = height - 1;
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            image.SetPixel(x, y, colour);
        }

        public void RenderCountry(Country country)
        {
            foreach (Town countryTown in country.Towns)
            {
                int x = (int)Math.Round(countryTown.Position.X) + width / 2;
                int y = (int)Math.Round(countryTown.Position.Y) + height / 2;

                Color colour = Color.Black;

                if (countryTown.Type == Town.TownType.City)
                {
                    colour = Color.Red;
                }

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        SetPixel(x + i, y + j, colour);
                    }
                }
            }
        }

        public void Save()
        {
            image.Save("out.png", ImageFormat.Png);
        }
    }
}
