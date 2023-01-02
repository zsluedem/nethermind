// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.Emit;

namespace Evm.IL{
    public class Example4
    {
        private delegate void HelloDelegate();
        public static void call()
        {
            DynamicMethod method = new("IL_Test", typeof(void), null, true)
            {
                InitLocals = false
            };

            ILGenerator il = method.GetILGenerator();
            
            LocalBuilder x1 = il.DeclareLocal(typeof(Int32));
            LocalBuilder x2 = il.DeclareLocal(typeof(Int32));
            Label Start = il.DefineLabel();
            Label Exit = il.DefineLabel();

            il.Emit(OpCodes.Ldc_I4, 4);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldc_I4, 0);
            il.Emit(OpCodes.Stloc_1);
            il.MarkLabel(Start);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ldloc_1);

            il.Emit(OpCodes.Ble, Exit);

            il.Emit(OpCodes.Ldloc_1);
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(Int32)} ));


            il.Emit(OpCodes.Ldc_I4, 1);
            il.Emit(OpCodes.Ldloc_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc_1);
            il.Emit(OpCodes.Br, Start);

            il.MarkLabel(Exit);
            il.Emit(OpCodes.Ret);
            
            method.Invoke(null,  null);
            // HelloDelegate hi =
            //     (HelloDelegate) method.CreateDelegate(typeof(HelloDelegate));
            // hi();
        }
    }
}

