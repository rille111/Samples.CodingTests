using System;

namespace OrderRegistrar.Console.Menus
{
    public class MainMenu : IMenu
    {
        public MenuAction Show()
        {
            System.Console.Clear();
            System.Console.WriteLine("Order Registrar");
            System.Console.WriteLine();
            System.Console.WriteLine("C - Create Order");
            System.Console.WriteLine("S - Show Orders");
            System.Console.WriteLine("Esc - Exit");
            System.Console.WriteLine();
            System.Console.WriteLine("--- Awaiting user input ---");

            while (true)
            {
                var input = System.Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.C:
                        return MenuAction.CreateOrder;
                    case ConsoleKey.S:
                        return MenuAction.ShowOrders;
                    case ConsoleKey.Escape:
                        return MenuAction.Exit;
                }
            }
        }
    }
}
