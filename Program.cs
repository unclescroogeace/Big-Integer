using System;

namespace BigInteger
{

    class Program
    {

        static void Main(string[] args)
        {
            BigInt bigNum1 = new BigInt();
            BigInt bigNum2 = new BigInt();

            Console.WriteLine("************");
            Console.WriteLine("* Addition *");
            Console.WriteLine("************");
            bigNum1 = new BigInt("-123456789123456789123456789123456789");
            bigNum2 = new BigInt("98765432164321987654377889927471");
            Console.WriteLine(bigNum1.bigNum + " + " + bigNum2.bigNum + " = " + (bigNum1 + bigNum2).ToString());

            Console.Write(Environment.NewLine + Environment.NewLine);

            Console.WriteLine("***************");
            Console.WriteLine("* Subtraction *");
            Console.WriteLine("***************");
            bigNum1 = new BigInt("951753468295175346829517534682");
            bigNum2 = new BigInt("28643571592864357159");
            Console.WriteLine(bigNum1.bigNum + " - " + bigNum2.bigNum + " = " + (bigNum1 - bigNum2).ToString());

            Console.Write(Environment.NewLine + Environment.NewLine);

            Console.WriteLine("******************");
            Console.WriteLine("* Multiplication *");
            Console.WriteLine("******************");
            bigNum1 = new BigInt("36295182958474546951623847");
            bigNum2 = new BigInt("721689743456");
            Console.WriteLine(bigNum1.bigNum + " * " + bigNum2.bigNum + " = " + (bigNum1 * bigNum2).ToString());

            Console.Write(Environment.NewLine + Environment.NewLine);

            Console.WriteLine("************");
            Console.WriteLine("* Division *");
            Console.WriteLine("************");
            bigNum1 = new BigInt("565478898751126564897635135644");
            bigNum2 = new BigInt("-18426546546544655446565965");
            Console.WriteLine(bigNum1.bigNum + " / " + bigNum2.bigNum + " = " + (bigNum1 / bigNum2).ToString());

            Console.Write(Environment.NewLine + Environment.NewLine);

            Console.WriteLine("*********");
            Console.WriteLine("* Power *");
            Console.WriteLine("*********");
            bigNum1 = new BigInt("25");
            bigNum2 = new BigInt("251");
            Console.WriteLine(bigNum1.ToString() + " ^ " + bigNum2.ToString() + " = " + BigInt.Pow(bigNum1, bigNum2).ToString());

            Console.Write(Environment.NewLine + Environment.NewLine);

            Console.WriteLine("********");
            Console.WriteLine("* Sqrt *");
            Console.WriteLine("********");
            bigNum1 = new BigInt("266256");
            Console.WriteLine("Square root from " + bigNum1.ToString() + " is " + BigInt.Sqrt(bigNum1).ToString());

            Console.Write(Environment.NewLine + Environment.NewLine);

            Console.ReadKey();
        }
    }
}
