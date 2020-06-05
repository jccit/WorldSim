using System;

namespace WorldSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Renderer renderer = new Renderer(2000, 2000);

            Country newCountry = new Country();
            newCountry.GenerateTowns();

            renderer.RenderCountry(newCountry);
            renderer.Save();
        }
    }
}
