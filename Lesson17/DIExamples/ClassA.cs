namespace Lesson17.DIExamples;

public class ClassA : InterfaceA
{
    public InterfaceB ObjectB { get; set; }

    public ClassA(InterfaceB objectB)
    {
        ObjectB = objectB;
    }

    public void Write()
    {
        Console.WriteLine(ObjectB.Name);
    }
}