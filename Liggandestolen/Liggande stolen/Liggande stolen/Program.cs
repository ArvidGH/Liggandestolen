namespace Liggande_stolen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ange polynomets termer för dividend (högsta grad först):");
            List<PolynomialTerm> dividendTerms = ReadPolynomialTerms();

            Console.WriteLine("Ange polynomets termer för divisor (högsta grad först):");
            List<PolynomialTerm> divisorTerms = ReadPolynomialTerms();

            List<PolynomialTerm> quotientTerms;
            List<PolynomialTerm> remainderTerms;

            PolynomDivision(dividendTerms, divisorTerms, out quotientTerms, out remainderTerms);

            Console.WriteLine("Kvoten är:");
            PrintPolynomial(quotientTerms);

            Console.WriteLine("Resten är:");
            PrintPolynomial(remainderTerms);
        }

        static List<PolynomialTerm> ReadPolynomialTerms()
        {
            List<PolynomialTerm> terms = new List<PolynomialTerm>();

            string[] input = Console.ReadLine().Split(' ');
            foreach (string term in input)
            {
                string[] parts = term.Split('x');
                int coefficient = int.Parse(parts[0]);
                int exponent = parts.Length > 1 ? int.Parse(parts[1].Substring(1)) : 0;
                terms.Add(new PolynomialTerm(coefficient, exponent));
            }

            return terms;
        }

        static void PrintPolynomial(List<PolynomialTerm> terms)
        {
            for (int i = 0; i < terms.Count; i++)
            {
                PolynomialTerm term = terms[i];

                if (term.Coefficient != 0)
                {
                    if (term.Coefficient > 0 && i != 0)
                    {
                        Console.Write("+");
                    }

                    if (term.Exponent == 0)
                    {
                        Console.Write(term.Coefficient);
                    }
                    else if (term.Exponent == 1)
                    {
                        Console.Write(term.Coefficient + "x");
                    }
                    else
                    {
                        Console.Write(term.Coefficient + "x^" + term.Exponent);
                    }
                }
            }

            Console.WriteLine();
        }

        static void PolynomDivision(List<PolynomialTerm> dividend, List<PolynomialTerm> divisor, out List<PolynomialTerm> quotient, out List<PolynomialTerm> remainder)
        {
            quotient = new List<PolynomialTerm>();
            remainder = new List<PolynomialTerm>(dividend);

            while (remainder.Count >= divisor.Count)
            {
                PolynomialTerm factor = new PolynomialTerm(remainder[0].Coefficient / divisor[0].Coefficient, remainder[0].Exponent - divisor[0].Exponent);
                quotient.Add(factor);

                List<PolynomialTerm> tempDivisor = new List<PolynomialTerm>(divisor);
                MultiplyPolynomial(tempDivisor, factor);

                for (int i = 0; i < tempDivisor.Count; i++)
                {
                    remainder[i].Coefficient -= tempDivisor[i].Coefficient;
                }

                // Ta bort ledande termer med koefficienten 0 från resten
                while (remainder.Count > 0 && remainder[0].Coefficient == 0)
                {
                    remainder.RemoveAt(0);
                }
            }
        }

        static void MultiplyPolynomial(List<PolynomialTerm> polynomial, PolynomialTerm factor)
        {
            for (int i = 0; i < polynomial.Count; i++)
            {
                polynomial[i].Coefficient *= factor.Coefficient;
                polynomial[i].Exponent += factor.Exponent;
            }
        }
    }

    class PolynomialTerm
    {
        public int Coefficient { get; set; }
        public int Exponent { get; set; }

        public PolynomialTerm(int coefficient, int exponent)
        {
            Coefficient = coefficient;
            Exponent = exponent;
        }
    }
}