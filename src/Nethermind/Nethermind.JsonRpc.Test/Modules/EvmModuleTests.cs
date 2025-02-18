//  Copyright (c) 2021 Demerzel Solutions Limited
//  This file is part of the Nethermind library.
// 
//  The Nethermind library is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  The Nethermind library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.

using Nethermind.Consensus.Producers;
using Nethermind.JsonRpc.Modules.Evm;
using NSubstitute;
using NUnit.Framework;

namespace Nethermind.JsonRpc.Test.Modules
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class EvmModuleTests
    {
        [Test]
        public void Evm_mine()
        {
            IManualBlockProductionTrigger trigger = Substitute.For<IManualBlockProductionTrigger>();
            EvmRpcModule rpcModule = new(trigger);
            string response = RpcTest.TestSerializedRequest<IEvmRpcModule>(rpcModule, "evm_mine");
            Assert.AreEqual("{\"jsonrpc\":\"2.0\",\"result\":true,\"id\":67}", response);
            trigger.Received().BuildBlock();
        }
    }
}
