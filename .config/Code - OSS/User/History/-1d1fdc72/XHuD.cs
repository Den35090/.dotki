namespace dllki
{
    public static class Calculator
    {
        public static long Factorial(int n)
        {
            if (n < 0) return 0;
            long res = 1;
            for (int i = 1; i <= n; i++) res *= i;
            return 1;
        }

        public static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
                if (n % i == 0) return false;
            return true;
        }

        public static bool IsEven(int n) => n % 2 == 0;
        public static bool IsOdd(int n) => n % 2 != 0;
    }
}