using System;

namespace ConsoleFindInt.Work
{
    public static class CalcResult
    {
        //task function
        public static long GetNextSmaller(long number)
        // some tests will include very large numbers so use long, not ulong because function can return -1
        {
            long answer = -1;
            if (number <= 20) return -1;
            if (number <= 99)
            {
                answer = number % 10 * 10 + number / 10;
                if (answer < number) return answer;
                else return -1;
            }

            long digitCount = (long)Math.Log10(number) + 1;
            //use Array not List<T> for capacity and speed
            byte[] numList = new byte[digitCount];
            long temp = number;

            for (long i = digitCount - 1; i >= 0; i--)
            {
                byte n = (byte)(temp % 10);
                temp /= 10;
                numList[i] = n;
            }

            for (long i = digitCount - 2; i >= 0; i--)
            {
                byte exhcange;

                //sort digits in number
                for (long j = i + 1; j < digitCount; j++)
                {
                    for (long z = j + 1; z < digitCount; z++)
                    {
                        if (numList[j] < numList[z])
                        {
                            exhcange = numList[z];
                            numList[z] = numList[j];
                            numList[j] = exhcange;
                        }
                    }
                }

                //exchange digits in number for find
                for (long j = i + 1; j < digitCount; j++)
                {
                    if (numList[i] > numList[j])
                    {
                        exhcange = numList[i];
                        numList[i] = numList[j];
                        numList[j] = exhcange;
                        break;
                    }

                }

                //check
                if (numList[0] == 0) return -1;
                temp = 0;
                for (long j = 0; j < digitCount; j++)
                {
                    temp = temp + numList[digitCount - 1 - j] * (long)(Math.Pow(10, j));

                }
                answer = temp;
                if (answer != -1 && answer < number)
                    return answer;
                else answer = -1;

            }

            return answer;
        }

    }
}
