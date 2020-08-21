namespace MGame.General
{
    public class MasonRandom
    {
        private int[] MT = new int[624];
        private int idx;
        private bool isInitialized = false;

        public static MasonRandom globalRandom = new MasonRandom();


        public static MasonRandom GetMasonRandom()
        {
            return new MasonRandom();
        }
        public MasonRandom()
        {
            msInit(1);
        }

        /* Initialize the generator from a seed */
        private void msInit(int seed)
        {
            int i;
            int p;
            idx = 0;
            MT[0] = seed;
            for (i = 1; i < 624; ++i)
            { /* loop over each other element */
                p = 1812433253 * (MT[i - 1] ^ (MT[i - 1] >> 30)) + i;
                MT[i] = (int)(p & 0xffffffff); /* get last 32 bits of p */
            }
            isInitialized = true;
        }

        private int msRand()
        {
            if (!isInitialized)
            {
                return 0;
            }


            if (idx == 0)
                msRenerate();

            int y = MT[idx];
            y = y ^ (y >> 11);
            y = (int)(y ^ ((y << 7) & 2636928640L));
            y = (int)(y ^ ((y << 15) & 4022730752L));
            y = y ^ (y >> 18);

            idx = (idx + 1) % 624; /* increment idx mod 624*/
            return y;
        }

        private void msRenerate()
        {
            int i;
            int y;
            for (i = 0; i < 624; ++i)
            {
                y = (int)(MT[i] & 0x80000000) +
                        (MT[(i + 1) % 624] & 0x7fffffff);
                MT[i] = MT[(i + 397) % 624] ^ (y >> 1);
                if (y % 2 != 0)
                { /* y is odd */
                    MT[i] = (int)(MT[i] ^ 2567483615L);
                }
            }
        }

        //随机种子  
        public void SetSeed(int seed)
        {
            msInit(seed);
        }

        //随机值  

        public int Range()
        {
            if (isInitialized == false)
            {
                return 0;
            }
            int rd = msRand();
            if (rd == int.MaxValue)
            {
                rd = int.MaxValue - 1;
            }
            return rd;
        }

        public int Range(int min, int max)//不包括 max
        {
            if (isInitialized == false || max < min)
            {
                return 0;
            }

            if (max == min)
            {
                return min;
            }

            int delta = max - min;

            long rand = ((long)delta * Range());
            long r1 = rand % int.MaxValue;
            rand -= r1;
            return (int)(rand / int.MaxValue + min);
        }

        public int Range(int max)
        {
            return Range(0, max);
        }

    }
}