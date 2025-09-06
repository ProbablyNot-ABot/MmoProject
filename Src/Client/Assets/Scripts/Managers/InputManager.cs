using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class InputManager : MonoSingleton<InputManager>
    {
        public bool IsInputMode = false;//当前是否是输入模式
    }
}
