// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.Emit;

namespace Evm.IL{
    public class Example3
    {
        private delegate void HelloDelegate();
        public static void call()
        {

            //             // Define a dynamic method
            // DynamicMethod method = new DynamicMethod("MyMethod", typeof(string), null);

            // // Get an ILGenerator for the method
            // ILGenerator ilGenerator = method.GetILGenerator();

            // // Emit the call opcode to call the Console.ReadLine method
            // ilGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine", Type.EmptyTypes));

            // // Emit the ret opcode to return the result
            // ilGenerator.Emit(OpCodes.Ret);

            // // Execute the method and print the result
            // Console.WriteLine(method.Invoke(null, null));

            DynamicMethod method = new("IL_Test", typeof(void), null, true)
            {
                InitLocals = false
            };

            ILGenerator il = method.GetILGenerator();

            Label Smaller = il.DefineLabel();
            Label Exit = il.DefineLabel();

            il.Emit(OpCodes.Ldstr, "Enter First Number");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine", Type.EmptyTypes));
            il.Emit(OpCodes.Call, typeof(Int32).GetMethod("Parse", new Type[] { typeof(string) }));

            il.Emit(OpCodes.Ldstr, "Enter Second Number");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine", Type.EmptyTypes)); 
            il.Emit(OpCodes.Call, typeof(Int32).GetMethod("Parse", new Type[] { typeof(string) }));

            il.Emit(OpCodes.Ble, Smaller);
            il.Emit(OpCodes.Ldstr, "Second Number is smaller than first.");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Br, Exit);

            il.MarkLabel(Smaller);
            il.Emit(OpCodes.Ldstr, "First number is smaller than second.");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));

            il.MarkLabel(Exit);
            il.Emit(OpCodes.Ret);

            method.Invoke(null,  null);
            // HelloDelegate hi =
            //     (HelloDelegate) method.CreateDelegate(typeof(HelloDelegate));
            // hi();
        }
    }
}


