using System;

namespace GMessage
{
    public interface IService
    {
        Delegate Serve { get; }
    }
}