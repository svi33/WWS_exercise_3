using ClassLibraryForDB.DataBase;
using ConsoleFindInt.Work;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace ConsoleFindInt
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu work = new Menu();
            Console.WriteLine(Menu.Title);
            do
            {
                Console.WriteLine("Last five calculations:");

                using (var db = ApplicationContext.SetDBcon())
                {
                    var lastResult = db.Results.OrderBy(b => b.Id)
                    .Skip(Math.Max(0, db.Results.OrderBy(b => b.Id).Count() - 5));

                    foreach (Result II in lastResult)
                    {
                        string cell = (II.Answer == -1) ? "no smaller" : II.Answer.ToString();
                        Console.WriteLine(II.Id + ") " + II.Input + " -> " + cell);
                    }
                }
                work.GetInput();
                work.SetAnswer();

                using (var db = ApplicationContext.SetDBcon())
                {
                    db.Results.Add(new Result
                    {
                        Input = work.CurrentRessult.Input,
                        Answer = work.CurrentRessult.Answer
                    }
                    );
                    db.SaveChanges();
                }
            } while (work.GetCheckMenu());

        }
    }
}
