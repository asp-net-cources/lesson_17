namespace Lesson17.LifetimeExamples;

public class Service1
{
    private readonly ScopedClass _scopedClass;

    public Service1(ScopedClass scopedClass)
    {
        _scopedClass = scopedClass;
    }

    public int ScopeNumber => _scopedClass.Some;
}