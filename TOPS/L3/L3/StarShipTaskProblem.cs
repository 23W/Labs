using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3
{
    internal class StarShipTaskProblem : ITaskProblem
    {
        internal enum Feature
        {
            GatlingGun = 0,
            Missiles,
            IronArmour,
            TitanArmor,
            PlasmaEngine,
            IonEngine,
        }

        internal struct FeatureParam
        {
            internal double WarScore;
            internal double Price;
            internal double FirePlace;
            internal double ArmorPlace;
            internal double EnginePlace;
        }

        static internal readonly Dictionary<Feature, FeatureParam> FeatureDate = new Dictionary<Feature, FeatureParam>()
        {
            { Feature.GatlingGun,   new FeatureParam {WarScore = 5,   Price = 10,  FirePlace = 1,  ArmorPlace = 0,  EnginePlace = 0} },
            { Feature.Missiles,     new FeatureParam {WarScore = 25,  Price = 100, FirePlace = 2,  ArmorPlace = 0,  EnginePlace = 0} },
            { Feature.IronArmour,   new FeatureParam {WarScore = 5,   Price = 10,  FirePlace = 0,  ArmorPlace = 1,  EnginePlace = 0} },
            { Feature.TitanArmor,   new FeatureParam {WarScore = 10,  Price = 40,  FirePlace = 0,  ArmorPlace = 1,  EnginePlace = 0} },
            { Feature.PlasmaEngine, new FeatureParam {WarScore = 8,   Price = 20,  FirePlace = 0,  ArmorPlace = 0,  EnginePlace = 1} },
            { Feature.IonEngine,    new FeatureParam {WarScore = 20,  Price = 80,  FirePlace = 0,  ArmorPlace = 0,  EnginePlace = 1} }
        };

        internal const double MoneyAmount = 150;
        internal const double FireZones = 4;
        internal const double ArmorZones = 2;
        internal const double EngineZonesMax = 2;
        internal const double EngineZonesMin = 1;

        internal double[] Scores { get; } = Enum.GetValues<Feature>().Select(f => FeatureDate[f].WarScore).ToArray();
        internal double[] Prices { get; } = Enum.GetValues<Feature>().Select(f => FeatureDate[f].Price).ToArray();
        internal double[] Fires { get; } = Enum.GetValues<Feature>().Select(f => FeatureDate[f].FirePlace).ToArray();
        internal double[] Armors { get; } = Enum.GetValues<Feature>().Select(f => FeatureDate[f].ArmorPlace).ToArray();
        internal double[] Engines { get; } = Enum.GetValues<Feature>().Select(f => FeatureDate[f].EnginePlace).ToArray();

        public LPSolver Build()
        {
            var lp = new LPSolver(Scores, LPSolver.LPType.Integer);

            // total money constrain
            lp.AddConstraint(LPSolver.ConstrainType.Upper, Prices, MoneyAmount);
            lp.AddConstraint(LPSolver.ConstrainType.Upper, Fires, FireZones);
            lp.AddConstraint(LPSolver.ConstrainType.Upper, Armors, ArmorZones);
            lp.AddConstraint(LPSolver.ConstrainType.Upper, Engines, EngineZonesMax);
            lp.AddConstraint(LPSolver.ConstrainType.Lower, Engines, EngineZonesMin);

            foreach (var f in Enum.GetValues<Feature>())
            {
                lp.AddBound(LPSolver.BoundType.Lower, (int)f, 0);
                lp.AddIntegralConstraint((int)f);
            }

            return lp;
        }
    }
}
