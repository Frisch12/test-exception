using System;
using System.Threading.Tasks;

namespace ExceptionTestRealTest
{
    public interface IMyCoolTestInterfaceWhichCantDoMuch
    {
        Task<String> DoMuchStuff(String stuff);
    }
}