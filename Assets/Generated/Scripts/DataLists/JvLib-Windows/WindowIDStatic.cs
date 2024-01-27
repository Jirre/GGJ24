using JvLib.Data;

namespace JvLib.Windows
{
    public partial class WindowID
    {
        private static JvLib.Windows.WindowIDs values;
        private static JvLib.Windows.WindowID mainMenu;
        private static JvLib.Windows.WindowID characterSelect;

        public static JvLib.Windows.WindowIDs Values
        {
            get
            {
                if (values == null)
                    values = (JvLib.Windows.WindowIDs)DataRegistry.GetList("f394fd4891e0344419e588a5f44d77fc");
                return values;
            }
        }

        public static JvLib.Windows.WindowID MainMenu
        {
            get
            {
                if (mainMenu == null && Values != null)
                    mainMenu = (JvLib.Windows.WindowID)Values.GetEntry("846dc24474764d7408f832779e62061b");
                return mainMenu;
            }
        }

        public static JvLib.Windows.WindowID CharacterSelect
        {
            get
            {
                if (characterSelect == null && Values != null)
                    characterSelect = (JvLib.Windows.WindowID)Values.GetEntry("1fe4c1115cd4c8143b744750781ce88a");
                return characterSelect;
            }
        }

    }
}

