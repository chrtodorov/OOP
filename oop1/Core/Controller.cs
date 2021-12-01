using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private IRepository<IAstronaut> astronuatRepository;
        private IRepository<IPlanet> planetRepository;
        private IMission mission;
        private int exploredPlanets;

        public Controller()
        {
            astronuatRepository = new AstronautRepository();
            planetRepository = new PlanetRepository();
            mission = new Mission();
            exploredPlanets = 0;
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronautNew = null;
            string returnMessage = string.Empty;

            switch (type)
            {
                case nameof(Biologist):
                    astronautNew = new Biologist(astronautName);
                    returnMessage = string.Format(OutputMessages.AstronautAdded, type, astronautName);
                    break;

                case nameof(Geodesist):
                    astronautNew = new Geodesist(astronautName);
                    returnMessage = string.Format(OutputMessages.AstronautAdded, type, astronautName);
                    break;

                case nameof(Meteorologist):
                    astronautNew = new Meteorologist(astronautName);
                    returnMessage = string.Format(OutputMessages.AstronautAdded, type, astronautName);
                    break;

                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);

            }

            this.astronuatRepository.Add(astronautNew);

            return returnMessage;
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet newPlanet = new Planet(planetName, items);
            planetRepository.Add(newPlanet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            List<IAstronaut> astronautExplorers = astronuatRepository.Models.Where(a => a.Oxygen > 60).ToList();

            if (astronautExplorers.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IPlanet planet = planetRepository.FindByName(planetName);
            mission.Explore(planet, astronautExplorers);
            exploredPlanets += 1;

            //potential bug !! 
            IEnumerable<IAstronaut> astronautsDied = astronautExplorers.Where(a => a.CanBreath == false);
            return string.Format(OutputMessages.PlanetExplored, planetName, astronautsDied.Count());

        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{exploredPlanets} planets were explored!");
            sb.AppendLine($"Astronauts info:");

            foreach (IAstronaut astronaut in astronuatRepository.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                sb.AppendLine($"Bag items: " +
                    $"{(astronaut.Bag.Items.Count == 0 ? "none" : string.Join(", ", astronaut.Bag.Items))}");
            }

            return sb
                .ToString()
                .TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut existingAstronaut = astronuatRepository.FindByName(astronautName);

            if (existingAstronaut is null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }

            astronuatRepository.Remove(existingAstronaut);
            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
