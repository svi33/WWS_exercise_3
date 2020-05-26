using ClassLibraryForDB.DataBase;
using System;


namespace ConsoleFindInt.Work
{
    class Menu
    {
        public const string Title = "Function that takes a positive integer and returns the next smaller positive integer containing the same digits.";

        public Result CurrentRessult { get; }

        public bool GetCheckMenu() 
        {
            int checkMenu=1;
            Console.WriteLine("Input 0 to exit any key to cotininue: ");
            string Input = Console.ReadLine();
            bool success = Int32.TryParse(Input, out checkMenu);
            if (success && checkMenu==0) return false;
            return true;
        }

        public void GetInput()
        {
            Console.WriteLine("Input number to find next smaller:");
            CurrentRessult.Input = Convert.ToInt64(Console.ReadLine());
        }

        public void SetAnswer() 
        {
           CurrentRessult.Answer = CalcResult.GetNextSmaller(CurrentRessult.Input);

            if (CurrentRessult.Answer == (-1))
                Console.WriteLine("This number has not next smaller!");
            else
                Console.WriteLine($"Next smaller was: {CurrentRessult.Answer}");
        }

        public Menu()
        {
            CurrentRessult = new Result();
        }
    }
}
