using JvLib.Data;
using Project.Gameplay;

namespace JvLib.Pooling.Data.Objects
{
    public partial class PooledObjectConfig
    {
        private static JvLib.Pooling.Data.Objects.PooledObjectConfigs values;
        private static Project.Gameplay.AmmunitionConfig coconut;
        private static Project.Gameplay.AmmunitionConfig watermelon;
        private static Project.Gameplay.AmmunitionConfig apple;
        private static Project.Gameplay.AmmunitionConfig banana;
        private static Project.Gameplay.AmmunitionConfig mandarine;
        private static Project.Gameplay.AmmunitionConfig tomato;

        public static JvLib.Pooling.Data.Objects.PooledObjectConfigs Values
        {
            get
            {
                if (values == null)
                    values = (JvLib.Pooling.Data.Objects.PooledObjectConfigs)DataRegistry.GetList("59e5ce10c9334a84999ee081725105f3");
                return values;
            }
        }

        public static Project.Gameplay.AmmunitionConfig Coconut
        {
            get
            {
                if (coconut == null && Values != null)
                    coconut = (Project.Gameplay.AmmunitionConfig)Values.GetEntry("45083288ed63f2242b0a3940c18a7618");
                return coconut;
            }
        }

        public static Project.Gameplay.AmmunitionConfig Watermelon
        {
            get
            {
                if (watermelon == null && Values != null)
                    watermelon = (Project.Gameplay.AmmunitionConfig)Values.GetEntry("988e9417b7c35b9468ef9f678c0c7dbe");
                return watermelon;
            }
        }

        public static Project.Gameplay.AmmunitionConfig Apple
        {
            get
            {
                if (apple == null && Values != null)
                    apple = (Project.Gameplay.AmmunitionConfig)Values.GetEntry("6fcf80efb6f1ff440a991b6ce0ee0d76");
                return apple;
            }
        }

        public static Project.Gameplay.AmmunitionConfig Banana
        {
            get
            {
                if (banana == null && Values != null)
                    banana = (Project.Gameplay.AmmunitionConfig)Values.GetEntry("75a466a147db7534abd2e531e02e82f9");
                return banana;
            }
        }

        public static Project.Gameplay.AmmunitionConfig Mandarine
        {
            get
            {
                if (mandarine == null && Values != null)
                    mandarine = (Project.Gameplay.AmmunitionConfig)Values.GetEntry("159f339eecf325c41a68bedab0d170bc");
                return mandarine;
            }
        }

        public static Project.Gameplay.AmmunitionConfig Tomato
        {
            get
            {
                if (tomato == null && Values != null)
                    tomato = (Project.Gameplay.AmmunitionConfig)Values.GetEntry("35940ed338bd1ed40a41905725461cdf");
                return tomato;
            }
        }

    }
}

