using System;

public class Preconditions
{
    public static T CheckNotNull<T>(T reference, string message)
    {
        if (reference == null)
        {
            throw new ArgumentNullException();
        }

        return reference;
    }
}
