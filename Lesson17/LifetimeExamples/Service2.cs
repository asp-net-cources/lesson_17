namespace Lesson17.LifetimeExamples;

public class Service2
{
    private readonly ScopedClass _scopedClass;

    public Service2(ScopedClass scopedClass)
    {
        _scopedClass = scopedClass;
    }

    public int ScopeNumber => _scopedClass.Some;
}