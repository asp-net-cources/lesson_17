namespace Lesson17.DIExamples;

public class ClassC
{
    public InterfaceA ObjectA { get; set; }

    public ClassC(InterfaceA objectA)
    {
        ObjectA = objectA;
    }

    public void Write()
    {
        ObjectA.Write();
    }
}