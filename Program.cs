using System;

namespace BabyLeaveCalculator
{
  class Program
  {
    static void Main(string[] args)
    {
      var ptoAccrued = 72.04;
      var ptoGainedPerWorkDay = .923;
      var ptoUsedPerWorkDay = 4.0; // half days...
      var initialTimeOffAfterDeliveryInDays = 2.0; // assume you'll take a 2 days off after arrival
      var stopWhenPtoBalanceReaches = -24.0; // max allowed is -40.0
      var deliveredOn = DateTime.Parse("4/24/2021");
      DateTime currentDay = deliveredOn.AddDays(initialTimeOffAfterDeliveryInDays); // advance time based on initial time off...

      Console.WriteLine($"Baby delivered on {currentDay.ToString("MM/dd/yyyy")}, Congratulations... you're sleep deprived (again!)");
      Console.WriteLine($"Taking {(int)initialTimeOffAfterDeliveryInDays} days off...\r\n");

      var ptoLeft = ptoAccrued - (initialTimeOffAfterDeliveryInDays * 8.0); 

      while (ptoLeft > stopWhenPtoBalanceReaches)
      {
        if (currentDay.DayOfWeek == DayOfWeek.Sunday)
        {
          Console.WriteLine(OffToday());
          AdvanceDay();
          continue;
        }

        Console.Write(WorkingToday());
        ptoLeft -= ptoUsedPerWorkDay;
        ptoLeft += ptoGainedPerWorkDay;
        Console.WriteLine(PtoBalance());

        AdvanceDay();
      }

      Console.WriteLine($"\r\nYou will run out of pto on {currentDay.ToString("MM/dd/yy")}.  Back to work sucka!");
      Console.ReadLine();

      void AdvanceDay() 
        => currentDay = currentDay.AddDays(1);

      string OffToday()
        => $"{currentDay.ToString("ddd MMM dd")} - OFF!      PTO: {FormatsPto()}";

      string WorkingToday()
        => $"Working on {currentDay.ToString("ddd MMM dd")}: PTO: {FormatsPto()}";

      string PtoBalance()
        => $" => {FormatsPto()}";

      string FormatsPto()
        => ptoLeft.ToString("N1").PadLeft(5, ' ');
    }
  }
}
