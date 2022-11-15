using System.Buffers.Binary;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Nethermind.Evm.Test;

namespace Nethermind.Evm.ILBenchmark
{

    public class ILEvmVsEvm
    {

        public struct WrapByteCode
        {
            public WrapByteCode(byte[] byteCode) => ByteCode = byteCode;

            public byte[] ByteCode { get; }

            public override string ToString() => BitConverter.ToString(ByteCode).Replace("-","");
        }

        private VirtualMachineTestsBase _evm;
        private VirtualMachineTestsBase _ilEvm;

        public IEnumerable<WrapByteCode> Bytecodes
        {
            get
            {
                yield return new WrapByteCode(bytecode1);
                yield return new WrapByteCode(bytecode2);
                yield return new WrapByteCode(bytecode3);
                yield return new WrapByteCode(bytecode4);
                yield return new WrapByteCode(bytecode5);
                yield return new WrapByteCode(bytecode6);
                yield return new WrapByteCode(bytecode7);
                yield return new WrapByteCode(bytecode8);
                yield return new WrapByteCode(bytecode9);


                const int loopCount = 200_000;
                byte[] repeat = new byte[4];
                BinaryPrimitives.TryWriteInt32BigEndian(repeat, loopCount);
                byte[] code = Prepare.EvmCode
                    .PushData(repeat)
                    .Op(Instruction.JUMPDEST) // counter
                    .PushData(1) // counter, 1
                    .Op(Instruction.SWAP1) // 1, counter
                    .Op(Instruction.SUB) // counter-1
                    .Op(Instruction.DUP1) // counter-1, counter-1
                    .PushData(1 + repeat.Length) // counter-1, counter-1, 2
                    .Op(Instruction.JUMPI)      // counter-1
                    .Op(Instruction.POP)
                    .Done;
                yield return new WrapByteCode(code);
                
            }
        }
        byte[] bytecode1 = Prepare.EvmCode
            .Op(Instruction.PC)
            .Op(Instruction.POP)
            .Done;

        byte[] bytecode2 = Prepare.EvmCode
            .PushData(1)
            .Op(Instruction.JUMP)
            .Done;
        byte[] bytecode3 = Prepare.EvmCode
            .PushData(3)
            .Op(Instruction.JUMP)
            .Op(Instruction.JUMPDEST)
            .Done;

        byte[] bytecode4 = Prepare.EvmCode
            .PushData(0) // invalid condition
            .PushData(1) // address
            .Op(Instruction.JUMPI)
            .Done;

        byte[] bytecode5 = Prepare.EvmCode
            .PushData(1) // valid condition
            .PushData(1) // address
            .Op(Instruction.JUMPI)
            .Done;

        byte[] bytecode6 = Prepare.EvmCode
            .PushData(1) // valid condition
            .PushData(5) // address
            .Op(Instruction.JUMPI)
            .Op(Instruction.JUMPDEST)
            .Done;

        byte[] bytecode7 = Prepare.EvmCode
            .PushData(1)
            .PushData(2)
            .PushData(1)
            .PushData(4)
            .Op(Instruction.SUB)    // 4 - 1 = 3
            .Op(Instruction.SUB)    // 3 - 2 = 1
            .Op(Instruction.SUB)    // 1 - 1 = 0
            .Done;

        byte[] bytecode8 = Prepare.EvmCode
            .PushData(1)
            .Op(Instruction.DUP1)
            .Op(Instruction.SUB)
            .Op(Instruction.POP)
            .Done;

        byte[] bytecode9 = Prepare.EvmCode
            .PushData(2)
            .PushData(1)
            .Op(Instruction.SWAP1)
            .Op(Instruction.SUB)
            .Done;

        [ParamsSource(nameof(Bytecodes))]
        public WrapByteCode Bytecode { get; set; }


        [GlobalSetup]
        public void GlobalSetup()
        {
            _evm = new VirtualMachineTestsBase();
            _ilEvm = new VirtualMachineTestsBase();
            _evm.Setup();
            _ilEvm.Setup();
            _ilEvm.Machine.BuildILForNext();
        }


        [Benchmark]
        public void ILEvm()
        {   
            _ilEvm.Execute(Bytecode.ByteCode);
        }

        [Benchmark]
        public void Evm()
        {
            _evm.Execute(Bytecode.ByteCode);
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(ILEvmVsEvm));
        }
    }
}
