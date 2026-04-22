namespace MathLibrary
{
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0) throw new System.ArgumentException("Знаменатель не может быть 0");
            Numerator = numerator;
            Denominator = denominator;
        }

        public Fraction Multiply(Fraction other)
        {
            return new Fraction(this.Numerator * other.Numerator, this.Denominator * other.Denominator);
        }

        public override string ToString() => $"{Numerator}/{Denominator}";
    }
}