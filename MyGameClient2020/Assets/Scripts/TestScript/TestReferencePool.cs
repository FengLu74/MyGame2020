
using FrameWork.ReferencePool;

public class TestReferencePool
{


    public C TestH()
    {
        C testC = ReferencePool.Acquire<C>();
        return testC;
    }

    public void Rel(C c)
    {
        ReferencePool.Release(c);
    }

    public class C : B
    {
        public int dd;
        public C()
        {
            dd = 1;
        }
        public  void Clear()
        {
            base.Clear();

            dd = 0;
        }
    }

    public class B : A
    {
        public int cc;
        public B() 
        {
            cc = 2;
        }
        public  void Clear()
        {
            base.Clear();

            cc = 0;
        }

    }
    public class A : IReference
    {
        public int aa;
        public A()
        {
            aa = 3;
        }
        public void Clear()
        {
            aa = 0;
        }
    }
}
