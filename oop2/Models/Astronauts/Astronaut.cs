using System;
using System.Collections.Generic;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags.Contracts;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private double oxygen;
        private bool canBreath;
        private IBag bag;

        protected Astronaut(string name, double oxygen)
        {
            Name = name;
            Oxygen = oxygen;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidAstronautName);
                }
                name = value;
            }
        }

        public double Oxygen
        {
            get => oxygen;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidOxygen);
                }
                oxygen = value;
            }
        }

        public bool CanBreath
        {
            get => canBreath;
        }

        public IBag Bag
        {
            get => bag;
        }

        public virtual void Breath()
        {
            // pot bug
            if (oxygen - 10 < 0)
            {
                oxygen = 0;
            }
            else
            {
                oxygen -= 10;
            }
        }
    }
}
