// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.Emit;

namespace Evm.IL{
    public class Example6
    {
        private delegate void HelloDelegate();
        public static void call()
        {
            DynamicMethod DoSum = new("DoSum", typeof(void), new Type[]{typeof(Int32).MakeByRefType(), typeof(Int32)}, true)
            {
                InitLocals = false
            };

            ILGenerator il = DoSum.GetILGenerator();

            LocalBuilder x= il.DeclareLocal(typeof(Int32));

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldind_I4);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);

            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc_0);

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Stind_I4);

            il.Emit(OpCodes.Ret);


            DynamicMethod method = new("main", typeof(void), null, true)
            {
                InitLocals = false
            };

            ILGenerator main_il =  method.GetILGenerator();

            LocalBuilder x2 = main_il.DeclareLocal(typeof(Int32));
            LocalBuilder y = main_il.DeclareLocal(typeof(Int32));


            main_il.Emit(OpCodes.Ldc_I4, 10);
            main_il.Emit(OpCodes.Stloc_0);
            main_il.Emit(OpCodes.Ldc_I4, 20);
            main_il.Emit(OpCodes.Stloc_1);

            main_il.Emit(OpCodes.Ldloca, 0);
            main_il.Emit(OpCodes.Ldloc_1);
            main_il.Emit(OpCodes.Call, DoSum);
            main_il.Emit(OpCodes.Ldloc_0);
            main_il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(Int32) }));
            main_il.Emit(OpCodes.Ret);

            method.Invoke(null,  null);
        }
    }
}


