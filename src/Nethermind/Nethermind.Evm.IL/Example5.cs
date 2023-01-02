// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.Emit;

namespace Evm.IL{
    public class Example5
    {
        private delegate void HelloDelegate();
        public static void call()
        {

            DynamicMethod DoSum = new("DoSum", typeof(Int32), new Type[]{typeof(Int32), typeof(Int32)}, true)
            {
                InitLocals = false
            };

            ILGenerator DoSum_il = DoSum.GetILGenerator();

            DoSum_il.Emit(OpCodes.Ldarg_0);
            DoSum_il.Emit(OpCodes.Ldarg_1);
            DoSum_il.Emit(OpCodes.Add);
            DoSum_il.Emit(OpCodes.Ret);



            DynamicMethod PrintSum = new("PrintSum", typeof(void),  new Type[] { typeof(Int32) }, true);

            ILGenerator PrintSum_il = PrintSum.GetILGenerator();

            PrintSum_il.Emit(OpCodes.Ldstr, "The Result is : ");
            PrintSum_il.Emit(OpCodes.Call,  typeof(Console).GetMethod("Write", new Type[] { typeof(string) }));
            PrintSum_il.Emit(OpCodes.Ldarg_0);
            PrintSum_il.Emit(OpCodes.Call,  typeof(Console).GetMethod("WriteLine", new Type[] { typeof(Int32) }));
            PrintSum_il.Emit(OpCodes.Ret);



            DynamicMethod method = new("main", typeof(void), null, true)
            {
                InitLocals = false
            };

            ILGenerator main_il = method.GetILGenerator();

            main_il.Emit(OpCodes.Ldc_I4, 10);
            main_il.Emit(OpCodes.Ldc_I4, 20);
            main_il.Emit(OpCodes.Call, DoSum);
            main_il.Emit(OpCodes.Call, PrintSum);
            main_il.Emit(OpCodes.Ret);

            
            method.Invoke(null,  null);
            // HelloDelegate hi =
            //     (HelloDelegate) method.CreateDelegate(typeof(HelloDelegate));
            // hi();
        }
    }
}

