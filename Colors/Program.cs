using SKcode.Data;

namespace Colors
{
    internal static class Program
    {
        public static ProjectDBContext Project = new ProjectDBContext("colors.db");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Project.Database.EnsureDeleted();
            Project.Database.EnsureCreated();

            AzureOpenAIService.Endpoint = Environment.GetEnvironmentVariable("AZUREOPENAIENDPOINT");
            AzureOpenAIService.Key = Environment.GetEnvironmentVariable("AZUREOPENAIKEY");
            AzureOpenAIService.Model = "gpt-4o-smile";

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}