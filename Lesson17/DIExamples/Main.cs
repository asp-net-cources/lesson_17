using Lesson17.DIExamples;
using Lesson17.Models;

namespace Lesson15.DIExamples;

public class Main
{
    // public InterfaceA objectA;
    // public InterfaceB objectB;
    // public InterfaceC objectC;
    // public InterfaceD objectD;
    //
    // public static void Config()
    // {
    //     objectA = new ClassA();
    //     objectD = new ClassD();
    //     objectB = new ClassB(objectD);
    //     objectC = new ClassC(objectA, objectB);
    // }
    
    public static void Run()
    {
        var objectA = new ClassA(new ClassB1());
        objectA.Write();
        objectA.ObjectB = new ClassB2();
        objectA.Write();
    }
}


    
