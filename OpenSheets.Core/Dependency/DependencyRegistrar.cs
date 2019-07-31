using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSheets.Core.Dependency
{
    public interface IRegisterDependency
    {
        void Register(RegistrationDelegate regDelegate);
    }

    public delegate void RegistrationDelegate(Type serviceType, Type interfaceType);
}
