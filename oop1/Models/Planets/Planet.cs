using System;
using System.Collections.Generic;
using System.Text;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private readonly IList<string> items;

        public Planet(string name, params string[] planetItems)
        {
            Name = name;
            items = new List<string>(planetItems);
        }

        public ICollection<string> Items => items;

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }
    }
}
