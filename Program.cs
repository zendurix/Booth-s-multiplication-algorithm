using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booth_alorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Write first decimal number: ");
            int dec1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Write second decimal number: ");
            int dec2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(dec1 + " * " + dec2 + " = " + (dec1*dec2));

            string bin1 = Dec_to_bin(dec1);
            string bin2 = Dec_to_bin(dec2);

            int l1 = bin1.Length, l2 = bin2.Length;


            if (bin1.Length > bin2.Length)
            {
                for (int i = 1; i <= l1 - l2; i++) 
                    bin2 = (bin2[0] == '+') ? bin2.Remove(0, 1).Insert(0, "+0") : bin2.Remove(0, 1).Insert(0, "-0");
            }

            if (bin2.Length > bin1.Length)
            {
                for (int i = 1; i <= l2 - l1; i++)
                    bin1 = (bin1[0] == '+') ? bin1.Remove(0, 1).Insert(0, "+0") : bin1.Remove(0, 1).Insert(0, "-0");
            }

            Console.WriteLine("numbers converted to binary:\n"+bin1 + "\n"+bin2);

            Console.WriteLine("\n     BOOTH ALGORITHM:\n");
            string a = BOOTH(bin1, bin2);
            Console.WriteLine(U2_to_dec(a));
            
            Console.ReadLine();
        }





        static string BOOTH(string bin1, string bin2)
        {
            string result = "";
            string part1 = "", part2 = "";
            string plusM, minusM;
            char q = '0';
            string space =" ";

            part2 = Bin_to_U2(bin2);
            for (int i = 0; i < part2.Length; i++)
                part1 += "0";

            plusM = Bin_to_U2(bin1);
            bin1 = (bin1[0] == '+') ? bin1.Remove(0, 1).Insert(0, "-") : bin1.Remove(0, 1).Insert(0, "+");
            minusM = Bin_to_U2(bin1);

            for (int i = 1; i <= part1.Length; i++)
            {
                if (i >= 10) space = "";

                Console.WriteLine(i + space + ":    " + part1 + "   " + part2 + "   " + q);
                if (part2[part2.Length - 1] == '0' && q == '0' || part2[part2.Length - 1] == '1' && q == '1')
                    { }
                if (part2[part2.Length - 1] == '1' && q == '0')
                    part1 = Bin_add(part1, minusM);
                if (part2[part2.Length - 1] == '0' && q == '1')
                    part1 = Bin_add(part1, plusM);

                Shift(ref part1, ref part2, ref q);
            }
            result = part1 + part2;
            Console.WriteLine(result);
            return result;
        }






        static void Shift(ref string p1, ref string p2, ref char q)
        {
            q = p2[p2.Length - 1];
            for (int i = p2.Length-1; i >= 1; i--)
                p2 = p2.Remove(i, 1).Insert(i, Convert.ToString(p2[i -1]));
            p2 = p2.Remove(0, 1).Insert(0, Convert.ToString(p1[p1.Length - 1]));

            for (int i = p1.Length - 1; i >= 1; i--)
                p1 = p1.Remove(i, 1).Insert(i, Convert.ToString(p1[i - 1]));
        }









        static string Dec_to_bin(int dec)
        {
            string bin = "";
            bool finished = false;
            bool minus = false;
            char holder;

            if (dec < 0)
            {
                dec *= -1;
                minus = true;
            }

            while (!finished)
            {
                bin += (dec % 2 == 1) ? '1' : '0';
                dec /= 2;
                finished = (dec == 0) ? true : false;
            }

            for (int i = 0; i < (bin.Length) / 2; i++)
            {
                holder = bin[i];
                bin = bin.Remove(i, 1).Insert(i, Convert.ToString(bin[bin.Length - 1 - i]));
                bin = bin.Remove(bin.Length - 1 - i, 1).Insert(bin.Length - 1 - i, Convert.ToString(holder));
            }

            bin = (minus) ? "-" + bin : "+" + bin;
       
            return bin;
        }







        static string Bin_to_U2(string bin)
        {
            if (bin[0] == '+')
                bin = bin.Remove(0, 1).Insert(0, "0");
            else
            {
                bin = bin.Remove(0, 1).Insert(0, "0");
                for (int i = 0; i < bin.Length; i++)
                    bin = (bin[i] == '1') ? bin.Remove(i, 1).Insert(i, "0") : bin.Remove(i, 1).Insert(i, "1");

                for (int i = bin.Length-1; i >=0 ; i--)
                {
                    bin = (bin[i] == '1') ? bin.Remove(i, 1).Insert(i, "0") : bin.Remove(i, 1).Insert(i, "1");
                    if (bin[i] == '1')
                        break;
                }
            }

            return bin;
        }







        static int U2_to_dec(string U2)
        {
            int dec = 0;

            dec -= Convert.ToInt32(Char.GetNumericValue(U2[0])) * Convert.ToInt32(Math.Pow(2, Convert.ToDouble(U2.Length-1)));

            for (int i = 1; i <= U2.Length - 1; i++)
                dec += Convert.ToInt32(Char.GetNumericValue(U2[i])) * Convert.ToInt32(Math.Pow(2, Convert.ToDouble(U2.Length -1 -i)));

            return dec;
        }






        static string Bin_add(string bin1, string bin2)
        {
            string result = "";
            char moved = '0';
            for (int i = bin1.Length - 1; i >= 0; i--)
                result += "x";

            for (int i = bin1.Length-1; i >= 0; i--)
            {
                if (Convert.ToInt32(Char.GetNumericValue(bin1[i])) + Convert.ToInt32(Char.GetNumericValue(bin2[i]))
                    + Convert.ToInt32(Char.GetNumericValue(moved)) == 3)
                {
                    result = result.Remove(i, 1).Insert(i, "1");
                    moved = '1';
                }
                else if (Convert.ToInt32(Char.GetNumericValue(bin1[i])) + Convert.ToInt32(Char.GetNumericValue(bin2[i]))
                    + Convert.ToInt32(Char.GetNumericValue(moved)) == 2)
                {
                    result = result.Remove(i, 1).Insert(i, "0");
                    moved = '1';
                }
                else if (Convert.ToInt32(Char.GetNumericValue(bin1[i])) + Convert.ToInt32(Char.GetNumericValue(bin2[i]))
                    + Convert.ToInt32(Char.GetNumericValue(moved)) == 1)
                {
                    result = result.Remove(i, 1).Insert(i, "1");
                    moved = '0';
                }
                else if (Convert.ToInt32(Char.GetNumericValue(bin1[i])) + Convert.ToInt32(Char.GetNumericValue(bin2[i]))
                    + Convert.ToInt32(Char.GetNumericValue(moved)) == 0)
                {
                    result = result.Remove(i, 1).Insert(i, "0");
                    moved = '0';
                }
            }
            return result;
        }

    }

}
