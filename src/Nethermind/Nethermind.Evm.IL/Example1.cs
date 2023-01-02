// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.Emit;

namespace Evm.IL{
    public class Example1
    {
        private delegate void HelloDelegate();
        public static void call()
        {
            DynamicMethod method = new("IL_Test", typeof(void), null, true)
            {
                InitLocals = false
            };

            ILGenerator il = method.GetILGenerator();

            il.Emit(OpCodes.Ldstr, "I am from the IL Assembly Language...");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Ret);

            method.Invoke(null,  null);
            // HelloDelegate hi =
            //     (HelloDelegate) method.CreateDelegate(typeof(HelloDelegate));
            // hi();
        }
    }
}


