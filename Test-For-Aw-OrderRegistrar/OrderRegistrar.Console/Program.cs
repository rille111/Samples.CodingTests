namespace OrderRegistrar.Console
{
    using System;

    class Program
    {
        static void Main()
        {
            try
            {
                var container = Bootstrap.ConfigureContainer();
                var app = container.GetInstance<ApplicationOrchestrator>();
                app.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }
}