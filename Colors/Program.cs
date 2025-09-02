using Colors.AI.Services;
using SKcode.Data;
using Smile_7.Agents.Services;
using Smile_7.Plugins.MultiAgent;

namespace Colors
{
    internal static class Program
    {
        public static string BasePath = @"C:\Users\benja\source\Next\Colors\CoreGame\Game\";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ProjectDBContext.Project.Database.EnsureDeleted();

            try
            {
                ProjectDBContext.Project.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating database: " + ex.Message);
                return;
            }

            AzureOpenAIService.Endpoint = Environment.GetEnvironmentVariable("AZUREOPENAIENDPOINT");
            AzureOpenAIService.Key = Environment.GetEnvironmentVariable("AZUREOPENAIKEY");
            AzureOpenAIService.Model = "gpt-4o-smile";

            SpeechService.Endpoint = Environment.GetEnvironmentVariable("AZURESPEECHENDPOINT");
            SpeechService.Key = Environment.GetEnvironmentVariable("AZURESPEECHKEY");

            BaseAgent.OpenAIKey = Environment.GetEnvironmentVariable("OPENAIKEY");

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}