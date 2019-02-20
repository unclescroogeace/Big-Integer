using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BigInteger
{
    class BigInt
    {

        public string bigNum { get; private set; }

        public BigInt(string _bigNum)
        {
            Match match = Regex.Match(_bigNum, "^$|^-?([0-9]+[-,])*[0-9]+$");
            if (match.Success)
            {
                bigNum = _bigNum;
            }
            else
            {
                throw new FormatException();
            }
        }

        public BigInt(BigInt _bigNum)
        {
            Match match = Regex.Match(_bigNum.bigNum, "^$|^-?([0-9]+[-,])*[0-9]+$");
            if (match.Success)
            {
                bigNum = _bigNum.bigNum;
            }
            else
            {
                throw new FormatException();
            }
        }

        public BigInt()
        {

        }

        public override string ToString()
        {
            return bigNum;
        }

        public static string ReplaceCharAtPosition(string str, int pos, string newChar)
        {
            str = str.Remove(pos, 1).Insert(pos, newChar);
            return str;
        }

        public static string ReplaceAndLowerPrevNum(string str, int pos)
        {
            int value = int.Parse(str[pos].ToString());
            value--;
            str = ReplaceCharAtPosition(str, pos, value.ToString());
            return str;
        }

        public static string ReplaceAndUpNum(string str, int pos)
        {
            int value = int.Parse(str[pos].ToString());
            value += 10;
            StringBuilder sb = new StringBuilder(str);
            sb[pos] = (char)value;
            str = sb.ToString();
            return str;
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is BigInt))
            {
                return false;
            }
            BigInt bigNum1 = (BigInt)obj;

            return bigNum1 == this;

        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + bigNum.GetHashCode();
                return hash;
            }
        }

        public static BigInt Pow(BigInt bigNum1, BigInt power)
        {
            if (power.bigNum == "0")
            {
                return new BigInt("1");
            }
            BigInt multiplier = Pow(bigNum1, power / new BigInt("2"));
            if (int.Parse(power.bigNum[power.bigNum.Length - 1].ToString()) % 2 == 0)
            {
                return multiplier * multiplier;
            }
            else
            {
                return (bigNum1 * multiplier * multiplier);
            }
        }

        public static BigInt Sqrt(BigInt bigNum)
        {
            BigInt low = new BigInt("0");
            BigInt top = bigNum;
            BigInt mid = new BigInt();
            while (true)
            {
                mid = (low + top) / new BigInt("2");

                BigInt calcMid = mid * mid;
                if (calcMid >= bigNum && (mid - new BigInt("1")) * (mid - new BigInt("1")) < bigNum)
                {
                    if (calcMid != bigNum)
                    {
                        return mid - new BigInt("1");
                    }
                    return mid;
                }
                else if (calcMid < bigNum)
                {
                    low = mid + new BigInt("1");
                }
                else
                {
                    top = mid - new BigInt("1");
                }
            }
        }

        public static BigInt operator ++(BigInt bigNum1)
        {
            int incr = 0;
            int numOneLength = bigNum1.bigNum.Length;
            StringBuilder sb = new StringBuilder(bigNum1.bigNum);

            if (sb[0] == '-')
            {
                sb = sb.Remove(0, 1);
                if (((numOneLength - 1) == 1) && (sb[0] == '1'))
                {
                    sb[0] = '0';
                }
                else
                {
                    bigNum1.bigNum = sb.ToString();
                    bigNum1 = bigNum1--;
                    sb.Clear();
                    sb.Append("-");
                    sb.Append(bigNum1.bigNum);
                }
            }
            else if (int.Parse(sb[numOneLength - 1].ToString()) < 9)
            {
                incr = int.Parse(sb[numOneLength - 1].ToString());
                incr++;
                sb = sb.Remove(numOneLength - 1, 1);
                sb.Append(incr.ToString());
            }
            else if (bigNum1.bigNum.Length == 1)
            {
                sb[0] = '1';
                sb.Append("0");
            }
            else
            {
                int i = numOneLength - 1;

                while (int.Parse(sb[i].ToString()) == 9 && i >= 1)
                {
                    sb[i] = '0';
                    i--;
                }
                if (numOneLength - i == 0)
                {
                    sb.Insert(0, "1");
                }
                else
                {
                    incr = int.Parse(sb[i].ToString());
                    incr++;
                    sb = sb.Remove(i, 1);
                    sb = sb.Insert(i, incr.ToString());
                }
            }
            bigNum1.bigNum = sb.ToString();
            return bigNum1;
        }

        public static BigInt operator --(BigInt bigNum1)
        {
            int decr = 0;
            int numOneLength = bigNum1.bigNum.Length;
            StringBuilder sb = new StringBuilder(bigNum1.bigNum);

            if (sb[0] == '-')
            {
                sb = sb.Remove(0, 1);
                bigNum1.bigNum = sb.ToString();
                bigNum1 = bigNum1++;
                sb.Clear();
                sb.Append("-");
                sb.Append(bigNum1.bigNum);
            }
            else if (int.Parse(sb[numOneLength - 1].ToString()) >= 1)
            {
                decr = int.Parse(sb[numOneLength - 1].ToString());
                decr--;
                sb = sb.Remove(numOneLength - 1, 1);
                sb = sb.Insert(numOneLength - 1, decr.ToString());
            }
            else if (numOneLength == 1 && sb[0] == '0')
            {
                sb.Clear();
                sb.Append("-1");
            }
            else
            {
                int i = numOneLength - 1;

                while (sb[i] == '0' && i >= 1)
                {
                    sb[i] = '9';
                    i--;
                }
                if (i == 0)
                {
                    if (int.Parse(sb[0].ToString()) > 1)
                    {
                        decr = int.Parse(sb[0].ToString());
                        decr--;
                        sb.Remove(0, 1);
                        sb.Insert(0, decr.ToString());
                    }
                    else
                    {
                        sb.Remove(0, 1);
                    }
                }
            }
            bigNum1.bigNum = sb.ToString();
            return bigNum1;
        }

        public static bool operator >(BigInt bigNum1, BigInt bigNum2)
        {
            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] != '-')
            {
                return false;
            }

            if (bigNum1.bigNum[0] != '-' && bigNum2.bigNum[0] == '-')
            {
                return true;
            }

            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] == '-')
            {
                bigNum1.bigNum = bigNum1.bigNum.Remove(0, 1);
                bigNum2.bigNum = bigNum2.bigNum.Remove(0, 1);
                if (bigNum1 < bigNum2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            int numOneLength = bigNum1.bigNum.Length;
            int numTwoLength = bigNum2.bigNum.Length;

            if (numOneLength > numTwoLength)
            {
                return true;
            }

            if (numOneLength == numTwoLength)
            {
                for (int i = 0; i < numOneLength; i++)
                {
                    if (int.Parse(bigNum1.bigNum[i].ToString()) > int.Parse(bigNum2.bigNum[i].ToString()))
                    {
                        return true;
                    }
                    else if (int.Parse(bigNum1.bigNum[i].ToString()) == int.Parse(bigNum2.bigNum[i].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public static bool operator <(BigInt bigNum1, BigInt bigNum2)
        {
            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] != '-')
            {
                return true;
            }

            if (bigNum1.bigNum[0] != '-' && bigNum2.bigNum[0] == '-')
            {
                return false;
            }

            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] == '-')
            {
                bigNum1.bigNum = bigNum1.bigNum.Remove(0, 1);
                bigNum2.bigNum = bigNum2.bigNum.Remove(0, 1);
                if (bigNum1 > bigNum2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            int numOneLength = bigNum1.bigNum.Length;
            int numTwoLength = bigNum2.bigNum.Length;

            if (numOneLength < numTwoLength)
            {
                return true;
            }

            if (numOneLength == numTwoLength)
            {
                for (int i = 0; i < numOneLength; i++)
                {
                    if (int.Parse(bigNum1.bigNum[i].ToString()) < int.Parse(bigNum2.bigNum[i].ToString()))
                    {
                        return true;
                    }
                    else if (int.Parse(bigNum1.bigNum[i].ToString()) == int.Parse(bigNum2.bigNum[i].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public static bool operator ==(BigInt bigNum1, BigInt bigNum2)
        {
            if (bigNum1.bigNum == bigNum2.bigNum)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(BigInt bigNum1, BigInt bigNum2)
        {
            if (bigNum1.bigNum != bigNum2.bigNum)
            {
                return true;
            }
            return false;
        }

        public static bool operator >=(BigInt bigNum1, BigInt bigNum2)
        {
            if (bigNum1 > bigNum2 || bigNum1 == bigNum2)
            {
                return true;
            }
            return false;
        }

        public static bool operator <=(BigInt bigNum1, BigInt bigNum2)
        {
            if (bigNum1 < bigNum2 || bigNum1 == bigNum2)
            {
                return true;
            }
            return false;
        }

        public static BigInt operator +(BigInt bigNum1, BigInt bigNum2)
        {
            if (bigNum1.bigNum != "0")
            {
                bigNum1.bigNum = bigNum1.bigNum.TrimStart('0');
            }

            if (bigNum2.bigNum != "0")
            {
                bigNum2.bigNum = bigNum2.bigNum.TrimStart('0');
            }
            string result = String.Empty;
            int numOneLength = bigNum1.bigNum.Length;
            int numTwoLength = bigNum2.bigNum.Length;

            if (bigNum1.bigNum[0] != '-' && bigNum2.bigNum[0] != '-')
            {
                if (bigNum1.bigNum == "0")
                {
                    BigInt rtrResult = new BigInt(bigNum2.bigNum);
                    return rtrResult;
                }
                if (bigNum2.bigNum == "0")
                {
                    BigInt rtrResult = new BigInt(bigNum1.bigNum);
                    return rtrResult;
                }

                bool carry = false;
                int sum = 0;

                if (numOneLength > numTwoLength)
                {
                    for (int i = 0; i < numOneLength - numTwoLength; i++)
                    {
                        bigNum2.bigNum = "0" + bigNum2.bigNum;
                    }
                }
                else if (numOneLength < numTwoLength)
                {
                    for (int i = 0; i < numTwoLength - numOneLength; i++)
                    {
                        bigNum1.bigNum = "0" + bigNum1.bigNum;
                    }
                }

                int maxLength = Math.Max(numOneLength, numTwoLength);

                for (int i = maxLength - 1; i >= 0; i--)
                {
                    sum = int.Parse(bigNum1.bigNum[i].ToString()) + int.Parse(bigNum2.bigNum[i].ToString());
                    if (carry)
                    {
                        sum += 1;
                    }
                    carry = false;
                    if (sum > 9)
                    {
                        carry = true;
                        sum -= 10;
                    }
                    result = sum.ToString() + result;
                }
                if (carry)
                {
                    result = "1" + result;
                }
            }

            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] != '-')
            {
                bigNum1.bigNum = bigNum1.bigNum.Substring(1, bigNum1.bigNum.Length - 1);

                if (bigNum1.bigNum == bigNum2.bigNum)
                {
                    BigInt rtrResult = new BigInt("0");
                    return rtrResult;
                }

                bool flagEqual = false;
                if (numOneLength - 1 == numTwoLength)
                {
                    for (int i = 0; i < numOneLength - 1; i++)
                    {
                        if (int.Parse(bigNum1.bigNum[i].ToString()) > int.Parse(bigNum2.bigNum[i].ToString()))
                        {
                            flagEqual = true;
                            break;
                        }
                        else if (int.Parse(bigNum2.bigNum[i].ToString()) > int.Parse(bigNum1.bigNum[i].ToString()))
                        {
                            BigInt rtrResult = new BigInt(bigNum2 - bigNum1);
                            return rtrResult;
                        }
                    }
                }

                if (((numOneLength - 1) > numTwoLength) || flagEqual)
                {
                    BigInt rtrResult = new BigInt(bigNum1 - bigNum2);
                    rtrResult.bigNum = "-" + rtrResult.bigNum;
                    return rtrResult;
                }

                if (numOneLength - 1 < numTwoLength)
                {
                    BigInt rtrResult = new BigInt(bigNum2 - bigNum1);
                    return rtrResult;
                }
            }

            if (bigNum1.bigNum[0] != '-' && bigNum2.bigNum[0] == '-')
            {
                bigNum2.bigNum = bigNum2.bigNum.Substring(1, bigNum2.bigNum.Length - 1);

                if (bigNum1.bigNum == bigNum2.bigNum)
                {
                    BigInt rtrResult = new BigInt("0");
                    return rtrResult;
                }

                if (bigNum1.bigNum == "0")
                {
                    BigInt rtrResult = new BigInt("0");
                    return rtrResult;
                }

                numOneLength = bigNum1.bigNum.Length;
                numTwoLength = bigNum2.bigNum.Length;

                bool flagEqual = false;
                if (numOneLength == numTwoLength)
                {
                    for (int i = 0; i < numOneLength - 1; i++)
                    {
                        if (int.Parse(bigNum1.bigNum[i].ToString()) > int.Parse(bigNum2.bigNum[i].ToString()))
                        {
                            flagEqual = true;
                            break;
                        }
                        else if (int.Parse(bigNum2.bigNum[i].ToString()) > int.Parse(bigNum1.bigNum[i].ToString()))
                        {
                            BigInt rtrResult = new BigInt(bigNum2 - bigNum1);
                            rtrResult.bigNum = "-" + rtrResult.bigNum;
                            return rtrResult;
                        }
                    }
                }

                if ((numOneLength > numTwoLength) || flagEqual)
                {
                    BigInt rtrResult = new BigInt(bigNum1 - bigNum2);
                    return rtrResult;
                }

                if (numTwoLength > numOneLength)
                {
                    BigInt rtrResult = new BigInt(bigNum2 - bigNum1);
                    rtrResult.bigNum = "-" + rtrResult.bigNum;
                    return rtrResult;
                }
            }

            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] == '-')
            {
                bigNum1.bigNum = bigNum1.bigNum.Substring(1, bigNum1.bigNum.Length - 1);
                bigNum2.bigNum = bigNum2.bigNum.Substring(1, bigNum2.bigNum.Length - 1);

                BigInt rtrResult = new BigInt(bigNum1 + bigNum2);
                rtrResult.bigNum = "-" + rtrResult.bigNum;
                return rtrResult;
            }

            if (bigNum1.bigNum != "0")
            {
                bigNum1.bigNum = bigNum1.bigNum.TrimStart('0');
            }

            if (bigNum2.bigNum != "0")
            {
                bigNum2.bigNum = bigNum2.bigNum.TrimStart('0');
            }

            BigInt returnResult = new BigInt(result);
            return returnResult;
        }

        public static BigInt operator -(BigInt bigNum1, BigInt bigNum2)
        {

            if (bigNum1.bigNum != "0")
            {
                bigNum1.bigNum = bigNum1.bigNum.TrimStart('0');
            }

            if (bigNum2.bigNum != "0")
            {
                bigNum2.bigNum = bigNum2.bigNum.TrimStart('0');
            }

            string result = string.Empty;

            if (bigNum1.bigNum == "0")
            {
                result = bigNum2.bigNum;
                goto Result;
            }

            if (bigNum2.bigNum == "0")
            {
                result = bigNum1.bigNum;
                goto Result;
            }

            if (bigNum1.bigNum == bigNum2.bigNum)
            {
                result = "0";
                goto Result;
            }

            int numOneLength;
            int numTwoLength;

            if ((bigNum1.bigNum[0] != '-') && (bigNum2.bigNum[0] != '-'))
            {
                numOneLength = bigNum1.bigNum.Length;
                numTwoLength = bigNum2.bigNum.Length;
                bool flagEqual = false;
                if (numOneLength == numTwoLength)
                {
                    for (int i = 0; i < numOneLength; i++)
                    {
                        if (int.Parse(bigNum1.bigNum[i].ToString()) > int.Parse(bigNum2.bigNum[i].ToString()))
                        {
                            flagEqual = true;
                            break;
                        }
                        else if (int.Parse(bigNum2.bigNum[i].ToString()) > int.Parse(bigNum1.bigNum[i].ToString()))
                        {
                            BigInt rtrResult = new BigInt(bigNum2 - bigNum1);
                            rtrResult.bigNum = "-" + rtrResult.bigNum;
                            return rtrResult;
                        }
                    }
                }

                if ((numOneLength > numTwoLength) || flagEqual)
                {
                    for (int i = 0; i < numOneLength - numTwoLength; i++)
                    {
                        bigNum2.bigNum = "0" + bigNum2.bigNum;
                    }

                    for (int i = numOneLength - 1; i >= 0; i--)
                    {
                        if (int.Parse(bigNum1.bigNum[i].ToString()) < int.Parse(bigNum2.bigNum[i].ToString()))
                        {
                            int pos = i - 1;
                            while (bigNum1.bigNum[pos] == '0' && pos >= 1)
                            {
                                bigNum1.bigNum = ReplaceCharAtPosition(bigNum1.bigNum, pos, "9");
                                pos--;
                            }
                            bigNum1.bigNum = ReplaceAndLowerPrevNum(bigNum1.bigNum, pos);

                            int a, b;
                            a = int.Parse(bigNum1.bigNum[i].ToString());
                            b = int.Parse(bigNum2.bigNum[i].ToString());
                            a += 10;
                            int x = a - b;
                            result = x.ToString() + result;
                        }
                        else if (int.Parse(bigNum2.bigNum[i].ToString()) <= int.Parse(bigNum1.bigNum[i].ToString()))
                        {
                            int a, b;
                            a = int.Parse(bigNum1.bigNum[i].ToString());
                            b = int.Parse(bigNum2.bigNum[i].ToString());
                            int x = a - b;
                            result = x.ToString() + result;
                        }
                    }
                }
                if (numOneLength < numTwoLength)
                {
                    BigInt rtrResult = new BigInt(bigNum2 - bigNum1);
                    rtrResult.bigNum = "-" + rtrResult.bigNum;
                    return rtrResult;
                }
            }

            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] == '-')
            {
                numOneLength = bigNum1.bigNum.Length;
                numTwoLength = bigNum2.bigNum.Length;
                bool flagEqual = false;
                if (numOneLength == numTwoLength)
                {
                    for (int i = 1; i < numOneLength; i++)
                    {
                        if (int.Parse(bigNum1.bigNum[i].ToString()) > int.Parse(bigNum2.bigNum[i].ToString()))
                        {
                            flagEqual = true;
                            break;
                        }
                        else if (int.Parse(bigNum2.bigNum[i].ToString()) > int.Parse(bigNum1.bigNum[i].ToString()))
                        {
                            bigNum1.bigNum = bigNum1.bigNum.Substring(1, numOneLength - 1);
                            bigNum2.bigNum = bigNum2.bigNum.Substring(1, numTwoLength - 1);
                            BigInt rtrResult = new BigInt(bigNum2 - bigNum1);
                            return rtrResult;
                        }
                    }
                }
                if ((numOneLength > numTwoLength) || flagEqual)
                {
                    bigNum1.bigNum = bigNum1.bigNum.Substring(1, numOneLength - 1);
                    bigNum2.bigNum = bigNum2.bigNum.Substring(1, numTwoLength - 1);
                    BigInt rtrResult = new BigInt(bigNum1 - bigNum2);
                    rtrResult.bigNum = "-" + rtrResult.bigNum;
                    return rtrResult;
                }
                if (numTwoLength > numOneLength)
                {
                    bigNum1.bigNum = bigNum1.bigNum.Substring(1, numOneLength - 1);
                    bigNum2.bigNum = bigNum2.bigNum.Substring(1, numTwoLength - 1);
                    BigInt rtrResult = new BigInt(bigNum2 - bigNum1);
                    return rtrResult;
                }
            }

            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] != '-')
            {
                numOneLength = bigNum1.bigNum.Length;
                numTwoLength = bigNum2.bigNum.Length;

                bigNum1.bigNum = bigNum1.bigNum.Substring(1, numOneLength - 1);
                BigInt rtrResult = new BigInt(bigNum1 + bigNum2);
                rtrResult.bigNum = "-" + rtrResult.bigNum;
                return rtrResult;
            }

            if (bigNum1.bigNum[0] != '-' && bigNum2.bigNum[0] == '-')
            {
                numOneLength = bigNum1.bigNum.Length;
                numTwoLength = bigNum2.bigNum.Length;

                bigNum2.bigNum = bigNum2.bigNum.Substring(1, numTwoLength - 1);
                BigInt rtrResult = new BigInt(bigNum1 + bigNum2);
                return rtrResult;
            }

        Result:

            if (result != "0")
            {
                result = result.TrimStart('0');
                BigInt returnResult = new BigInt(result);
                return returnResult;
            }
            else
            {
                BigInt returnResult = new BigInt(result);
                return returnResult;
            }
        }

        public static BigInt operator *(BigInt bigNum1, BigInt bigNum2)
        {
            if (bigNum1.bigNum != "0")
            {
                bigNum1.bigNum = bigNum1.bigNum.TrimStart('0');
            }

            if (bigNum2.bigNum != "0")
            {
                bigNum2.bigNum = bigNum2.bigNum.TrimStart('0');
            }
            if (bigNum1.bigNum == "0" || bigNum2.bigNum == "0")
            {
                BigInt rtrResult = new BigInt("0");
                return rtrResult;
            }

            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] != '-')
            {
                bigNum1.bigNum = bigNum1.bigNum.Remove(0, 1);
                BigInt rtrResult = new BigInt(bigNum1 * bigNum2);
                rtrResult.bigNum = "-" + rtrResult.bigNum;
                return rtrResult;
            }
            else if (bigNum1.bigNum[0] != '-' && bigNum2.bigNum[0] == '-')
            {
                bigNum2.bigNum = bigNum2.bigNum.Remove(0, 1);
                BigInt rtrResult = new BigInt(bigNum1 * bigNum2);
                rtrResult.bigNum = "-" + rtrResult.bigNum;
                return rtrResult;
            }
            else
            {
                if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] == '-')
                {
                    bigNum1.bigNum = bigNum1.bigNum.Remove(0, 1);
                    bigNum2.bigNum = bigNum2.bigNum.Remove(0, 1);
                }
                
                BigInt carry = new BigInt("0");
                BigInt sum = new BigInt("0");
                BigInt bigProduct = new BigInt("0");
                BigInt ten = new BigInt("10");
                BigInt bigPartial = new BigInt("");
                BigInt result = new BigInt("");
                BigInt bigTemp = new BigInt();
                List<BigInt> bigPartialList = new List<BigInt>();

                for (int j = bigNum2.bigNum.Length - 1; j >= 0; j--)
                {
                    for (int i = bigNum1.bigNum.Length - 1; i >= 0; i--)
                    {
                        bigProduct = new BigInt((int.Parse(bigNum1.bigNum[i].ToString()) * int.Parse(bigNum2.bigNum[j].ToString())).ToString());
                        bigProduct = bigProduct + carry;


                        carry = bigProduct / ten;
                        bigPartial.bigNum = bigProduct.bigNum[bigProduct.bigNum.Length - 1].ToString() + bigPartial.bigNum ;
                    }

                    if (carry.bigNum != "0")
                    {
                        bigPartial.bigNum = carry.bigNum + bigPartial.bigNum;
                    }

                    bigPartialList.Add(bigPartial);
                    bigPartial = new BigInt("");
                    carry.bigNum = "0";
                }

                for (int i = 1; i < bigPartialList.Count; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        bigPartialList[i].bigNum = bigPartialList[i].bigNum + "0";
                    }
                }

                int largestPartial = bigPartialList[bigPartialList.Count - 1].bigNum.Length;
                
                int zeroes;
                for (int i = 0; i < bigPartialList.Count; i++)
                {
                    zeroes = largestPartial - bigPartialList[i].bigNum.Length;
                    if (zeroes >= 1)
                    {
                        for (int j = zeroes; j > 0; j--)
                        {
                            bigPartialList[i].bigNum = "0" + bigPartialList[i].bigNum;
                        }
                    }
                }

                carry.bigNum = "0";
                for (int i = largestPartial - 1; i >= 0; i--)
                {
                    sum.bigNum = "0";
                    for (int j = 0; j < bigPartialList.Count; j++)
                    {
                        bigTemp.bigNum = bigPartialList[j].bigNum[i].ToString();
                        sum += bigTemp;
                    }

                    sum = sum + carry;
                    carry = sum / ten;
                    result.bigNum = sum.bigNum[sum.bigNum.Length - 1].ToString() + result.bigNum;
                }

                if (carry.bigNum != "0")
                {
                    result.bigNum = carry.bigNum + result.bigNum;
                }
                return result;
            }
        }

        public static BigInt operator /(BigInt bigNum1, BigInt bigNum2)
        {
            if (bigNum1.bigNum != "0")
            {
                bigNum1.bigNum = bigNum1.bigNum.TrimStart('0');
            }

            if (bigNum2.bigNum != "0")
            {
                bigNum2.bigNum = bigNum2.bigNum.TrimStart('0');
            }

            if (bigNum1.bigNum == "0" && bigNum2.bigNum == "0")
            {
                BigInt rtrResult = new BigInt("NaN");
                return rtrResult;
            }

            if (bigNum1.bigNum != "0" && bigNum2.bigNum == "0")
            {
                BigInt rtrResult = new BigInt("undefined");
                return rtrResult;
            }

            if (bigNum1.bigNum == "0" && bigNum2.bigNum != "0")
            {
                BigInt rtrResult = new BigInt("0");
                return rtrResult;
            }

            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] != '-')
            {
                bigNum1.bigNum = bigNum1.bigNum.Remove(0, 1);
                BigInt rtrResult = new BigInt(bigNum1 / bigNum2);
                if (rtrResult.bigNum == "0")
                {
                    return rtrResult;
                }
                else
                {
                    rtrResult.bigNum = "-" + rtrResult.bigNum;
                    return rtrResult;
                }
            }

            if (bigNum1.bigNum[0] != '-' && bigNum2.bigNum[0] == '-')
            {
                bigNum2.bigNum = bigNum2.bigNum.Remove(0, 1);
                BigInt rtrResult = new BigInt(bigNum1 / bigNum2);
                if (rtrResult.bigNum == "0")
                {
                    return rtrResult;
                }
                else
                {
                    rtrResult.bigNum = "-" + rtrResult.bigNum;
                    return rtrResult;
                }
            }

            if (bigNum1.bigNum[0] == '-' && bigNum2.bigNum[0] == '-')
            {
                bigNum1.bigNum = bigNum1.bigNum.Remove(0, 1);
                bigNum2.bigNum = bigNum2.bigNum.Remove(0, 1);
            }

            bool looping = true;
            BigInt count = new BigInt("0");
            BigInt incr = new BigInt(bigNum2);

            while (looping)
            {
                if (bigNum1 >= incr)
                {
                    incr += bigNum2;
                    count++;
                }
                else
                {
                    looping = false;
                }
            }

            if (bigNum1.bigNum != "0")
            {
                bigNum1.bigNum = bigNum1.bigNum.TrimStart('0');
            }

            if (bigNum2.bigNum != "0")
            {
                bigNum2.bigNum = bigNum2.bigNum.TrimStart('0');
            }

            return count;
        }
    }
}