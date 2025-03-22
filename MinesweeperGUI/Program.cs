namespace MinesweeperGUI
{
    internal static class Program
    {
        public static Form1 Form1
        {
            get => default;
            set
            {
            }
        }

        public static Form2 Form2
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form2());
        }
    }
}