using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimTrainer
{
    internal class Bonus : Target
    {
        public TypeBonus type;
        public double secondeApparition;

        public TypeBonus Type
        {
            get; set;
        }

        public double SecondeApparition
        {
            get; set;
        }

        public Bonus(int taille, TypeBonus type, double secondeApparition) : base(taille)
        {
            Type = type;
            SecondeApparition = secondeApparition;
        }
    }
}
