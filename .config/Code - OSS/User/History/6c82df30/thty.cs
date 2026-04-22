using System.Linq;
using System.Threading.Tasks;

namespace ParallelLibrary
{
    public class ParallelCalculator
    {
        public CalculationResult ProcessAll(int[] numbers, int divisor)
        {
            CalculationResult result = new CalculationResult();

            Parallel.Invoke(
                () => result.Sum = numbers.Sum(x => (long)x),
                () => result.Max = numbers.AsParallel().Max(),
                () => result.MultiplesCount = numbers.AsParallel().Count(x => x % divisor == 0),
                () => result.Average = numbers.AsParallel().Average()
            );

            return result;
        }
    }
}