using System;

namespace GMessage
{
    public partial class Service : IService
    {
        public virtual Delegate Serve { get; protected set; }

        public Service(Delegate @delegate)
        {
            Serve = @delegate;
        }
    }
}