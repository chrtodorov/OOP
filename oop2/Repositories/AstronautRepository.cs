using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly IDictionary<string, IAstronaut> astronautName;

        public AstronautRepository()
        {
            astronautName = new Dictionary<string, IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => astronautName.Values.ToList();

        public void Add(IAstronaut model)
        {
            astronautName.Add(model.Name, model);
        }

        public IAstronaut FindByName(string name)
        {
            if (astronautName.ContainsKey(name))
            {
                return astronautName[name];
            }
            else
            {
                return null;
            }
        }

        public bool Remove(IAstronaut model)
        {
            return astronautName.Remove(model.Name);
        }
    }
}
