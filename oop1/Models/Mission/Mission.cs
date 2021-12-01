using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            while (true)
            {
                IAstronaut currentAstronaut = astronauts.FirstOrDefault(a => a.CanBreath);

                if (currentAstronaut is null)
                {
                    break;
                }

                string item = planet.Items.FirstOrDefault();
                if (item is null)
                {
                    break;
                }

                currentAstronaut.Bag.Items.Add(item);
                currentAstronaut.Breath();
                planet.Items.Remove(item);
            }
        }
    }
}
