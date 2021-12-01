using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly IDictionary<string, IPlanet> planetsName;

        public PlanetRepository()
        {
            planetsName = new Dictionary<string, IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => planetsName.Values.ToList();

        public void Add(IPlanet model)
        {
            planetsName.Add(model.Name, model);
        }

        public IPlanet FindByName(string name)
        {
            if (planetsName.ContainsKey(name))
            {
                return planetsName[name];
            }
            else
            {
                return null;
            }
        }

        public bool Remove(IPlanet model)
        {
            return planetsName.Remove(model.Name);
        }
    }
}
