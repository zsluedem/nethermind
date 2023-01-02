// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.Emit;

namespace Evm.IL{
    public class Example2
    {
        private delegate void HelloDelegate();
        public static void call()
        {
            DynamicMethod method = new("IL_Test", typeof(void), null, true)
            {
                InitLocals = false
            };

            ILGenerator il = method.GetILGenerator();

            il.Emit(OpCodes.Ldstr, "The sum of 50 and 30 is = ");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));

            il.Emit(OpCodes.Ldc_I4_S, 50);
            il.Emit(OpCodes.Ldc_I4, 30);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(Int32) }));
            il.Emit(OpCodes.Ret);

            method.Invoke(null,  null);
            // HelloDelegate hi =
            //     (HelloDelegate) method.CreateDelegate(typeof(HelloDelegate));
            // hi();
        }
    }
}


