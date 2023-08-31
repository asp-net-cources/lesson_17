namespace Lesson17.SingletonExamples;

public class SingletonExtended
{
    private SingletonExtended()
    {
    }
    
    public string Some { get; set; }

    public void Do()
    {
        // TODO
    }

    private static SingletonExtended _object;

    public SingletonExtended GetInstance()
    {
        if (_object == null)
        {
            _object = new SingletonExtended();
        }

        return _object;
    }
}